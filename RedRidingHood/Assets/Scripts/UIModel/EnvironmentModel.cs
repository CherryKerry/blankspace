using UnityEngine;
using System.Collections;

public class EnvironmentModel : MonoBehaviour
{
	public static Constants.EnvironmentColor color = new Constants.EnvironmentColor();
	private static Color from;
	private static Color to;
	private float processTime = color.ColorProcessTime;
	//private SpriteRenderer renderer;

	void Start ()
	{
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
		if (keyValue == color.Keyword) {
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
