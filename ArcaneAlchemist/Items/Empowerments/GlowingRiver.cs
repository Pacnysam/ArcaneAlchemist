using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using ArcaneAlchemist.Buffs;

namespace ArcaneAlchemist.Items.Empowerments
{
	public class GlowingRiver : AlchemistItem
	{
		public override void SetStaticDefaults() {
            Tooltip.SetDefault("Right Click gives all arcane weapons lifesteal \nLeft Click greatly increases arcane damage \n''The pearl shines brightly.''");
        }

		public override void SafeSetDefaults() 
		{
            item.damage = 0;
            item.width = 32;
            item.height = 32;
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
            recipe.AddIngredient(ItemID.GoldBar, 3);
            recipe.AddIngredient(ItemType<LifelessPearl>(), 1);
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
                item.buffTime = 600;
            }
            else
            {
                item.buffType = BuffType<FallingThunder>();
                item.buffTime = 600;
            }
            return base.CanUseItem(player);
        }
    }
}