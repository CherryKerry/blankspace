using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextPrompt : MonoBehaviour
{

		private Text promptTextbox;

		public float fadeInTime;
		public float fadeOutTime;

		void Start ()
		{
				Initializations ();
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
				promptTextbox.CrossFadeAlpha (0f, 0.0f, false); //text first appear as faded out
				promptTextbox.CrossFadeAlpha (1f, fadeInTime, false); //fade in text
				string promptMessage = "";//retrieve message
				SetPrompt (promptMessage);
		}

		void EventListenerInitializations ()
		{
		}

		private string GetPrompt ()
		{
				return promptTextbox.text;
		}

		private void SetPrompt (string promptText)
		{
				promptTextbox.text = promptText;
		}

}
