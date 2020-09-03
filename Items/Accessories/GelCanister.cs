using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using ArcaneAlchemist.Projectiles;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using ArcaneAlchemist.Items.Flasks;
using ArcaneAlchemist.Dusts;

namespace ArcaneAlchemist.Items.Accessories
{
	public class GelCanister : AlchemistItem
	{
		public override void SetStaticDefaults() {
            Tooltip.SetDefault("Every 300 damage dealt throws A gel canister at enemies\nEnemies struck by the canister take more damage from fire");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 50;
            item.width = 22;
            item.height = 32;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = ItemRarityID.Blue;
            item.accessory = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<GelCanisterEffect>().canisterEffect = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.GetModPlayer<GelCanisterEffect>().damageCount >= player.GetModPlayer<GelCanisterEffect>().damageCountMax)
            {
                player.GetModPlayer<GelCanisterEffect>().damageCount = 0;

                Vector2 toPos = Vector2.Normalize(Main.MouseWorld - player.position) * Main.rand.NextFloat(8f, 12f);
                Projectile.NewProjectile(player.position, toPos, ProjectileType<GelCanisterP>(), (int)(item.damage), 0f, Main.myPlayer, 0f, 0f);

                CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width / 2, player.height / 2), new Color(169, 248, 255, 100), "Counter Reset");
            }
        }
    }

    public class CanisterToss : AlchemistItem
    {
        public override string Texture => "ArcaneAlchemist/Items/Accessories/GelCanister";

        public override void SafeSetDefaults()
        {
            item.damage = 50;
            item.width = 22;
            item.height = 32;
            item.useTime = 20;
            item.useAnimation = 20;
            item.UseSound = SoundID.Item1;
            item.shootSpeed = 12f;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = ItemRarityID.Blue;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.shoot = ProjectileType<GelCanisterP>();
        }
    }

    public class GelCanisterEffect : ModPlayer
    {
        public bool canisterEffect = false;
        public int damageCount; 
        public int damageCountMax = 300;

        public override void ResetEffects() 
        {
            canisterEffect = false;
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit) 
        {
            if (proj.owner == player.whoAmI && canisterEffect && !target.immortal && proj.type != ProjectileType<GelCanisterP>() && proj.type != ProjectileType<CanisterBoom>() && player.GetModPlayer<AlchemistPlayer>().arcane == true) 
            {
                damageCount += damage;
            }
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (canisterEffect && !target.immortal)
            {
                damageCount += damage;
            }
        }

        public override void PreUpdate()
        {
            
        }
    }

    public class GelCanisterP : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gel Canister");
        }

        public override void SetDefaults()
        {
            projectile.arrow = false;
            projectile.width = 20;
            projectile.height = 20;
            projectile.friendly = true;
            projectile.aiStyle = 2;
            projectile.penetrate = -1;

            projectile.GetGlobalProjectile<AlchemistProjectile>().arcane = true;
        }
        public override void AI()
        {
            for (int I = 0; I < 2; I++)
            {
                if (Main.rand.Next(5) != 0)
                {
                    //slime color is (0, 80, 255)

                    Dust lolwtf = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, DustType<CompressedGelDust>(), projectile.velocity.X, projectile.velocity.Y, 100)];
                    lolwtf.velocity = lolwtf.velocity / 4f + projectile.velocity / 2f;
                    lolwtf.scale = 0.8f + Main.rand.NextFloat() * 0.4f;
                    lolwtf.position = projectile.Center;
                    lolwtf.position += new Vector2(projectile.width * 2, 0f).RotatedBy((float)Math.PI * 2f * Main.rand.NextFloat()) * Main.rand.NextFloat();
                    lolwtf.noLight = true;
                }
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
                target.AddBuff(BuffID.Slimed, 1200);
            target.AddBuff(BuffID.Oiled, 1200);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return false;
        }

        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(projectile.position, projectile.velocity * 0, ProjectileType<CanisterBoom>(), (int)(projectile.damage), 0f, projectile.owner, 0f, 0f);
        }
    }

    public class CanisterBoom : ModProjectile
    {
        public override string Texture => "ArcaneAlchemist/BlankTexture";

        public override void SetDefaults()
        {
            projectile.arrow = false;
            projectile.tileCollide = false;
            projectile.width = 300;
            projectile.height = 300;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 100;
            projectile.extraUpdates = 100;

            projectile.GetGlobalProjectile<AlchemistProjectile>().arcane = true;
        }

        public override void AI()
        {
            Dust dust = Main.dust[Terraria.Dust.NewDust(projectile.Center, 1, 1, DustType<CompressedGelDust>(), Main.rand.Next(-7, 7), Main.rand.Next(-7, 7), 100, new Color(255, 255, 255), 1f)];
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Slimed, 600);
            target.AddBuff(BuffID.Oiled, 600);
        }
    }
}