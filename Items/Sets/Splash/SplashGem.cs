using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using static Terraria.ModLoader.ModContent;
using ArcaneAlchemist.Dusts;
using Terraria.ID;

namespace ArcaneAlchemist.Items.Sets.Splash
{
	public class SplashGem : ModItem
	{
		public override void SetStaticDefaults() {
            Tooltip.SetDefault("Made from seafoam.");
        }

		public override void SetDefaults() 
		{
            item.width = 18;
			item.height = 16;
            item.value = 500;
			item.rare = ItemRarityID.Blue;
            item.maxStack = 999;
        }
	}
}