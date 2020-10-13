using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using ArcaneAlchemist.Projectiles;
using Terraria;
using ArcaneAlchemist.Buffs;
using Microsoft.Xna.Framework;

namespace ArcaneAlchemist.Items.Flasks
{
    internal class ScarletBottle : AlchemistItem
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
            item.knockBack = 3;
            item.value = 5000;
            item.maxStack = 1;
            item.rare = 2;
            item.shoot = ProjectileType<ScarletBottleP>();
            item.shootSpeed = 13f;
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
            projectile.width = 10;
            projectile.height = 10;
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
                int flamelet = Projectile.NewProjectile(projectile.Center, Vector2.Zero, ProjectileID.MolotovFire, (3), 0f, projectile.owner);
                Main.projectile[flamelet].penetrate = 1;

                projectile.ai[1] = 0;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
        }

        public override void Kill(int timeLeft)
        {
                Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 107), projectile.Center);
                int burst = Projectile.NewProjectile(projectile.Center, Vector2.Zero, ProjectileID.InfernoFriendlyBlast, projectile.damage / 2, 0f, projectile.owner);
                Main.projectile[burst].timeLeft = 50;
        }
    }
}