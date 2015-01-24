using UnityEngine;
using System.Collections;

public class SkyModel : EnvironmentModel
{
	public override EnvironmentColor SetColor()
	{
		return new EnvironmentColor();
	}

	void OnMouseDown()
	{
		Debug.LogError ("Mouse toucked the sky");
	}
}

