using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using ArcaneAlchemist.Projectiles;
using Terraria;
using ArcaneAlchemist.Dusts;
using Microsoft.Xna.Framework;

namespace ArcaneAlchemist.Items.Flasks
{
	public class SplashBottle : AlchemistItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Splash Bottle");
        }

        public override void SafeSetDefaults() 
		{
            item.damage = 13;
            item.width = 28;
            item.height = 28;
            item.useTime = 28;
            item.useAnimation = 28;
            item.useStyle = 1;
            item.knockBack = 3;
            item.value = 8500;
            item.maxStack = 1;
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.shoot = ProjectileType<SplashBottleP>();
            item.shootSpeed = 10;
            item.noUseGraphic = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<BasicFlask>(), 1);
            recipe.AddIngredient(ItemID.BottledWater, 8);
            recipe.AddIngredient(ItemID.Gel, 6);
            recipe.AddTile(TileID.Bottles);
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
            projectile.arrow = false;
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = true;
            projectile.aiStyle = 2;
            projectile.penetrate = 1;
        }
        public override void AI()
        {
            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.FallingThunder>()) && projectile.owner == Main.myPlayer)
            {
                if (Main.rand.NextFloat() < 0.3f)
                {
                    Dust dust;
                    Vector2 position = Main.LocalPlayer.Center;
                    dust = Terraria.Dust.NewDustDirect(projectile.position, projectile.width / 2, projectile.height / 2, 276, 0f, -2f, 0, new Color(255, 255, 255), 1f);
                    dust.noLight = true;
                }
            }

            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.RisingStar>()) && projectile.owner == Main.myPlayer)
            {
                Dust dust;
                dust = Terraria.Dust.NewDustPerfect(projectile.position, 277, new Vector2(0f, 0f), 0, new Color(255, 255, 255), 1f);
                dust.noLight = true;

            }
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
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 107);
            Gore.NewGore(projectile.position, -projectile.oldVelocity * 0.2f, 704, 1f);
            Gore.NewGore(projectile.position, -projectile.oldVelocity * 0.2f, 705, 1f);

            Projectile.NewProjectile(projectile.position, (projectile.velocity * 0), ProjectileType<SplashBomb>(), (int)(projectile.damage*0.6), 0f, projectile.owner, 0f, 0f);

        }
    }

    public class SplashBomb : ModProjectile
    {
        int FrameCountMeter = 0;
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
            FrameCountMeter++;

            if (Main.rand.NextFloat() < 0.3f)
            {
                
            }
            Projectile.NewProjectile(projectile.position + new Vector2(Main.rand.Next(-100, 101), -Main.screenHeight * 0.8f), (projectile.velocity * 0), ProjectileType<Splash>(), (int)(projectile.damage * 0.8), 0f, projectile.owner, 0f, 0f);
            
            if (FrameCountMeter >= 3)
            {
                projectile.frame++;
                FrameCountMeter = 0;
                if (projectile.frame > 7)
                {
                    projectile.frame = 0;
                }
            }
        }
    }

    public class Splash : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Splash");
        }

        public override void SetDefaults()
        {
            projectile.friendly = true;
            projectile.melee = true;
            projectile.timeLeft = 120;
            projectile.width = 10;
            projectile.height = 10;
            projectile.penetrate = 1;
            projectile.ignoreWater = true;
            projectile.alpha = 50;
        }

        public override void AI()
        {
            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.RisingStar>()) && Main.rand.NextFloat() < 0.001f)
            {
                Projectile.NewProjectile(projectile.position, (projectile.velocity * 0), ProjectileType<RisingStarP>(), (int)(projectile.damage), 0f, projectile.owner, 0f, 0f);
            }

            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.FallingThunder>()) && Main.rand.NextFloat() < 0.001f)
            {
                Projectile.NewProjectile(projectile.position, (projectile.velocity * 0), ProjectileType<FallingThunderP>(), (int)(projectile.damage), 0f, projectile.owner, 0f, 0f);
            }

            projectile.velocity.Y = (Main.rand.Next(6, 30));
            projectile.velocity.X = Main.rand.Next(-10, 10);

            Dust dust;
            dust = Terraria.Dust.NewDustPerfect(projectile.position, DustType<SplashDust1>(), new Vector2(0f, 0f), 0, new Color(150, 150, 150), 0.6f);
            dust.noLight = true;
        }
    }
}