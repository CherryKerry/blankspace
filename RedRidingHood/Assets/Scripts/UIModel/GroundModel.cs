using UnityEngine;
using System.Collections;

public class GroundModel : EnvironmentModel
{
	public class GroundColor : EnvironmentColor
	{
		public GroundColor() 
		{
			ColorBright = new Color (255 / 255.0f, 255 / 255.0f, 255 / 255.0f);
			ColorDark  =  new Color  (180 / 255.0f, 180 / 255.0f, 180 / 255.0f);
			ColorRainy = new Color (205 / 255.0f, 255 / 255.0f, 255 / 255.0f);
		}
	}

	public override EnvironmentColor SetColor()
	{
		return new GroundColor();
	}
}

