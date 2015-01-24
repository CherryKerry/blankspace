using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WordChoices : MonoBehaviour
{
		public delegate void PuddleTapEventHandler (WordChoices selectedWord);
		public static event PuddleTapEventHandler onSelectWord;
		private Text wordTextbox;
		public float fadeInTime;
		public float fadeOutTime;
		private bool textPromptCollision;
		private Manager.Word word;
		private Vector2 position;

		static int offset = 50;
		static int wordWidth = 100;

		void Start ()
		{
				Initializations ();
		}
	
		void Update ()
		{
		}
		
		void Initializations ()
		{
				TimingInitialize ();
				TextInitialize ();
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
				//string promptMessage = RetrieveWordChoice ();
				if (word != null) {
					SetWordText (word.word);
				}
		}

		public string GetWordText ()
		{
				return wordTextbox.text;
		}
	
		public void SetWordText (string promptText)
		{
				wordTextbox.text = promptText;
		}

		string RetrieveWordChoice ()
		{
				string WordChoice = "beautiful";
				//retrieve from message
				return WordChoice;
		}

		void OnMouseDown ()
		{
				//onSelectWord (this);
				//Debug.Log ("onSelectWord: " + this);
		}

		void OnMouseDrag ()
		{
				Vector3 point = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				//point.x = gameObject.transform.position.x;
				//point.y = gameObject.transform.position.y;
				point.z = gameObject.transform.position.z;
				gameObject.transform.position = point;
		}

		void OnTriggerEnter2D (Collider2D collission)
		{
				if (collission.gameObject.name == "TextPrompt") {
						//Debug.Log (this + "Collision with " + collission.gameObject.name);			
						textPromptCollision = true;
				}
		}

		void OnMouseUp ()
		{
				if (textPromptCollision) {
						onSelectWord (this);
						//gameObject.SetActive (false);
				}
		}

		public static void SetWordsTo(ArrayList words) 
		{ 
			foreach (GameObject word in GameObject.FindGameObjectsWithTag ("Word")) {
				Destroy(word);
			}

			GameObject container = GameObject.Find("WordContainer");
			float width = -((RectTransform)container.transform).sizeDelta.x / 2;

			for (int i = 0; i < words.Count; i++) {
				Manager.Word word = words[i] as Manager.Word;
				
				GameObject gameObject = Instantiate(Resources.Load("Word")) as GameObject;
				gameObject.transform.SetParent(container.transform);
				WordChoices choice = gameObject.GetComponent<WordChoices>();
				choice.word = word;

				choice.position = new Vector2(width + offset + wordWidth * i, 0);
				gameObject.transform.localPosition = choice.position;
				Vector3 scale = new Vector3(1,1,1);
				gameObject.transform.localScale = scale;
			}
		}
		
		
}
