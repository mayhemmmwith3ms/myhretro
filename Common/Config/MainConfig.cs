using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace myhretro.Common.Config
{
	public class MainConfig : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ClientSide;

		[Header("[i:5061] [c/ffeb6e:Main]")]

		[DefaultValue(true)]
		[Label("Retro Effect")]
		[Tooltip("Toggles the retro effect in its entirety")]
		public bool EffectActive { get; set; }

		[DefaultValue(true)]
		[Label("Fade Between Palettes")]
		[Tooltip("Makes the transition between different palettes less jarring by fading between them")]
		public bool ColourFade { get; set; }

		[DefaultValue(false)]
		[Label("Old Broken Formula")]
		[Tooltip("Uses the old (incorrect) colour compression formula because I thought it looked cool\nMakes the game extremely dark")]
		public bool OldDarkness { get; set; }

		[DefaultValue(0)]
		[Label("Force Palette")]
		[Tooltip("Forces the effect to use a certain palette regardless of biome/events\nSet to 0 to disable forcing a palette")]
		public int ForcePalette { get; set; }

		[DefaultValue(4)]
		[Label("Colour Depth (WIP)")]
		[Range(2, 256)]
		[Tooltip("The number of different colours used for the effect\nCurrently does not work properly for more than 4 colours")]
		public int ColourDepth { get; set; }

		[Label("Dither Spread")]
		[Tooltip("The amount the dither spreads colours\nSet to 0 to disable dithering\nValues over (1.0 / ColourDepth) may cause the dither stages to overlap and look weird")]
		[Range(0, 1)]
		[DefaultValue(0.25f)]
		[Slider()]
		public float DitherSpread { get; set; }

		[Header("[i:4818] [c/ffeb6e:Debug]")]

		[DefaultValue(false)]
		[Label("Debug")]
		[Tooltip("Debug mode, intended for developers")]
		public bool DebugMode { get; set; }

	}
}
