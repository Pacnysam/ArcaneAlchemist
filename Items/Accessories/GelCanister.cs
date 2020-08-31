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

namespace ArcaneAlchemist.Items.Accessories
{
	public class GelCanister : AlchemistItem
	{
		public override void SetStaticDefaults() {
            Tooltip.SetDefault("Every 300 arcane damage dealt throws A gel canister at enemies\nEnemies struck by the canister take more damage from fire");
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
    }

    public class GelCanisterEffect : ModPlayer
    {
        public bool canisterEffect = false; //does the player get this effect
        public int damageCount; //used to count as damage is dealt
        public int damageCountMax = 300;

        public override void ResetEffects() //used to reset if the player unequips the accesory
        {
            canisterEffect = false;
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit) //runs when an npc is hit by the player's projectile
        {
            if (proj.owner == player.whoAmI && canisterEffect && !target.immortal && proj.type != ProjectileType<GelCanisterP>() && player.GetModPlayer<AlchemistPlayer>().arcane == true) //check if vallid npc and effect is active
            {
                damageCount += damage; //count up
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
            if (damageCount >= damageCountMax)
            {
                damageCount = 0;

                Vector2 toPos = Vector2.Normalize(player.position + Main.MouseWorld) * 7f;
                Projectile.NewProjectile(player.position, toPos, ProjectileType<GelCanisterP>(), (int)(50), 0f, Main.myPlayer, 0f, 0f);

                CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width/2, player.height/2), new Color(169, 248, 255, 100), "Counter Reset");
            }
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
            //to be done later
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
                target.AddBuff(BuffID.Slimed, 600);
            target.AddBuff(BuffID.Oiled, 600);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return false;
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 107);
            Gore.NewGore(projectile.position, -projectile.oldVelocity * 0.2f, 704, 1f);
            Gore.NewGore(projectile.position, -projectile.oldVelocity * 0.2f, 705, 1f);
            for (float k = 0; k < 6.28f; k += 0.25f) ;
        }
    }
}