using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using myhretro.Common.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using XPT.Core.Audio.MP3Sharp.Decoding.Decoders.LayerI;

namespace myhretro.Common.Systems
{
	public class RetroShaderManager : ModSystem
	{
		static RTConfig MConfig => ModContent.GetInstance<RTConfig>();
		static Player Plr => Main.player[Main.myPlayer];
		public static bool EffectActive => (MConfig.DebugMode && debugEffectActive) || (!MConfig.DebugMode && MConfig.EffectActive);

		bool lastVKey;
		public static bool debugEffectActive;

		public static int currentReplacePalette = 1;
		public static int lastReplacePalette = 1;
		public static int fadeReplacePalette = 1;
		public static int currentPriority = -1;

		public static float paletteFade = 0;

		public override void PreUpdateInvasions()
		{
			if (MConfig.DebugMode)
			{
				bool vKey = false;
				Keys[] pressedKeys = Main.keyState.GetPressedKeys();
				for (int j = 0; j < pressedKeys.Length; j++)
				{
					string keyPress = string.Concat(pressedKeys[j]);
					vKey = keyPress == "V";
				}
				if (vKey != lastVKey && vKey)
				{
					debugEffectActive = !debugEffectActive;
					Main.NewText("Debug Effect Toggle: " + debugEffectActive);
				}
				lastVKey = vKey;
			}

			if (EffectActive && Plr.active)
			{
				if (MConfig.ForcePalette == 0 || MConfig.ForcePalette > 29)
				{
					if (Plr.ZonePurity) SetReplacePalette(1, 0);
					if (Plr.ZoneDesert) SetReplacePalette(2, 3);
					if (Plr.ZoneSnow) SetReplacePalette(3, 3);
					if (Plr.ZoneCorrupt) SetReplacePalette(4, 10);
					if (Plr.ZoneJungle) SetReplacePalette(5, 3);
					if (Plr.ZoneBeach) SetReplacePalette(6, 0);
					if (Plr.ZonePurity && !Main.dayTime) SetReplacePalette(7, 1);
					if (Plr.ZoneNormalUnderground || Plr.ZoneRockLayerHeight) SetReplacePalette(8, 2);
					if (Plr.ZoneUnderworldHeight) SetReplacePalette(9, 100);
					if (Plr.ZoneShimmer || Main.SceneMetrics.ShimmerMonolithState == 1) SetReplacePalette(10, 99);
					if (Plr.ZoneCrimson) SetReplacePalette(11, 10);
					if (Main.bloodMoon || Main.SceneMetrics.BloodMoonMonolith) SetReplacePalette(12, 98);
					if (Plr.ZoneDungeon) SetReplacePalette(13, 97);
					if (Plr.ZoneGlowshroom) SetReplacePalette(14, 3);
					if (Plr.ZoneLihzhardTemple) SetReplacePalette(15, 97);
					if (Plr.ZoneSkyHeight) SetReplacePalette(16, 5);
					if (Plr.ZoneHallow && Plr.ZoneOverworldHeight) SetReplacePalette(17, 11);
					if (Plr.ZoneHallow && !Plr.ZoneOverworldHeight) SetReplacePalette(18, 11);
					if (Plr.ZoneHallow && Plr.ZoneSnow) SetReplacePalette(19, 12);
					if (Plr.ZoneTowerSolar || Plr.solarMonolithShader || Main.SceneMetrics.ActiveMonolithType == 3) SetReplacePalette(20, 101);
					if (Plr.ZoneTowerNebula || Plr.nebulaMonolithShader || Main.SceneMetrics.ActiveMonolithType == 1) SetReplacePalette(21, 101);
					if (Plr.ZoneTowerStardust || Plr.stardustMonolithShader || Main.SceneMetrics.ActiveMonolithType == 2) SetReplacePalette(22, 101);
					if (Plr.ZoneTowerVortex || Plr.vortexMonolithShader || Main.SceneMetrics.ActiveMonolithType == 0) SetReplacePalette(23, 101);
					if (Main.eclipse) SetReplacePalette(24, 98);
					if (Main.pumpkinMoon) SetReplacePalette(26, 99);
					if (Main.snowMoon) SetReplacePalette(27, 99);
					if (Plr.ZoneGraveyard) SetReplacePalette(8, 50);
					if (Plr.ZoneGranite) SetReplacePalette(28, 4);
					if (Plr.ZoneMarble) SetReplacePalette(29, 4);

					bool moonLord = false;
					for (int i = 0; i < Main.maxNPCs; i++)
					{
						if (Main.npc[i].type == NPCID.MoonLordCore)
						{
							moonLord = true;
							break;
						}
					}
					if (moonLord || Plr.moonLordMonolithShader || Main.SceneMetrics.ActiveMonolithType == 4) SetReplacePalette(25, 200);
				}
				else
				{
					currentReplacePalette = MConfig.ForcePalette;
				}

				if (MConfig.ColourFade)
				{
					if (currentReplacePalette != lastReplacePalette)
					{
						fadeReplacePalette = lastReplacePalette;
						paletteFade = 1;
					}

					lastReplacePalette = currentReplacePalette;

					if (paletteFade > 0) paletteFade -= 0.05f;
				}

				Texture2D replace = ModContent.Request<Texture2D>("myhretro/Assets/Textures/ReplacePalettes/replacePalette" + currentReplacePalette).Value;
				Texture2D fade = ModContent.Request<Texture2D>("myhretro/Assets/Textures/ReplacePalettes/replacePalette" + fadeReplacePalette).Value;

				Filters.Scene["myh1bit:ColorReplace"].GetShader().UseImage(replace, 0, SamplerState.PointClamp);
				if (MConfig.ColourFade)
				{
					Filters.Scene["myh1bit:ColorReplace"].GetShader().UseImage(fade, 1, SamplerState.PointClamp);
					Filters.Scene["myh1bit:ColorReplace"].GetShader().UseIntensity(paletteFade);
				}
				Filters.Scene.Activate("myh1bit:ColorReplace");

				Filters.Scene["myh1bit:ColorCompress"].GetShader().UseIntensity(MConfig.ColourDepth);
				Filters.Scene["myh1bit:ColorCompress"].GetShader().Shader.Parameters["veryDark"].SetValue(MConfig.OldDarkness);
				Filters.Scene.Activate("myh1bit:ColorCompress");

				Filters.Scene["myh1bit:Dither"].GetShader().UseIntensity(MConfig.DitherSpread);
				Filters.Scene.Activate("myh1bit:Dither");
			}
			else
			{
				Filters.Scene.Deactivate("myh1bit:ColorReplace");
				Filters.Scene.Deactivate("myh1bit:ColorCompress");
				Filters.Scene.Deactivate("myh1bit:Dither");
			}

			currentReplacePalette = 1;
			currentPriority = -1;
		}

		public static void SetReplacePalette(int paletteID, int priority)
		{
			if (priority > currentPriority)
			{
				currentReplacePalette = paletteID;
				currentPriority = priority;
			}
		}
	}
}
