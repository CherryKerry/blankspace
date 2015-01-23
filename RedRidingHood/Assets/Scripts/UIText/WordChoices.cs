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

		void Start ()
		{
				Initializations ();
		}
	
		void Update ()
		{
				//wordTextbox.rectTransform.position.y = 60;
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
				//SetWordText (promptMessage);
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
				onSelectWord (this);
				//Debug.Log ("onSelectWord: " + this);
		}

}
