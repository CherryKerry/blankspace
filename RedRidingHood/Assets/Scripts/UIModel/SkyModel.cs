using UnityEngine;
using System.Collections;

public class SkyModel : EnvironmentModel
{
	private int skyChanging = 3;

	public override EnvironmentColor SetColor()
	{
		Manager.OnEvent += Manager_OnClickSky;
		return new EnvironmentColor();
	}

	void OnMouseDown()
	{
		if (skyChanging == 0) {
			Manager.ResetKeyWord(color.Keyword);
			Manager.SetInterruptSentance (color.ChangeSentence);
			skyChanging = 3;
		}
	}

	public void Manager_OnClickSky(string keyValue, string word) 
	{	
		if (skyChanging > 0) {
				skyChanging--;
		}
	}
}

