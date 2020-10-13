using ArcaneAlchemist.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;

namespace ArcaneAlchemist.Items.Sets.Splash
{
	public class SplashBar : ModItem
	{
		public override void SetStaticDefaults()
		{
			ItemID.Sets.SortingPriorityMaterials[item.type] = 61; // influences the inventory sort order. 59 is PlatinumBar, higher is more valuable.
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 24;
			item.maxStack = 99;
			item.value = 7500;
			item.useStyle = 1;
            item.useTime = 10;
            item.useAnimation = 10;
            item.autoReuse = true;
			item.consumable = true;
            item.createTile = TileType<SplashBarT>();
			item.placeStyle = 0;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<SplashGem>(), 5);
            recipe.AddTile(TileID.Solidifier);
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }

    public class SplashBarT : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileShine[Type] = 1100;
            Main.tileSolid[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;
            soundType = SoundID.Splash;
            dustType = DustType<SplashDust1>();

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.addTile(Type);

                AddMapEntry(new Color(255, 196, 0), Language.GetText("MapObject.SplashBar"));
            }

            public override bool Drop(int i, int j)
            {
                Tile t = Main.tile[i, j];
                int style = t.frameX / 18;
                if (style == 0) // It can be useful to share a single tile with multiple styles. This code will let you drop the appropriate bar if you had multiple.
                {
                    Item.NewItem(i * 16, j * 16, 16, 16, ItemType<Items.Sets.Splash.SplashBar>());
                }
                return base.Drop(i, j);
            }
        }
}
