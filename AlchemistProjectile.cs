using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace ArcaneAlchemist
{
    public class AlchemistProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public override bool CloneNewInstances => true;
        public bool arcane = false;

        public override void ModifyHitNPC(Projectile projectile, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player player = Main.player[projectile.owner];
            int critChance = player.HeldItem.crit;
            ItemLoader.GetWeaponCrit(player.HeldItem, player, ref AlchemistPlayer.ModPlayer(player).arcaneCrit);
            PlayerHooks.GetWeaponCrit(player, player.HeldItem, ref AlchemistPlayer.ModPlayer(player).arcaneCrit);
            if (critChance >= 100 || Main.rand.Next(1, 101) <= critChance)
            {
                crit = true;
            }
        }
    }
}
