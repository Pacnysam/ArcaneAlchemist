using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using ArcaneAlchemist.Projectiles;
using Terraria;
using ArcaneAlchemist.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArcaneAlchemist.Items.Lunar
{
	public class VoidBlazer : AlchemistItem
	{
        public override void SetStaticDefaults()
        {
            
        }

        public override void SafeSetDefaults() 
		{
            item.damage = 60;
            item.autoReuse = true;
            item.width = 34;
            item.height = 24;
            item.useTime = 12;
            item.useAnimation = 12;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 0;
            Item.sellPrice(15, 0, 0, 0);
            item.maxStack = 1;
            item.rare = ItemRarityID.Quest;
            item.UseSound = SoundID.Item11;
            item.noMelee = true;
            item.shoot = ProjectileType<VoidPulse>();
            item.shootSpeed = 32f;
        }

        public override bool UseItem(Player player)
        {
            Projectile.NewProjectile(player.position, Main.MouseScreen, ProjectileID.TowerDamageBolt, (int)(0), 0f, Main.myPlayer, 0f, 0f);
            return true;
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = mod.GetTexture("Items/Lunar/VoidBlazerGlow");
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    item.position.X - Main.screenPosition.X + item.width * 0.5f,
                    item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }
    }

    public class VoidPulse : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Pulse");
        }

        public override void SetDefaults()
        {
            projectile.arrow = false;
            projectile.width = 30;
            projectile.height = 8;
            projectile.friendly = true;
            aiType = ProjectileID.CrystalDart;
            projectile.maxPenetrate = -1;
            projectile.GetGlobalProjectile<AlchemistProjectile>().arcane = true;
        }
        
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 0;

            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.RisingStar>()) && projectile.owner == Main.myPlayer && crit == true)
            {
                    Projectile.NewProjectile(projectile.position, (projectile.velocity * 0), ProjectileType<RisingStarP>(), (int)(projectile.damage), 0f, projectile.owner, 0f, 0f);
            }

            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.FallingThunder>()) && projectile.owner == Main.myPlayer && crit == true)
            {
                Projectile.NewProjectile(projectile.position, (projectile.velocity * 0), ProjectileType<FallingThunderP>(), (int)(projectile.damage), 0f, projectile.owner, 0f, 0f);
            }
        }

        public override void Kill(int timeLeft)
        {

        }
    }
}