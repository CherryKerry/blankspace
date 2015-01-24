using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WordChoices : MonoBehaviour
{
		public delegate void WordChoicesEventHandler (WordChoices selectedWord);
		public static event WordChoicesEventHandler onSelectWord;

		private Text wordTextbox;
		private Vector2 position;
		private BoxCollider2D boxCollider;

		public float fadeInTime;
		public float fadeOutTime;
	
		private bool markedForDestruction = false;

		public dragState currentDragState = dragState.nothing;
		
		public Manager.Word word;
	
		static int offset = 50;
		static int wordWidth = 100;

		void Start ()
		{
				Initializations ();
		}
	
		void Update ()
		{
				BoxColliderUpdate ();
				if (markedForDestruction) {
						fadeOutTime-=Time.deltaTime;
				}
				if (fadeOutTime < 0) {
						Destroy(gameObject);
				}
		}

		void OnDisable()
		{
			//Debug.LogError("Stopped");
			Manager.OnEvent -= Manager_OnEvent;
		}

		void Initializations ()
		{
				boxCollider = GetComponent <BoxCollider2D> ();
				TimingInitialize ();
				TextInitialize ();
				Manager.OnEvent += this.Manager_OnEvent;
		}
	
		void TimingInitialize ()
		{
				fadeInTime = 2.0f;
				fadeOutTime = 1.0f;
		}
	
		void TextInitialize ()
		{
				wordTextbox = GetComponent <Text> ();
				wordTextbox.CrossFadeAlpha (0.0f, 0.0f, false); //text first appear as faded out
				wordTextbox.CrossFadeAlpha (1.0f, fadeInTime, false); //fade in text
				if (word != null) {
						SetWordText (word.word);
				}
		}

		void DestroyWithFade() 
		{
			wordTextbox = GetComponent <Text> ();
			wordTextbox.CrossFadeAlpha (0.0f, fadeOutTime, false); //fade in text
			markedForDestruction = true;
		}
	
		void BoxColliderUpdate ()
		{
				float x;
				float y;

				x = wordTextbox.rectTransform.position.x;
				y = wordTextbox.rectTransform.position.y;
				boxCollider.transform.position = new Vector2 (x, y);

				x = wordTextbox.rectTransform.sizeDelta.x / 8;
				y = wordTextbox.rectTransform.sizeDelta.y / 8;
				boxCollider.center = new Vector2 (x, y);

				boxCollider.size = wordTextbox.rectTransform.sizeDelta;

		}

		public string GetWordText ()
		{
				return wordTextbox.text;
		}
	
		public void SetWordText (string promptText)
		{
				wordTextbox.text = promptText;
		}

		void OnMouseDown ()
		{
				currentDragState = dragState.dragged_onto_nothing;
		}

		void OnMouseDrag ()
		{
				Vector3 point = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				point.z = gameObject.transform.position.z;
				gameObject.transform.position = point;
		}

		void OnTriggerEnter2D (Collider2D collission)
		{
				if (collission.gameObject.name == "TextPrompt") {
						//Debug.Log (this + "Collision with " + collission.gameObject.name);			
						currentDragState = dragState.dragged_onto_prompt;
				}
		}
		void OnTriggerExit2D (Collider2D collission)
		{
				if (collission.gameObject.name == "TextPrompt") {
						currentDragState = dragState.dragged_onto_nothing;
						//Debug.Log ("Exit " + collision.gameObject.name);
				}
		}

		void OnMouseUp ()
		{
				//Debug.Log ("onMouseup: " + currentDragState);
				if (currentDragState == dragState.dragged_onto_prompt) {
						onSelectWord (this);
				} else if (currentDragState == dragState.dragged_onto_nothing) {
						transform.localPosition = position;
				} else {
						currentDragState = dragState.nothing;
				}
		}

		public static void SetWordsTo (ArrayList words)
		{ 
				DestroyAll ();
				GameObject container = GameObject.Find ("WordContainer");
				float width = -((RectTransform)container.transform).sizeDelta.x / 2;

				for (int i = 0; i < words.Count; i++) {
						Manager.Word word = words [i] as Manager.Word;
				
						GameObject gameObject = Instantiate (Resources.Load ("Word")) as GameObject;
						gameObject.transform.SetParent (container.transform);
						WordChoices choice = gameObject.GetComponent<WordChoices> ();
						choice.word = word;

						choice.position = new Vector2 (width + offset + wordWidth * i, 0);
						gameObject.transform.localPosition = choice.position;
						Vector3 scale = new Vector3 (1, 1, 1);
						gameObject.transform.localScale = scale;
				}
		}

		public void Manager_OnEvent (string keyValue, string value)
		{
				if (this.gameObject != null && !markedForDestruction) {	
						if (value == word.word) {
				Destroy(gameObject);
						} else {
							DestroyWithFade();
						}
				} 
		}

		public static void DestroyAll ()
		{
				foreach (GameObject word in GameObject.FindGameObjectsWithTag ("Word")) {
						Destroy (word);
				}
		}

		void ColorChanger ()
		{
				string check = wordTextbox.text.ToLower ();
				//Debug.Log (check);
				if (check == "red") {
						wordTextbox.color = Color.red;
				} else if (check == "blue") {
						wordTextbox.color = Color.blue;
				} else if (check == "green") {
						wordTextbox.color = Color.green;
				} else if (check == "pink") {
						wordTextbox.color = new Color (253, 246, 250, 1);
				} else {
						//Debug.Log ("No " + check);
				}
		}	

		public enum dragState
		{
				dragged_onto_nothing,
				dragged_onto_prompt,
				nothing
		}

}
