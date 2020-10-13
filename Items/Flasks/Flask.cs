using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using ArcaneAlchemist.Projectiles;
using Terraria;
using ArcaneAlchemist.Dusts;
using Microsoft.Xna.Framework;

namespace ArcaneAlchemist.Items.Flasks
{
    internal class Flask : AlchemistItem
	{
        public override void SafeSetDefaults() 
		{
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.UseSound = SoundID.Item1;
        }
    }

    internal class FlaskProjectile : ModProjectile
    {
        public virtual void SafeSetDefaults()
        {
        }

        public sealed override void SetDefaults()
        {
            SafeSetDefaults();
            projectile.friendly = true;
            projectile.aiStyle = 2;
            projectile.arrow = false;
            projectile.penetrate = -1;
        }

        public virtual void SafeAI()
        {
        }

        public virtual void AI()
        {
            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.FallingThunder>()) && projectile.owner == Main.myPlayer)
            {
                if (Main.rand.NextFloat() < 0.3f)
                {
                    Dust dust;
                    Vector2 position = Main.LocalPlayer.Center;
                    dust = Terraria.Dust.NewDustDirect(projectile.position, projectile.width / 2, projectile.height / 2, DustType<FallingThunder>(), 0f, -2f, 0, new Color(255, 255, 255), 1f);
                    dust.noLight = true;
                }

            }

            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.RisingStar>()) && projectile.owner == Main.myPlayer)
            {
                Dust dust;
                dust = Terraria.Dust.NewDustPerfect(projectile.position, DustType<RisingStar>(), new Vector2(0f, 0f), 0, new Color(255, 255, 255), 1f);
                dust.noLight = true;

            }
        }

        public virtual void Kill()
        {
            Main.PlaySound(SoundID.Item, (int)projectile.position.X, (int)projectile.position.Y, 107);
            Gore.NewGore(projectile.position, -projectile.oldVelocity * 0.2f, 704, 1f);
            Gore.NewGore(projectile.position, -projectile.oldVelocity * 0.2f, 705, 1f);
        }

        public virtual void SafeOnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
        }

        public virtual void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.RisingStar>()) && projectile.owner == Main.myPlayer && !target.immortal)
            {
                Projectile.NewProjectile(projectile.position, (projectile.velocity * 0), ProjectileType<RisingStarP>(), (int)(1), 0f, projectile.owner, 0f, 0f);
            }

            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.FallingThunder>()) && projectile.owner == Main.myPlayer && !target.immortal)
            {
                Projectile.NewProjectile(projectile.position, (projectile.velocity * 0), ProjectileType<FallingThunderP>(), (int)(projectile.damage / 2), 0f, projectile.owner, 0f, 0f);
            }
            SafeOnHitNPC(target, damage, knockback, crit);
        }
    }
}