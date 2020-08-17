using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using ArcaneAlchemist.Buffs;
using ArcaneAlchemist.Projectiles;

namespace ArcaneAlchemist.Items.Empowerments
{
	public class IlluminantLake : AlchemistItem
	{
		public override void SetStaticDefaults() {
            Tooltip.SetDefault("Right Click gives all arcane weapons lifesteal \nLeft Click greatly increases arcane damage \n''The pearl shines brightly.''");
        }

		public override void SafeSetDefaults() 
		{
            item.width = 44;
            item.height = 44;
            item.useStyle = 4;
            item.UseSound = SoundID.Item4;
            item.useTime = 20;
			item.useAnimation = 20;
            item.mana = 200;
            item.noMelee = true;
            item.knockBack = 0;
            item.value = 5000;
			item.rare = 4;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<GlowingRiver>(), 1);
            recipe.AddIngredient(ItemType<StarRod>(), 1);
            recipe.AddTile(TileID.WaterCandle);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.buffType = BuffType<RisingStar>();
                item.shootSpeed = 16;
                item.damage = 42;
                item.buffTime = 600;
                item.shoot = ProjectileType<Star>();
            }
            else
            {
                item.buffType = BuffType<FallingThunder>();
                item.shootSpeed = 2.5f;
                item.damage = 22;
                item.buffTime = 600;
                item.shoot = ProjectileType<FallingThunderP>();
            }
            return base.CanUseItem(player);
        }
    }
}