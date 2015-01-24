using UnityEngine;
using System.Collections;
using System.Xml;
using System;

public class Manager : MonoBehaviour
{
	// keyWord is the word between '[]' inclusive eg "[hood_colour]"
	// word is the word set by the user
	public delegate void ManagerEventHandler (string keyWord, string word);
	public static event ManagerEventHandler OnEvent;

	static Manager instance;

	public float TIME_TO_WAIT = 1.0f;


	Hashtable sentances = new Hashtable ();
	//Keys are '[word]' including [] chars
	Hashtable keyWords = new Hashtable ();
	int nextSentance = 1;
	float waitTime = 0;

	// Use this for initialization
	void Start () 
	{
		LoadXml ();
		if (instance == null) {
			instance = this;
		} else {
			Debug.LogError("AN INSTANCE OF MANAGER ALREADY EXISTS");
		}
		WordChoices.DestroyAll ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (nextSentance != (0) && waitTime <= 0) {
			Sentance sentance = GetSentance (nextSentance);
			if (sentance != null) {
				TextPrompt.SetSentance (sentance);
				WordChoices.SetWordsTo (sentance.words);
				nextSentance = 0;
			} else {
				Debug.LogError("Failed to find sentance at index:" + nextSentance);
			}
		}
		if (waitTime >= 0) {
			waitTime -= Time.deltaTime;
		}
	}

	//Remove key word
	public static void ResetKeyWord(string keyWord) 
	{
		if (instance.keyWords.ContainsKey(keyWord)) {
			instance.keyWords.Remove(keyWord);
		} 
	}

	//Will add or set key word
	public static void SetKeyWord(Sentance sentance, Word word) 
	{
		if (OnEvent != null) {
			Debug.Log("Manager.OnEvent key:" + sentance.keyWord + " word:" + word.word);
			OnEvent (sentance.keyWord, word.word);
		}
		SetNextSentance(word.next);

		if (!sentance.keyWord.Equals("[blank]")) {
			instance.SetKeyWord (sentance.keyWord, word.word);
		} 
	}

	void SetKeyWord(string keyWord, string word) 
	{
		if (keyWords.ContainsKey(keyWord)) {
			keyWords[keyWord] = word;
		} else {
			keyWords.Add(keyWord, word);
		}
	}

	public static void SetNextSentance(int index)
	{
		instance.waitTime = instance.TIME_TO_WAIT;
		instance.nextSentance = index;
	}

	public Sentance GetSentance(int index) 
	{
		Sentance sentance = (Sentance)sentances [index];
		if (sentance != null) {
			string text = sentance.text;

			foreach (String key in keyWords.Keys) {
				text = text.Replace (key, (string)keyWords [key]);
			}
			if (text.IndexOf ('[') > 0) {
				int keyStart = text.IndexOf ('[');
				int keyLen = text.IndexOf (']') - keyStart + 1;
				string keyWord = text.Substring (keyStart, keyLen);
				sentance.keyWord = keyWord;
				text = text.Replace(keyWord, "_______");
			}
			sentance.display = text;
		}
		return sentance;
	}

	private void LoadXml () 
	{
		string uri = Application.dataPath + "/Resources/LittleRedRidingHood.xml";
		using (XmlReader reader = XmlReader.Create (uri)) {				
			Debug.Log("Started Parseing");
			// Parse the file and display each of the nodes.
			while (reader.ReadToFollowing ("sentence")) {
				int index = Convert.ToInt32 (reader.GetAttribute("id"));
				ParseSentance (reader.ReadSubtree (), index);
			}
		}
		Debug.Log ("Finished Parsing");
	}

	private void ParseWords(XmlReader reader, Sentance sentance) 
	{
		Debug.Log ("Words for " + sentance.text);
		while (reader.ReadToFollowing ("word")) {
			Word word = new Word ();
			word.next = Convert.ToInt32("0" + reader.GetAttribute("next"));
			word.word = reader.ReadElementContentAsString ();
			Debug.Log ("Adding word:" + word.word + " next:" + word.next);
			sentance.words.Add (word);
		}
	}

	private void ParseSentance(XmlReader reader, int index) 
	{
		Sentance sentance = new Sentance ();
		while (reader.Read ()) {
			if (reader.NodeType == XmlNodeType.Element) {
				if (reader.Name.Equals ("text")) {
					sentance.text = reader.ReadElementContentAsString ();
				}
				if (reader.Name.Equals ("options")) {
					ParseWords(reader.ReadSubtree (), sentance);
				}
			}
		}
		Debug.Log("Adding sentance:" + sentance.text + " at:" + index);
		sentances.Add (index, sentance);

	}

	public class Sentance 
	{
		public string text;
		public ArrayList words = new ArrayList ();
		public string keyWord;
		public string display;
	}

	public class Word
	{
		public string word;
		public int next;
	}
}
