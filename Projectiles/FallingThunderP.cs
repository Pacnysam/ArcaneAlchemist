using ArcaneAlchemist.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;

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
            projectile.timeLeft = 600;
            projectile.penetrate = 8;
            projectile.extraUpdates = 1;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;

            projectile.GetGlobalProjectile<AlchemistProjectile>().arcane = true;
        }
        public override void AI()
        {
            Dust dust;
            Vector2 position = Main.LocalPlayer.Center;
            dust = Terraria.Dust.NewDustDirect(projectile.position, projectile.width / 2, projectile.height / 2, DustType<FallingThunder>(), 0f, -2f, 150, new Color(255, 255, 255), 0.3f);
            dust.noLight = true;
            dust.shader = GameShaders.Armor.GetSecondaryShader(29, Main.LocalPlayer);
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