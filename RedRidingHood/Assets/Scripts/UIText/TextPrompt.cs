using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextPrompt : MonoBehaviour
{
		public delegate void TextPromptEventHandler (Manager.Sentance sentance,Manager.Word word);
		public static event TextPromptEventHandler onCompleteSentence;

		private Text promptTextbox;
		private Manager.Sentance currentSentance;

		public float fadeInTime;
		public float fadeOutTime;
		private BoxCollider2D boxCollider;
		static TextPrompt instance;
		

		void Start ()
		{
				Initializations ();
				TextPrompt.instance = this;
				onCompleteSentence += Manager.SetKeyWord;
				boxCollider = GetComponent <BoxCollider2D> ();
		}
	
		void Update ()
		{
				float xx = 800;
				float yy = promptTextbox.rectTransform.sizeDelta.y;
				promptTextbox.rectTransform.sizeDelta = new Vector2 (xx, yy);
				
				boxCollider.size = promptTextbox.rectTransform.sizeDelta;
				boxCollider.transform.position = promptTextbox.rectTransform.position;
				
				float x = promptTextbox.rectTransform.sizeDelta.x / 2;
				float y = promptTextbox.rectTransform.sizeDelta.y / 2;
				boxCollider.center = new Vector2 (x, y);
		}

		void Initializations ()
		{
				EventListenerInitializations ();
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
