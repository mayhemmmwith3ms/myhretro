using Microsoft.Xna.Framework;
using myhretro.Common.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace myhretro.Common.Players
{
	public class CameraPosPlayer : ModPlayer
	{
		public override void ModifyScreenPosition()
		{
			if (RetroShaderManager.EffectActive)
			{
				Main.screenPosition.X = (float)Math.Floor(Main.screenPosition.X / 2) * 2 + 1;
				Main.screenPosition.Y = (float)Math.Floor(Main.screenPosition.Y / 2) * 2 + 1;
			}
		}

		public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
		{
			if (RetroShaderManager.EffectActive)
			{
				drawInfo.Position.X = (float)Math.Floor(drawInfo.Position.X / 2) * 2;
				drawInfo.Position.Y = (float)Math.Floor(drawInfo.Position.Y / 2) * 2;
			}
		}
	}
}
