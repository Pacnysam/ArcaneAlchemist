using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using static Terraria.ModLoader.ModContent;
using ArcaneAlchemist.Dusts;
using Terraria.ID;

namespace ArcaneAlchemist.Items.Placeable
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
			item.rare = 1;
            item.maxStack = 999;
            item.useStyle = 1;
            item.useTime = 10;
            item.useAnimation = 10;
            item.autoReuse = true;
            item.consumable = true;
            item.createTile = TileType<SplashGemT>();
            item.placeStyle = 0;
        }
	}

    public class SplashGemT : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileLighted[Type] = true;
            soundType = SoundID.Splash;
            dustType = DustType<SplashDust1>();
            drop = ItemType<SplashGem>();
            AddMapEntry(new Color(65, 178, 198));
            // Set other values here
        }
    }
}