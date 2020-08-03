using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ArcaneAlchemist.Dusts
{
	public class BasicDust : ModDust
	{
		public override void OnSpawn(Dust dust) {
            dust.noGravity = true;
            dust.noLight = true;
            dust.frame = new Rectangle(0, Main.rand.Next(3) * 8, 8, 8);
            dust.scale *= 2f;
        }

		public override bool MidUpdate(Dust dust) {
			if (!dust.noGravity) {
				dust.velocity.Y = 0f;
			}

			if (dust.noLight) {
				return false;
			}

			float strength = dust.scale * 1.4f;
			if (strength > 1f) {
				strength = 1f;
			}
			Lighting.AddLight(dust.position, 0.1f * strength, 0.2f * strength, 0.7f * strength);
			return false;
		}

        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            dust.scale *= 0.95f;
            if (dust.scale < 0.2f)
            {
                dust.active = false;
            }
            return false;
        }

        public override Color? GetAlpha(Dust dust, Color lightColor) 
			=> new Color(lightColor.R, lightColor.G, lightColor.B, 25);
	}
}