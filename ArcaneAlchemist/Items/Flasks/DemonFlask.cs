using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using ArcaneAlchemist.Projectiles;
using Terraria;
using ArcaneAlchemist.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.Graphics.Effects;
using Terraria.Localization;

namespace ArcaneAlchemist.Items.Flasks
{
	public class DemonFlask : AlchemistItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Demon Flask");
        }

        public override void SafeSetDefaults() 
		{
            item.damage = 500;
            item.width = 48;
            item.height = 48;
            item.useTime = 60;
            item.useAnimation = 60;
            item.useStyle = 1;
            item.knockBack = 2;
            item.value = 0;
            item.maxStack = 1;
            item.rare = 10;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.shoot = ProjectileType<DemonFlaskP>();
            item.shootSpeed = 20f;
            item.noUseGraphic = true;
        }

        //public override void AddRecipes()
        //{
            //ModRecipe recipe = new ModRecipe(mod);
            //recipe.AddIngredient(ItemType<ScarletBottle>(), 1);
            //recipe.AddIngredient(ItemID.InfernoFork, 1);
            //recipe.AddTile(TileID.AlchemyTable);
            //recipe.AddTile(TileID.LunarCraftingStation);
            //recipe.SetResult(this);
            //recipe.AddRecipe();
        //}
    }

    public class DemonFlaskP : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Demon Flask");
        }

        public override void SetDefaults()
        {
            projectile.arrow = false;
            projectile.width = 32;
            projectile.height = 32;
            projectile.friendly = true;
            projectile.aiStyle = 2;
            projectile.penetrate = 1;
        }
        public override void AI()
        {
            
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.RisingStar>()) && projectile.owner == Main.myPlayer)
            {
                Projectile.NewProjectile(projectile.position, (projectile.velocity * 0), ProjectileType<RisingStarP>(), (int)(projectile.damage * 0.2), 0f, projectile.owner, 0f, 0f);
            }

            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.FallingThunder>()) && projectile.owner == Main.myPlayer)
            {
                Projectile.NewProjectile(projectile.position, (projectile.velocity * 0), ProjectileType<FallingThunderP>(), (int)(projectile.damage * 0.4), 0f, projectile.owner, 0f, 0f);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return false;
        }

        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(projectile.position, (projectile.velocity * 0), ProjectileType<DemonBurst>(), (int)(projectile.damage * 0.25), 0f, projectile.owner, 0f, 0f);
        }
    }

    public class DemonBurst : ModProjectile
    {
        private int ChargeLevel { get { return (int)projectile.ai[0]; } set { projectile.ai[0] = value; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Demon Burst");
        }

        public override void SetDefaults()
        {
            projectile.width = 600;
            projectile.height = 600;

            projectile.timeLeft = 120;
            projectile.penetrate = -1;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;

            projectile.netImportant = true;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            fireState(player);
        }
        public override void Kill(int timeLeft)
        {
            explosionEnd();
        }

        private void fireState(Player player)
        {
            explosionStart();
            pushAway();

            //projectile.scale += (Explosion.explosionScale - 1) / Explosion.fireTicksTime;
        }
        
        public void explosionStart()
        {
            //Always make it sound closer
            Vector2 soundPoint = (Main.player[Main.myPlayer].position + projectile.Center * 2) / 3;

            Main.PlaySound(2, soundPoint, 119);
        }
        public void explosionFX(float normalTime)
        {
            //explosion ball dust indicates size
            for (int i = 0; i < (15 + 3) * 3 * normalTime; i++)
            {
                Vector2 velocity = new Vector2(
                    Main.rand.Next(-600, 600 + 1),
                    Main.rand.Next(-600, 600 + 1));

                //make into a circle
                Vector2 normal = new Vector2(velocity.X * 0.5f, velocity.Y * 0.5f);
                normal.Normalize();
                normal.X = Math.Abs(normal.X);
                normal.Y = Math.Abs(normal.Y);
                //make dust move distance of projectile size
                float log = (float)Math.Log((double)(velocity.X * velocity.X + velocity.Y * velocity.Y));

                //make into a ring
                float ring = 600 / velocity.Length();

                //explosion INNER
                Dust d = Dust.NewDustDirect(projectile.Center - new Vector2(16, 16), 32, 32, 262,
                    velocity.X * normal.X / log,
                    velocity.Y * normal.Y / log,
                    0, Color.White, 1f + 15 * 0.3f);
                d.noGravity = true;
                d.velocity *= 0.6f;
                //explosion OUTER
                d = Dust.NewDustDirect(projectile.Center - new Vector2(16, 16), 32, 32, 262,
                    velocity.X * ring / log,
                    velocity.Y * ring / log,
                    0, Color.White, 0.6f + 15 * 0.1f);
                d.noGravity = true;
                d.velocity *= 0.5f;

                if (i % 2 == 0)
                {
                    //explosion shockwave horizontal
                    d = Dust.NewDustDirect(projectile.Center - new Vector2(16, 16), 32, 32, 262,
                        velocity.X * ring * 1.5f / log,
                        velocity.Y * ring * 0.3f / log,
                        0, Color.White, 0.2f);
                    d.noGravity = true;
                    d.fadeIn = 0.4f + Main.rand.NextFloat() * 0.4f + 15 * 0.1f;
                    d.velocity *= 0.5f;

                    //explosion shockwave vertical
                    d = Dust.NewDustDirect(projectile.Center - new Vector2(16, 16), 32, 32, 262,
                        velocity.X * ring * 0.2f / log,
                        velocity.Y * ring * 1.5f / log,
                        0, Color.White, 0.2f);
                    d.noGravity = true;
                    d.fadeIn = 0.4f + Main.rand.NextFloat() * 0.4f + ChargeLevel * 0.1f;
                    d.velocity *= 0.5f;
                }
            }
        }
        public void explosionEnd()
        {
            //smoke dust
            for (int i = 0; i < 30 + ChargeLevel * 10; i++)
            {
                Dust d = Dust.NewDustDirect(projectile.position + new Vector2(projectile.width / 4, projectile.height / 4),
                    projectile.width / 2, projectile.height / 2,
                    31, 0f, 0f, 150, default(Color), 0.8f);
                d.fadeIn = 1f + ChargeLevel * 0.2f;
                d.velocity *= 5f;
            }

            int g = Gore.NewGore(projectile.Center - new Vector2(16, 16),
                new Vector2(projectile.width, projectile.height), Main.rand.Next(61, 64), 1f);
            Main.gore[g].scale *= 1 + 0.08f * ChargeLevel;
            Main.gore[g].velocity *= 0.01f;

            g = Gore.NewGore(projectile.Center - new Vector2(16, 16),
                new Vector2(projectile.width, -projectile.height), Main.rand.Next(61, 64), 1f);
            Main.gore[g].scale *= 1 + 0.08f * ChargeLevel;
            Main.gore[g].velocity *= 0.01f;

            g = Gore.NewGore(projectile.Center - new Vector2(16, 16),
                new Vector2(-projectile.width, -projectile.height), Main.rand.Next(61, 64), 1f);
            Main.gore[g].scale *= 1 + 0.08f * ChargeLevel;
            Main.gore[g].velocity *= 0.01f;

            g = Gore.NewGore(projectile.Center - new Vector2(16, 16),
                new Vector2(-projectile.width, projectile.height), Main.rand.Next(61, 64), 1f);
            Main.gore[g].scale *= 1 + 0.08f * ChargeLevel;
            Main.gore[g].velocity *= 0.01f;

            for (int i = 0; i < ChargeLevel - 3; i++)
            {
                Vector2 velocity = new Vector2(
                    Main.rand.Next(-projectile.width, projectile.width + 1),
                    Main.rand.Next(-projectile.height, projectile.height + 1));
                //make into a circle
                Vector2 normal = new Vector2(velocity.X * 0.5f, velocity.Y * 0.5f);
                normal.Normalize();
                normal.X = Math.Abs(normal.X);
                normal.Y = Math.Abs(normal.Y);
                velocity *= normal;
                //make dust move distance of projectile size
                float log = (float)Math.Log((double)(velocity.X * velocity.X + velocity.Y * velocity.Y));
                velocity /= log;

                g = Gore.NewGore(projectile.Center - new Vector2(16, 16), velocity, Main.rand.Next(61, 64), 1f);
                Main.gore[g].scale *= 1 + 0.08f * ChargeLevel;
                Main.gore[g].velocity *= 0.4f;
                g = Gore.NewGore(projectile.Center - new Vector2(16, 16), velocity, Main.rand.Next(61, 64), 1f);
                Main.gore[g].scale *= 1 + 0.08f * ChargeLevel;
                Main.gore[g].velocity *= 0.2f;
            }
        }

        private void pushAway()
        {
            foreach (Player p in Main.player)
            {
                if (!p.active || p.dead || p.webbed || p.stoned) continue;
                float dist = p.Distance(projectile.Center);
                if (dist > projectile.width) continue;

                Vector2 knockBack = (p.Center - projectile.Center);
                knockBack.Normalize();
                knockBack *= (projectile.width) / (projectile.width / 2 + dist * 2) * (6f + ChargeLevel * 0.5f);
                ////-//Main.NewText("knockback: \n" + knockBack);
                if (p.noKnockback)
                { p.velocity = (p.velocity + knockBack * 2) / 3; }
                else
                { p.velocity = (p.velocity + knockBack * 9) / 10; }
            }
            foreach (NPC n in Main.npc)
            {
                if (!n.active || n.life <= 0 || (n.realLife >= 0 && n.realLife != n.whoAmI) || (n.knockBackResist == 0 && n.velocity == default(Vector2))) continue;
                float dist = n.Distance(projectile.Center);
                if (dist > projectile.width) continue;

                Vector2 knockBack = (n.Center - projectile.Center);
                knockBack.Normalize();
                knockBack *= (projectile.width) / (projectile.width / 2 + dist * 2) * (6f + ChargeLevel * 0.5f);
                //Main.NewText("knockback: \n" + projectile.knockBack);
                knockBack *= (n.knockBackResist * 0.8f + 0.1f) * (1 + projectile.knockBack / 3);

                if ((n.realLife >= 0 && n.realLife != n.whoAmI))
                {
                    // Only push worms if velocity is greater
                    if (knockBack.Length() > n.velocity.Length()) n.velocity = knockBack;
                }
                else
                {
                    // Push away
                    n.velocity = (n.velocity + knockBack * 9) / 10;
                }
            }
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            faceExplosion(target);
            target.AddBuff(BuffID.OnFire, 600, false);
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            faceExplosion(target);
            target.AddBuff(BuffID.OnFire, 600, false);
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            faceExplosion(target);
            target.AddBuff(BuffID.OnFire, 600, false);
        }
        private void faceExplosion(Entity entity)
        {
            if (entity.Center.X < projectile.Center.X)
            {
                projectile.direction = -1;
            }
            else
            {
                projectile.direction = 1;
            }
        }
    }
}