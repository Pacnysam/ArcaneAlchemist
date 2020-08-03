using Terraria;
using Terraria.ModLoader;

namespace ArcaneAlchemist.Buffs
{
    public class RisingStar : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Rising Star");
            Description.SetDefault("Arcane Attacks Gain Lifesteal");
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = false;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<AlchemistPlayer>().RisingStar = true;
        }
    }
}