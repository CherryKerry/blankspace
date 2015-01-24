using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public static class Constants {

    public class HoodColour
    {
        public const string Keyword = "[hood_colour]";
        public const string Red = "Red";
        public const string Blue = "Blue";
        public const string Green = "Green";
        public const string HotLink = "Pink";
    }

	public class EnvironmentColor
	{
		public string Keyword = "[sky_colour]";
		public string Bright = "bright";
		public string Dark = "dark";
		public string Rainy = "rainy";
		public Color ColorBright = new Color (255 / 255.0f, 255 / 255.0f, 255 / 255.0f);
		public Color ColorDark = new Color (200 / 255.0f, 75 / 255.0f, 75 / 255.0f);
		public Color ColorRainy = new Color (201 / 255.0f, 184 / 255.0f, 227 / 255.0f);
		public float ColorProcessTime = 1.0f;
	}

	public class TreeColor : EnvironmentColor
	{
		new public Color ColorBright = new Color (255 / 255.0f, 255 / 255.0f, 255 / 255.0f);
		new public Color ColorDark  =  new Color  (158 / 255.0f, 118 / 255.0f, 118 / 255.0f);
		new public Color ColorRainy = new Color (126 / 255.0f, 145 / 255.0f, 238 / 255.0f);
	}

	public class GroundColor : EnvironmentColor
	{
		new public Color ColorBright = new Color (255 / 255.0f, 255 / 255.0f, 255 / 255.0f);
		new public Color ColorDark  =  new Color  (180 / 255.0f, 180 / 255.0f, 180 / 255.0f);
		new public Color ColorRainy = new Color (205 / 255.0f, 255 / 255.0f, 255 / 255.0f);
	}

	public class CloudColor : EnvironmentColor
	{
		new public Color ColorBright = new Color (255 / 255.0f, 255 / 255.0f, 255 / 255.0f);
		new public Color ColorDark = new Color (200 / 255.0f, 75 / 255.0f, 75 / 255.0f);
		new public Color ColorRainy = new Color (201 / 255.0f, 184 / 255.0f, 227 / 255.0f);
	}
}
