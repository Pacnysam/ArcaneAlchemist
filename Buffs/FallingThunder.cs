using Terraria;
using Terraria.ModLoader;
using ArcaneAlchemist;

namespace ArcaneAlchemist.Buffs
{
    public class FallingThunder : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Falling Thunder");
            Description.SetDefault("Arcane Damage Greatly Increased");
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = false;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
            player.GetModPlayer<AlchemistPlayer>().FallingThunder = true;
            player.GetModPlayer<AlchemistPlayer>().arcaneDamageMult *= 2;
            player.GetModPlayer<AlchemistPlayer>().arcaneCrit *= 9;
        }
    }
}