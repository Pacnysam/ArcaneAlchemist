using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ArcaneAlchemist
{
    // This class stores necessary player info for our custom damage class, such as damage multipliers and additions to knockback and crit.
    public class AlchemistPlayer : ModPlayer
    {
        public bool RisingStar;
        public bool FallingThunder;
        public bool GelCanister;
        public bool CoreOfGel;

        public static AlchemistPlayer ModPlayer(Player player)
        {
            return player.GetModPlayer<AlchemistPlayer>();
        }

        public bool arcane;
        public float arcaneDamageAdd;
        public float arcaneDamageMult = 1f;
        public float arcaneKnockback;
        public int arcaneCrit;

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

        public override void UpdateEquips(ref bool wallSpeedBuff, ref bool tileSpeedBuff, ref bool tileRangeBuff)
        {
            if (CoreOfGel == true)
            {
                player.maxMinions++;
            }
        }

        public override TagCompound Save()
        {
            return new TagCompound
            {
                {
                    "CoreOfGel",
                    CoreOfGel
                },
            };
        }

        public override void Load(TagCompound tag)
        {
            CoreOfGel = tag.Get<bool>("CoreOfGel");
        }
    }
}
