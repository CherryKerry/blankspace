using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextPrompt : MonoBehaviour
{

		private Text promptTextbox;
		private Manager.Sentance currentSentance;

		public float fadeInTime;
		public float fadeOutTime;

		static TextPrompt instance;
		

		void Start ()
		{
				Initializations ();
				TextPrompt.instance = this;
		}
	
		void Update ()
		{
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
				string promptMessage = RetrievePromptMessage ();
				SetPromptText (promptMessage);
		}

		void EventListenerInitializations ()
		{
				WordChoices.onSelectWord += UpdateToCompletedSentence;
		}

		string GetPromptText ()
		{
				return promptTextbox.text;
		}

		public static void SetSentance(Manager.Sentance sentance) 
		{
			instance.currentSentance = sentance;
			instance.SetPromptText (sentance.display);
		}

		void SetPromptText (string promptText)
		{
				promptTextbox.text = promptText;
		}
		
		string RetrievePromptMessage ()
		{
				string promptString = "i am a __________ promptString";
				//retrieve from message
				return promptString;
		}

		void UpdateToCompletedSentence (WordChoices selectedWord)
		{
				string word = selectedWord.GetComponent <Text> ().text;
				SetPromptText ("i am a " + word + " promptString");
		}


}
