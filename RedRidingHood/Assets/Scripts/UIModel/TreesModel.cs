using UnityEngine;
using System.Collections;

public class ForestModel : MonoBehaviour
{
	static Color light = new Color (255 / 255.0f, 255 / 255.0f, 255 / 255.0f);
	static Color dark  =  new Color  (158 / 255.0f, 118 / 255.0f, 118 / 255.0f);
	static Color rainy = new Color (126 / 255.0f, 145 / 255.0f, 238 / 255.0f);


	// Use this for initialization
	void Start ()
	{
		Manager.OnEvent += this.OnEvent;
	}

	// Update is called once per frame
	void Update ()
	{

	}

	public void OnEvent(string keyValue, string word) 
	{
		if (keyValue.Equals ("[sky_colour]")) {
			ChangeColor(word);
		}
	}
	
	void ChangeColor(string word) 
	{
		SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> ();
		if (word.Equals("light")) {
			renderer.color = light;
		} 
		if (word.Equals("dark")){
			renderer.color = dark;
		}
		if (word.Equals("rainy")){
			renderer.color = rainy;
		}
	}
}

