using ArcaneAlchemist.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace ArcaneAlchemist.Projectiles
{
    public class FallingThunderP : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Falling Thunder");
        }

        public override void SetDefaults()
        {
            projectile.arrow = false;
            projectile.width = 18;
            projectile.height = 18;
            projectile.friendly = true;
            projectile.aiStyle = 36;
            aiType = ProjectileID.Bee;
            projectile.maxPenetrate = -1;
            projectile.timeLeft = 300;
            projectile.penetrate = 8;
        }
        public override void AI()
        {
            Dust dust;
            Vector2 position = Main.LocalPlayer.Center;
            dust = Terraria.Dust.NewDustDirect(projectile.position, projectile.width / 2, projectile.height / 2, 276, 0f, -2f, 150, new Color(255, 255, 255), 0.3f);
            dust.noLight = true;
            dust.shader = GameShaders.Armor.GetSecondaryShader(29, Main.LocalPlayer);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.penetrate--;
            if (projectile.penetrate <= 0)
            {
                projectile.Kill();
            }
            else
            {
                Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, 24, 24);
                if (projectile.velocity.X != oldVelocity.X)
                {
                    projectile.velocity.X = -oldVelocity.X;
                }
                if (projectile.velocity.Y != oldVelocity.Y)
                {
                    projectile.velocity.Y = -oldVelocity.Y;
                }
            }
            return false;
        }

        public override void Kill(int timeLeft)
        {
            for (float k = 0; k < 6.28f; k += 0.5f)
                Dust.NewDustPerfect(projectile.position, DustType<FallingThunder>(), Vector2.One.RotatedBy(k) * 2);
        }
    }
}