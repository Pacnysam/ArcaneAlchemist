using ArcaneAlchemist.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ArcaneAlchemist.Projectiles
{
    public class RisingStarP : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Healing Star");
        }

        public override void SetDefaults()
        {
            projectile.arrow = false;
            projectile.width = 18;
            projectile.height = 18;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.timeLeft = 120;
            projectile.extraUpdates = 1;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;

            projectile.GetGlobalProjectile<AlchemistProjectile>().arcane = true;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }

        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 0.15f, 0.15f, 0.15f);

            Player player = Main.player[projectile.owner];

            Vector2 target = player.Center + new Vector2(0, -16);
            projectile.velocity += Vector2.Normalize(projectile.Center - target) * -0.8f;

            if (projectile.velocity.Length() >= 12)
            {
                projectile.velocity = Vector2.Normalize(projectile.velocity) * 12f;
            }
            if (projectile.Hitbox.Intersects(new Rectangle((int)player.Center.X -2, (int)player.Center.Y - 14, 4, 4)))
            {
                projectile.position = player.Center;
                Player p = Main.player[(int)projectile.ai[0]];
                p.statLife += (int)(projectile.damage);
                player.HealEffect(projectile.damage);
                projectile.Kill();
            }
        }

       
        public override void Kill(int timeLeft)
        {
            for (float k = 0; k < 6.28f; k += 0.5f)
                Dust.NewDustPerfect(projectile.Center, DustType<RisingStar>(), Vector2.One.RotatedBy(k) * 2);
            
        }
    }
}