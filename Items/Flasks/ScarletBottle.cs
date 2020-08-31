using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using ArcaneAlchemist.Projectiles;
using Terraria;
using ArcaneAlchemist.Buffs;
using Microsoft.Xna.Framework;

namespace ArcaneAlchemist.Items.Flasks
{
	public class ScarletBottle : AlchemistItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scarlet Flask");
            Tooltip.SetDefault("Bursts into flames");
        }

        public override void SafeSetDefaults() 
		{
            item.damage = 8;
            item.width = 18;
            item.height = 24;
            item.useTime = 50;
            item.useAnimation = 50;
            item.useStyle = 1;
            item.knockBack = 3;
            item.value = 5000;
            item.maxStack = 1;
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.shoot = ProjectileType<ScarletBottleP>();
            item.shootSpeed = 13f;
            item.noUseGraphic = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<BasicFlask>(), 1);
            recipe.AddIngredient(ItemID.Gel, 12);
            recipe.AddIngredient(ItemID.Torch, 8);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }

    public class ScarletBottleP : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scarlet Bottle");
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
            if (projectile.velocity.Y > 0)
            {
                projectile.velocity.Y *= 1.05f;
            }
            else
            {
                projectile.velocity.Y += 0.1f;
            }

            projectile.ai[1]++;
            if (projectile.ai[1] > 7)
            {
                int flamelet = Projectile.NewProjectile(projectile.Center, Vector2.Zero, ProjectileID.MolotovFire, (1), 0f, projectile.owner);
                Main.projectile[flamelet].penetrate = 2;

                projectile.ai[1] = 0;

                if (Main.LocalPlayer.HasBuff(BuffType<Buffs.RisingStar>()) && Main.rand.NextFloat() < 0.1f)
                {
                    Projectile.NewProjectile(projectile.position, (projectile.velocity * 0), ProjectileType<RisingStarP>(), (int)(projectile.damage), 0f, projectile.owner, 0f, 0f);
                }

                if (Main.LocalPlayer.HasBuff(BuffType<Buffs.FallingThunder>()) && Main.rand.NextFloat() < 0.1f)
                {
                    Projectile.NewProjectile(projectile.position, (projectile.velocity * 0), ProjectileType<FallingThunderP>(), (int)(projectile.damage), 0f, projectile.owner, 0f, 0f);
                }
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.FallingThunder>()) && projectile.owner == Main.myPlayer)
            {
                Projectile.NewProjectile(projectile.position, (projectile.velocity * 0), ProjectileType<FallingThunderP>(), (int)(projectile.damage), 0f, projectile.owner, 0f, 0f);
            }
        }

        public override void Kill(int timeLeft)
        {

            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.FallingThunder>()) && projectile.owner == Main.myPlayer)
            {
                Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 107), projectile.Center);
                int burst = Projectile.NewProjectile(projectile.Center, Vector2.Zero, ProjectileID.InfernoFriendlyBlast, projectile.damage / 2, 0f, projectile.owner);
                Main.projectile[burst].timeLeft = 150;
            }
            else
            {
                Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 107), projectile.Center);
                int burst = Projectile.NewProjectile(projectile.Center, Vector2.Zero, ProjectileID.InfernoFriendlyBlast, projectile.damage / 2, 0f, projectile.owner);
                Main.projectile[burst].timeLeft = 50;
            }
        }
    }
}