using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ArcaneAlchemist.Dusts
{
	public class CompressedGelDust : ModDust
	{
		public override void OnSpawn(Dust dust) {
            dust.noGravity = false;
            
            dust.frame = new Rectangle(0, Main.rand.Next(10) * 8, 8, 8);
            dust.scale *= 2f;
        }

		public override bool MidUpdate(Dust dust) {
			if (!dust.noGravity) {
				dust.velocity.Y = 0f;
			}
            return false;
		}

        public override bool Update(Dust dust)
        {
            dust.velocity.Y = dust.velocity.Y + 0.1f;
            if (dust.velocity.Y > 12f)
            {
                dust.velocity.Y = 12f;
            }

            if (dust.scale <0.4)
            {
                dust.scale *= 0.99f;
                dust.velocity *= 0.99f;
            } 
            else 
            {
                dust.scale *= 0.96f;
            }

            dust.position += dust.velocity;
            if (dust.scale < 0.1f)
            {
                dust.active = false;
            }
            return false;
        }

        public override Color? GetAlpha(Dust dust, Color lightColor) 
			=> new Color(lightColor.R, lightColor.G, lightColor.B, 25);
	}
}