using UnityEngine;
using System.Collections;

public class SkyModel : EnvironmentModel
{
	private bool skyChanging = true;

	public override EnvironmentColor SetColor()
	{
		Manager.OnEvent += Manager_OnClickSky;
		return new EnvironmentColor();
	}

	void OnMouseDown()
	{
		if (!skyChanging) {
			skyChanging = true;
			Manager.ResetKeyWord(color.Keyword);
			Manager.SetInterruptSentance (color.ChangeSentence);
		}
	}

	public void Manager_OnClickSky(string keyValue, string word) 
	{
		skyChanging = false;
	}
}

