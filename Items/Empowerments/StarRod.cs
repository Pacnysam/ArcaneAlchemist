using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ArcaneAlchemist.Items.Empowerments
{
	public class StarRod : AlchemistItem
	{
		public override void SetStaticDefaults() {
            Tooltip.SetDefault("Gives all arcane weapons lifesteal and Makes a star shoot to damage enemies \n''It came from the light of the stars''");
        }

		public override void SafeSetDefaults() 
		{
            item.damage = 24;
            item.width = 42;
            item.height = 42;
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
            recipe.AddIngredient(ItemID.FallenStar, 5);
            recipe.AddIngredient(ItemID.SunplateBlock, 10);
            recipe.AddTile(TileID.SkyMill);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }

    public class Star : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Star");
        }

        public override void SetDefaults()
        {
            projectile.arrow = false;
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = true;
            projectile.aiStyle = 5;
            projectile.penetrate = 3;

            projectile.GetGlobalProjectile<AlchemistProjectile>().arcane = true;
        }
        public override void AI() 
        {
            Dust.NewDustPerfect(projectile.position, DustType<Dusts.RisingStar>());

            Lighting.AddLight(projectile.Center, 0.15f, 0.15f, 0.15f);
        }
    }
}