using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace myhretro.Common.Systems
{
	public class DebugSystem : ModSystem
	{
		bool lastVKey;
		public static bool debugShaderActive;

		public override void PostUpdateInput()
		{
			bool vKey = false;
			Keys[] pressedKeys = Main.keyState.GetPressedKeys();
			for (int j = 0; j < pressedKeys.Length; j++)
			{
				string keyPress = string.Concat(pressedKeys[j]);
				vKey = keyPress == "V";
			}
			//if (vKey != lastVKey && vKey) debugShaderActive = !debugShaderActive;
			lastVKey = vKey;

			if (debugShaderActive)
			{
				Texture2D replace = ModContent.Request<Texture2D>("myhretro/Assets/Textures/ReplacePalettes/replacePalette1").Value;
				Filters.Scene["myh1bit:ColorReplace"].GetShader().UseImage(replace, 0, SamplerState.PointClamp);
				Filters.Scene["myh1bit:ColorReplace"].GetShader().UseImage(replace, 1, SamplerState.PointClamp);
				Filters.Scene.Activate("myh1bit:ColorReplace");

				Filters.Scene["myh1bit:ColorCompress"].GetShader().UseIntensity(4);
				Filters.Scene.Activate("myh1bit:ColorCompress");

				Filters.Scene["myh1bit:Dither"].GetShader().UseIntensity(0.25f);
				Filters.Scene.Activate("myh1bit:Dither");
				//Filters.Scene["myh1bit:PixelPerfect"].GetShader().UseIntensity(0.3f);
				//Main.NewText((float)Math.Sin(Main.GameUpdateCount * -0.01f));
				//Filters.Scene.Activate("myh1bit:PixelPerfect");
			}
			else
			{
				Filters.Scene.Deactivate("myh1bit:PixelPerfect");
				Filters.Scene.Deactivate("myh1bit:ColorReplace");
				Filters.Scene.Deactivate("myh1bit:ColorCompress");
				Filters.Scene.Deactivate("myh1bit:Dither");
			}
		}
	}
}
