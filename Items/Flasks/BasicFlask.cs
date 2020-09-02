using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using ArcaneAlchemist.Projectiles;
using Terraria;
using ArcaneAlchemist.Dusts;
using Microsoft.Xna.Framework;

namespace ArcaneAlchemist.Items.Flasks
{
	public class BasicFlask : AlchemistItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bubble Flask");
        }

        public override void SafeSetDefaults() 
		{
            item.damage = 10;
            item.width = 18;
            item.height = 24;
            item.useTime = 16;
            item.useAnimation = 16;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 3;
            item.value = 1000;
            item.maxStack = 1;
            item.rare = ItemRarityID.White;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.shoot = ProjectileType<BasicFlaskP>();
            item.shootSpeed = 8f;
            item.noUseGraphic = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BottledWater, 2);
            recipe.AddIngredient(ItemID.Gel, 6);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }

    public class BasicFlaskP : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bubble Flask");
        }

        public override void SetDefaults()
        {
            projectile.arrow = false;
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = true;
            projectile.aiStyle = 2;
            projectile.penetrate = 1;
        }
        public override void AI()
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
            Main.PlaySound(SoundID.Item, (int)projectile.position.X, (int)projectile.position.Y, 107);
            Gore.NewGore(projectile.position, -projectile.oldVelocity * 0.2f, 704, 1f);
            Gore.NewGore(projectile.position, -projectile.oldVelocity * 0.2f, 705, 1f);
            for (float k = 0; k < 6.28f; k += 0.25f)
                Dust.NewDustPerfect(projectile.position, DustType<BasicDust>(), Vector2.One.RotatedBy(k) * 2);

            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.RisingStar>()) && projectile.owner == Main.myPlayer)
            {
                Dust dust;
                dust = Terraria.Dust.NewDustPerfect(projectile.position, DustType<RisingStar>(), new Vector2(0f, -1f), 0, new Color(255, 255, 255), 5f);
            }
        }
    }
}