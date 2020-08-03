using Terraria;
using Terraria.ModLoader;

namespace ArcaneAlchemist
{
    // This class stores necessary player info for our custom damage class, such as damage multipliers and additions to knockback and crit.
    public class AlchemistPlayer : ModPlayer
    {
        public bool RisingStar;
        public bool FallingThunder;

        public static AlchemistPlayer ModPlayer(Player player)
        {
            return player.GetModPlayer<AlchemistPlayer>();
        }

        // Vanilla only really has damage multipliers in code
        // And crit and knockback is usually just added to
        // As a modder, you could make separate variables for multipliers and simple addition bonuses
        public bool arcane;
        public float arcaneDamageAdd;
        public float arcaneDamageMult = 1f;
        public float arcaneKnockback;
        public int arcaneCrit;

        public bool PacBat { get; internal set; }
        public bool PacBatEX { get; internal set; }

        public override void ResetEffects()
        {
            ResetVariables();
        }

        public override void UpdateDead()
        {
            ResetVariables();
        }

        private void ResetVariables()
        {
            arcane = true;
            arcaneDamageAdd = 0f;
            arcaneDamageMult = 1f;
            arcaneKnockback = 0f;
            arcaneCrit = 4;
        }
    }
}
