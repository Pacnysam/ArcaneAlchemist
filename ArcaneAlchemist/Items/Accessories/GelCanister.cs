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
            Tooltip.SetDefault("Every 300 damage dealt throws A gel canister at enemies");
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
            player.GetModPlayer<GelCanisterEffect>().effect = true;
        }
    }

    public class GelCanisterEffect : ModPlayer
    {
        public bool effect = false; //does the player get this effect
        public int damageTally; //used to count as damage is dealt
        public int damageTallyMax = 500;

        public override void ResetEffects() //used to reset if the player unequips the accesory
        {
            effect = false;
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit) //runs when an npc is hit by the player's projectile
        {
            if (proj.owner == player.whoAmI && effect && !target.immortal && proj.type != ProjectileType<SplashBomb>()) //check if vallid npc and effect is active
            {
                damageTally += damage; //count up
            }
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit) //runs when an npc is hit by an item (sword blade)
        {
            if (effect && !target.immortal)
            {
                damageTally += damage;
            }
        }

        public override void PreUpdate()
        {
            if (damageTally >= damageTallyMax)
            {
                damageTally = 0;
                CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width/2, player.height/2), new Color(169, 248, 255, 100), "Counter Reset");
            }
        }
    }
}