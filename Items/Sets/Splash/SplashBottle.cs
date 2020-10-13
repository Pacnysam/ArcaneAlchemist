using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using ArcaneAlchemist.Dusts;
using Microsoft.Xna.Framework;
using ArcaneAlchemist.Items.Flasks;

namespace ArcaneAlchemist.Items.Sets.Splash
{
    internal class SplashBottle : AlchemistItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Splash Bottle");
        }

        public override void SafeSetDefaults() 
		{
            item.damage = 11;
            item.width = 28;
            item.height = 28;
            item.useTime = 28;
            item.useAnimation = 28;
            item.knockBack = 3;
            item.value = 8500;
            item.maxStack = 1;
            item.rare = 2;
            item.shoot = ProjectileType<SplashBottleP>();
            item.shootSpeed = 10;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<BasicFlask>(), 1);
            recipe.AddIngredient(ItemType<SplashGem>(), 8);
            recipe.AddTile(TileID.Solidifier);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }

    public class SplashBottleP : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Splash Bottle");
        }

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.penetrate = 1;
        }
        public override void AI()
        {
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return false;
        }

        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(projectile.position, (projectile.velocity * 0), ProjectileType<SplashBomb>(), (int)(projectile.damage * 0.5), 0f, projectile.owner, 0f, 0f);
        }
    }

    public class SplashBomb : ModProjectile
    {
        int FrameCount = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SplashBomb");
            Main.projFrames[projectile.type] = 8;
        }
        public override void SetDefaults()
        {
            projectile.width = 48;
            projectile.height = 48;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 24;
            projectile.tileCollide = false;
        }

        public override void AI()
        {
            FrameCount++;

            if (Main.rand.NextFloat() < 0.3f)
            {
                Projectile.NewProjectile(projectile.position + new Vector2(Main.rand.Next(-100, 101), -Main.screenHeight * 0.8f), (projectile.velocity * 0), ProjectileType<Splash>(), (int)(projectile.damage * 2), 0f, projectile.owner, 0f, 0f);
            }
            
            if (FrameCount >= 3)
            {
                projectile.frame++;
                FrameCount = 0;
                if (projectile.frame > 7)
                {
                    projectile.frame = 0;
                }
            }
        }
    }

    public class Splash : ModProjectile
    {
        public override string Texture => "ArcaneAlchemist/BlankTexture";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Splash");
        }

        public override void SetDefaults()
        {
            projectile.friendly = true;
            projectile.timeLeft = 120;
            projectile.width = 12;
            projectile.height = 12;
            projectile.penetrate = 1;
            projectile.ignoreWater = true;
            projectile.alpha = 50;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
        }

        public override void AI()
        {
            projectile.ai[1]++;

            if (projectile.ai[1] <= 2) 
            {
                projectile.velocity.Y = (Main.rand.Next(12, 30));
                projectile.velocity.X = Main.rand.Next(-3, 3);
            }

            Dust dust;
            dust = Terraria.Dust.NewDustPerfect(projectile.position, DustType<SplashDust1>(), new Vector2(0f, 0f), 0, new Color(150, 150, 150), 0.6f);
            dust.noLight = true;
        }
    }
}