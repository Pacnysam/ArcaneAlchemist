using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;

namespace ArcaneAlchemist.Items.Summon
{
	public class CoreOfGel : ModItem
	{
		public override void SetStaticDefaults() {
            Tooltip.SetDefault("You've never seen something like this before");
        }

		public override void SetDefaults() 
		{
            item.width = 28;
			item.height = 28;
            item.value = 0;
			item.rare = ItemRarityID.Quest;
            item.maxStack = 1;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.consumable = true;
            item.UseSound = SoundID.Item4;
        }

        public override bool CanUseItem(Player player)
        {
            return !player.GetModPlayer<AlchemistPlayer>().CoreOfGel;
        }

        public override bool UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer && Main.netMode != 1)
            {
                CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width / 2, player.height / 2), Colors.RarityAmber, "Minion Slots Increased", true);
            }
            player.GetModPlayer<AlchemistPlayer>().CoreOfGel = true;
            for (int i = 0; i < 25; i++)
            {
                Dust.NewDust(player.Center, 0, 0, 6, Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 4f), 0, default(Color), Main.rand.NextFloat(1.2f, 1.5f));
            }
            return true;
        }
    }
}