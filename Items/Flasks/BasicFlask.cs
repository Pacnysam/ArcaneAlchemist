using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using ArcaneAlchemist.Projectiles;
using Terraria;
using ArcaneAlchemist.Dusts;
using Microsoft.Xna.Framework;

namespace ArcaneAlchemist.Items.Flasks
{
    internal class BasicFlask : Flask
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bubble Flask");
        }

        public virtual void SafeSetDefaults() 
		{
            item.damage = 10;
            item.width = 18;
            item.height = 24;
            item.useTime = 16;
            item.useAnimation = 16;
            item.knockBack = 3;
            item.value = 1000;
            item.maxStack = 1;
            item.rare = ItemRarityID.White;
            item.UseSound = SoundID.Item1;
            item.shoot = ProjectileType<BasicFlaskP>();
            item.shootSpeed = 8f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BottledWater, 2);
            recipe.AddIngredient(ItemID.Gel, 6);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }

    internal class BasicFlaskP : FlaskProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bubble Flask");
        }

        public virtual void SafeSetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.penetrate = 1;
        }
        public virtual void AI()
        {
        }

        public virtual void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return false;
        }

        public virtual void Kill(int timeLeft)
        {
            for (float k = 0; k < 6.28f; k += 0.25f)
                Dust.NewDustPerfect(projectile.position, DustType<BasicDust>(), Vector2.One.RotatedBy(k) * 2);
        }
    }
}