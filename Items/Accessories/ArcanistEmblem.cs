using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ArcaneAlchemist.Items.Accessories
{
	public class ArcanistEmblem : ModItem
	{
		public override void SetStaticDefaults() {
            Tooltip.SetDefault("15% increased arcane damage");
            DisplayName.SetDefault("Alchemist's Emblem");
        }

		public override void SetDefaults() 
		{
            item.width = 28;
			item.height = 28;
            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = ItemRarityID.LightRed;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            AlchemistPlayer modPlayer = AlchemistPlayer.ModPlayer(player);
            modPlayer.arcaneDamageMult *= 1.15f;

        }

        public class EmblemBagDrop : GlobalItem
        {
            public override void OpenVanillaBag(string context, Player player, int arg)
            {
                if (context == "bossBag" && arg == ItemID.WallOfFleshBossBag)
                {
                    switch (Main.rand.Next(4))
                    {
                        case 0:
                            player.QuickSpawnItem(ItemType<ArcanistEmblem>(), 1);
                            break;
                    }
                }
            }
        }

        public class EmblemDrop : GlobalNPC
        {
            public override void NPCLoot(NPC npc)
            {
                if (npc.type == NPCID.WallofFlesh && !Main.expertMode)
                {
                    switch (Main.rand.Next(4))
                    {
                        case 0:
                            Item.NewItem(npc.Hitbox, ItemType<ArcanistEmblem>(), 1);
                            break;
                    }
                }
            }
        }
    }
}