using Terraria;
using Terraria.ModLoader;

namespace ArcaneAlchemist.Buffs
{
    public class GelCanisterBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Gel Canister");
            Description.SetDefault("oops fuck im too lazy to code this uhhh ill just leave it in as an easter egg i guess idfk");
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = false;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<AlchemistPlayer>().GelCanister = true;
        }
    }
}