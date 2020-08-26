using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ArcaneAlchemist
{
    public class ArcaneAlchemistGlobalNPC : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if (npc.type == NPCID.KingSlime)
                Item.NewItem(npc.getRect(), ItemID.SlimeStaff);

            //if (npc.type == NPCID.KingSlime && Main.rand.Next(1) == 0)
                //Item.NewItem(npc.getRect(), ItemType<Items.Accessories.GelCanister>(), 1);

            if (npc.type == NPCID.DarkCaster && Main.rand.Next(16) == 0)
                Item.NewItem(npc.getRect(), ItemType<Items.Empowerments.LifelessPearl>(), 1);

            if (npc.type == NPCID.Harpy && Main.rand.Next(7) == 0)
                Item.NewItem(npc.getRect(), ItemType<Items.Empowerments.StarRod>(), 1);

            if ((Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].ZoneBeach) && Main.rand.Next(2) == 0)
                Item.NewItem(npc.getRect(), ItemType<Items.Placeable.SplashGem>(), Main.rand.Next(1, 4));

            if ((Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].ZoneRockLayerHeight) && Main.rand.Next(8) == 0)
                Item.NewItem(npc.getRect(), ItemType<Items.CornifersNotes>(), 1);
        }
    }
}