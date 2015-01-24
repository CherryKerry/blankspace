using UnityEngine;
using System.Collections;

public class CloudModel : EnvironmentModel
{
	public class CloudColor : EnvironmentColor
	{
		public CloudColor() 
		{
			ColorBright = new Color (255 / 255.0f, 255 / 255.0f, 255 / 255.0f);
			ColorDark  =  new Color  (200 / 255.0f, 200 / 255.0f, 200 / 255.0f);
			ColorRainy = new Color (120 / 255.0f, 120 / 255.0f, 120 / 255.0f);
		}
	}

	public override EnvironmentColor SetColor()
	{
		return new CloudColor();
	}

}
