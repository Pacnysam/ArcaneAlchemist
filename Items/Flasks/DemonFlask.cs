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
            DisplayName.SetDefault("Hell Flask");
        }

        public override void SafeSetDefaults() 
		{
            item.damage = 30;
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

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<ScarletBottle>(), 1);
            recipe.AddIngredient(ItemID.HellstoneBar, 20);
            recipe.AddTile(TileID.AlchemyTable);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
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
                Projectile.NewProjectile(projectile.position, (projectile.velocity * 0), ProjectileType<RisingStarP>(), (int)(1), 0f, projectile.owner, 0f, 0f);
            }

            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.FallingThunder>()) && projectile.owner == Main.myPlayer)
            {
                Projectile.NewProjectile(projectile.position, (projectile.velocity * 0), ProjectileType<FallingThunderP>(), (int)(projectile.damage/2), 0f, projectile.owner, 0f, 0f);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return false;
        }

        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(projectile.position, (projectile.velocity * 0), ProjectileType<DemonBurst>(), (int)(projectile.damage), 0f, projectile.owner, 0f, 0f);

            Vector2 soundPoint = (Main.player[Main.myPlayer].position + projectile.Center * 2) / 3;

            Main.PlaySound(2, soundPoint, 119);
        }
    }

    public class DemonBurst : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Demon Burst");
        }

        public override void SetDefaults()
        {
            projectile.width = 300;
            projectile.height = 300;
            projectile.timeLeft = 60;
            projectile.penetrate = -1;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.netImportant = true;

            projectile.GetGlobalProjectile<AlchemistProjectile>().arcane = true;
        }

        public override void AI()
        {
            if (projectile.ai[1] <= 1)
            {
                Player player = Main.player[projectile.owner];
                fireState(player);
            }

            Dust dust = Main.dust[Terraria.Dust.NewDust(projectile.Center, 1, 1, 174, Main.rand.Next(-20, 20), Main.rand.Next(-20, 20), 0, new Color(255, 255, 255), 3f)];
            dust = Main.dust[Terraria.Dust.NewDust(projectile.Center, 1, 1, 174, Main.rand.Next(-20, 20), Main.rand.Next(-9, 9), 0, new Color(255, 255, 255), 3f)];
            dust = Main.dust[Terraria.Dust.NewDust(projectile.Center, 1, 1, 174, Main.rand.Next(-20, 20), Main.rand.Next(-9, 9), 0, new Color(255, 255, 255), 3f)];
            dust = Main.dust[Terraria.Dust.NewDust(projectile.Center, 1, 1, 174, Main.rand.Next(-20, 20), Main.rand.Next(-9, 9), 0, new Color(255, 255, 255), 3f)];
            dust = Main.dust[Terraria.Dust.NewDust(projectile.Center, 1, 1, 174, Main.rand.Next(-20, 20), Main.rand.Next(-9, 9), 0, new Color(255, 255, 255), 3f)];
            dust.noGravity = true;
        }
        public override void Kill(int timeLeft)
        {

        }

        private void fireState(Player player)
        {
            pushAway();
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
                knockBack *= 3;
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
                knockBack *= (projectile.width) / (projectile.width / 2 + dist * 2) * (6f * 0.5f);
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