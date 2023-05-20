using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria;
using Terraria.ModLoader;

namespace myhretro.Common.Systems
{
	public class ShaderLoader : ModSystem
	{
		public override void Load()
		{
			LoadScreenShader("ColorReplace", "myhretro/Assets/Effects/ColorReplaceShader", "ColorReplace", EffectPriority.High);
			LoadScreenShader("Dither", "myhretro/Assets/Effects/DitherShader", "DitherShader", EffectPriority.High);
			LoadScreenShader("ColorCompress", "myhretro/Assets/Effects/ColorCompressShader", "ColorCompressShader", EffectPriority.High);
		}

		private static void LoadScreenShader(string name, string path, string passName, EffectPriority priority)
		{
			Ref<Effect> screenDesatRef = new Ref<Effect>(ModContent.Request<Effect>(path, AssetRequestMode.ImmediateLoad).Value);
			Filters.Scene["myh1bit:" + name] = new Filter(new ScreenShaderData(screenDesatRef, passName), priority);
			Filters.Scene["myh1bit:" + name].Load();
		}
	}
}
