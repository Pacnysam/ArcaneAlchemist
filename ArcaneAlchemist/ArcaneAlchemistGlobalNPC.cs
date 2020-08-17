using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArcaneAlchemist
{
    public class ArcaneAlchemistGlobalNPC : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if (npc.type == NPCID.KingSlime)
                Item.NewItem(npc.getRect(), ItemID.SlimeStaff);

            if (npc.type == NPCID.DarkCaster && Main.rand.Next(16) == 0)
                Item.NewItem(npc.getRect(), mod.ItemType("LifelessPearl"), 1);

            if (npc.type == NPCID.Harpy && Main.rand.Next(7) == 0)
                Item.NewItem(npc.getRect(), mod.ItemType("StarRod"), 1);

            if ((Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].ZoneBeach) && Main.rand.Next(2) == 0)
                Item.NewItem(npc.getRect(), mod.ItemType("SplashGem"), Main.rand.Next(1, 4));
        }
    }
}