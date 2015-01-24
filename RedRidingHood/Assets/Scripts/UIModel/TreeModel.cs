using UnityEngine;
using System.Collections;

public class TreeModel : EnvironmentModel
{
	public class TreeColor : EnvironmentColor
	{
		public TreeColor() 
		{
			ColorBright = new Color (255 / 255.0f, 255 / 255.0f, 255 / 255.0f);
			ColorDark  =  new Color  (158 / 255.0f, 118 / 255.0f, 118 / 255.0f);
			ColorRainy = new Color (126 / 255.0f, 145 / 255.0f, 238 / 255.0f);
		}
	}

	public override EnvironmentColor SetColor()
	{
		return new TreeColor();
	}
}
