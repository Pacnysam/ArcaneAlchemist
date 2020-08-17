using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ArcaneAlchemist.Items.Empowerments
{
	public class GlowingRiver : AlchemistItem
	{
		public override void SetStaticDefaults() {
            Tooltip.SetDefault("Greatly increases arcane damage \n''It came from the light of the stars''");
        }

		public override void SafeSetDefaults() 
		{
            item.damage = 24;
            item.width = 32;
            item.height = 32;
            item.shoot = ProjectileType<Star>();
            item.shootSpeed = 16f;
            item.buffType = BuffType<Buffs.RisingStar>();
            item.buffTime = 300;
            item.useStyle = 4;
            item.UseSound = SoundID.Item4;
            item.useTime = 20;
			item.useAnimation = 20;
            item.mana = 60;
            item.noMelee = true;
            item.knockBack = 0;
            item.value = 5000;
			item.rare = 2;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GoldBar, 3);
            recipe.AddIngredient(ItemType<LifelessPearl>(), 1);
            recipe.AddTile(TileID.WaterCandle);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}