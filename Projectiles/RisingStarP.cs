using ArcaneAlchemist.Dusts;
using Microsoft.Xna.Framework;
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
        }

        public override void AI()
        {
            Dust.NewDustPerfect(projectile.position, DustType<RisingStar>());

            Lighting.AddLight(projectile.Center, 0.15f, 0.15f, 0.15f);

            Player player = Main.player[projectile.owner];

            Vector2 target = player.Center + new Vector2(0, -16);
            projectile.velocity += Vector2.Normalize(projectile.Center - target) * -0.8f;

            if (projectile.velocity.Length() >= 12)
            {
                projectile.velocity = Vector2.Normalize(projectile.velocity) * 12f;
            }
            if (projectile.Hitbox.Intersects(new Rectangle((int)player.Center.X - 2, (int)player.Center.Y - 14, 4, 4)) || projectile.timeLeft == 1)
            {
                projectile.position = player.Center;
                Player p = Main.player[(int)projectile.ai[0]];
                p.statLife += (int)(2);
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