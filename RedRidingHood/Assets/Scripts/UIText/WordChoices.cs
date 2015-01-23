using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WordChoices : MonoBehaviour
{
		private Text wordTextbox;
	
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
				wordTextbox.CrossFadeAlpha (0f, 0.0f, false); //text first appear as faded out
		}

		public string GetWordText ()
		{
				return wordTextbox.text;
		}
	
		public void SetWordText (string promptText)
		{
				wordTextbox.text = promptText;
		}

}
