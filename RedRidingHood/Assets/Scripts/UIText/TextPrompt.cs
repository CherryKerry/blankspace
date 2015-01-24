using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextPrompt : MonoBehaviour
{
		public delegate void TextPromptEventHandler (Manager.Sentance sentance,Manager.Word word);
		public static event TextPromptEventHandler onCompleteSentence;

		private Text promptTextbox;		
		private BoxCollider2D boxCollider;
		private Transform childGO;

		private Vector3 originalPosition;
		private Vector3 originalSize;

		public float fadeInTime;
		public float fadeOutTime;

		private Manager.Sentance currentSentance;
		static TextPrompt instance;
	
		void Start ()
		{
				Initializations ();
		}
	
		void Update ()
		{
				BoxColliderUpdate ();
		}

		void BoxColliderUpdate ()
		{
				float x;
				float y;
				x = 800;
				y = promptTextbox.rectTransform.sizeDelta.y;
				promptTextbox.rectTransform.sizeDelta = new Vector2 (x, y);

				x = promptTextbox.rectTransform.sizeDelta.x / 2;
				y = promptTextbox.rectTransform.sizeDelta.y / 2;
				boxCollider.center = new Vector2 (x, y);

				boxCollider.size = promptTextbox.rectTransform.sizeDelta;
				boxCollider.transform.position = promptTextbox.rectTransform.position;
		}

		void Initializations ()
		{
				EventListenerInitializations ();
				TimingInitialize ();
				TextInitialize ();
				TextPrompt.instance = this;
				onCompleteSentence += Manager.SetKeyWord;
				boxCollider = GetComponent <BoxCollider2D> ();
				childGO = this.transform.FindChild ("white");
				originalSize = childGO.transform.localScale;
				originalPosition = childGO.transform.localPosition; 
//				Debug.Log (originalPosition);
		}

		void TimingInitialize ()
		{
				fadeInTime = 2.0f;
				fadeOutTime = 1.0f;
		}

		void TextInitialize ()
		{
				promptTextbox = GetComponent <Text> ();
				promptTextbox.CrossFadeAlpha (0.0f, 0.0f, false); //text first appear as faded out
				promptTextbox.CrossFadeAlpha (1.0f, fadeInTime, false); //fade in text
		}

		void EventListenerInitializations ()
		{
				WordChoices.onSelectWord += UpdateToCompletedSentence;
		}

		string GetPromptText ()
		{
				return promptTextbox.text;
		}

		public static void SetSentance (Manager.Sentance sentance)
		{
				instance.currentSentance = sentance;
				instance.SetPromptText (sentance.display);
		}

		void SetPromptText (string promptText)
		{
				promptTextbox.text = promptText;

				childGO.transform.localScale = originalSize;
				childGO.transform.localPosition = originalPosition;
				//Debug.Log (promptTextbox.text);		
				//Debug.Log (promptTextbox.text.Length);
				int length = promptTextbox.text.Length;
				int times = length / 68;
				int extra = length % 68;
				if (times > 1 && extra > 0) {
						times++;
				}

				int count = 0;
				while (times > 0) {
						times--;
						//Debug.Log (childGO.transform.localScale.y * 2);
						float x, y, z;
						x = childGO.transform.localScale.x;
		
						//Debug.Log ("RAN 1.8f");
						y = childGO.transform.localScale.y * 1.8f;

						z = childGO.transform.localScale.z;
						childGO.transform.localScale = new Vector3 (x, y, z);
						x = childGO.transform.localPosition.x;
						y = childGO.transform.localPosition.y - 15;
						z = childGO.transform.localPosition.z;
						childGO.transform.localPosition = new Vector3 (x, y, z);
						//if(count  
						times--;
						count++;
				}
				promptTextbox.text = promptText;
				if (promptTextbox.text.IndexOf ("opened his mouth in shock and ") > 0) {
						float x, y, z;

						x = childGO.transform.localPosition.x;
						y = childGO.transform.localPosition.y - 15;
						z = childGO.transform.localPosition.z;
						childGO.transform.localPosition = new Vector3 (x, y, z);
			
				}
		}
	
		void UpdateToCompletedSentence (WordChoices selectedWord)
		{
				string word = selectedWord.GetComponent <Text> ().text;
				int start = promptTextbox.text.IndexOf ("_");		
				int end = promptTextbox.text.LastIndexOf ("_");
				string firstHalf = promptTextbox.text.Substring (0, start);
				string secondHalf = promptTextbox.text.Substring (end + 1);
				//Debug.Log (firstHalf + word + secondHalf);
				SetPromptText (firstHalf + word + secondHalf);
				//Debug.Log (selectedWord.word);
				onCompleteSentence (this.currentSentance, selectedWord.word);
		}
	
}
