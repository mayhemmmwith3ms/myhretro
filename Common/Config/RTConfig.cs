using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace myhretro.Common.Config
{
	public class RTConfig : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ClientSide;

		[Header("Main")]

		[DefaultValue(true)]
		public bool EffectActive { get; set; }

		[DefaultValue(true)]
		public bool ColourFade { get; set; }

		[DefaultValue(false)]
		public bool OldDarkness { get; set; }

		[DefaultValue(0)]
		public int ForcePalette { get; set; }

		[DefaultValue(4)]
		[Range(2, 256)]
		public int ColourDepth { get; set; }

		[Range(0, 1)]
		[DefaultValue(0.25f)]
		[Slider()]
		public float DitherSpread { get; set; }

		[Header("Debug")]

		[DefaultValue(false)]
		public bool DebugMode { get; set; }

	}
}
