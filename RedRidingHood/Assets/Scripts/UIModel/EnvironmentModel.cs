using UnityEngine;
using System.Collections;

public abstract class EnvironmentModel : MonoBehaviour
{
	public class EnvironmentColor
	{
		public int ChangeSentence = 3;
		public string Keyword = "[sky_colour]";
		public string Bright = "bright";
		public string Dark = "dark";
		public string Rainy = "rainy";
		public Color ColorBright = new Color (255 / 255.0f, 255 / 255.0f, 255 / 255.0f);
		public Color ColorDark = new Color (200 / 255.0f, 75 / 255.0f, 75 / 255.0f);
		public Color ColorRainy = new Color (201 / 255.0f, 184 / 255.0f, 227 / 255.0f);
		public float ColorProcessTime = 1.0f;
	}

	public EnvironmentColor color;
	private Color from;
	private Color to;
	private float processTime;
	//private SpriteRenderer renderer;

	public abstract EnvironmentColor SetColor ();

	void Start ()
	{
		color = SetColor ();
		processTime = color.ColorProcessTime;
		//Debug.LogError("Env started");
		Manager.OnEvent += Manager_OnEvent;
	}
	
	void OnDisable()
	{
		//Debug.LogError("Stopped");
		Manager.OnEvent -= Manager_OnEvent;
	}

	void Update()
	{
		if (processTime < color.ColorProcessTime) {
			//Debug.Log("Lerping");
			((SpriteRenderer)renderer).color = Color.Lerp(from, to, processTime/color.ColorProcessTime);
			processTime += Time.deltaTime;
		}
	}
	
	public void Manager_OnEvent(string keyValue, string word) 
	{
		//Debug.LogError("Event");
		if (keyValue == color.Keyword && word != null) {
			from = ((SpriteRenderer)renderer).color;
			if (word == color.Bright) {
				to = color.ColorBright;
			}
			if (word == color.Dark) {
				to = color.ColorDark;
			}
			if (word == color.Rainy) {
				to = color.ColorRainy;
			}
			processTime = 0;
		}
	}
}
