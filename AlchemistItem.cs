using ArcaneAlchemist;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace ArcaneAlchemist 
{
    internal class AlchemistItem : ModItem
    {
        public AlchemistItem ()
        {
            //o
        }

        public virtual void SafeSetDefaults()
        {
        }

        public sealed override void SetDefaults()
        {
            SafeSetDefaults();
            item.melee = false;
            item.ranged = false;
            item.magic = false;
            item.thrown = false;
            item.summon = false;
    }

        public sealed override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            add += AlchemistPlayer.ModPlayer(player).arcaneDamageAdd;
            mult *= AlchemistPlayer.ModPlayer(player).arcaneDamageMult;
        }

        public sealed override void GetWeaponKnockback(Player player, ref float knockback)
        {
            knockback += AlchemistPlayer.ModPlayer(player).arcaneKnockback;
        }

        public sealed override void GetWeaponCrit(Player player, ref int crit)
        {
            crit += AlchemistPlayer.ModPlayer(player).arcaneCrit;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine tt = tooltips.FirstOrDefault(x => x.Name == "Damage" && x.mod == "Terraria");
            if (tt != null)
            {
                string[] splitText = tt.text.Split(' ');
                string damageValue = splitText.First();
                string damageWord = splitText.Last();
                tt.text = damageValue + " arcane " + damageWord;
            }
        }
    }
}