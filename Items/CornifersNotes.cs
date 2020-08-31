using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ArcaneAlchemist.Items
{
	public class CornifersNotes : ModItem
	{
		public override void SetStaticDefaults() {
            Tooltip.SetDefault("Reading this makes you want to hum.");
            DisplayName.SetDefault("Cornifer's Notes");
        }

		public override void SetDefaults() 
		{
            item.width = 24;
			item.height = 24;
            item.value = 10000;
            item.useStyle = ItemUseStyleID.HoldingUp;
			item.rare = ItemRarityID.LightRed;
            item.consumable = true;
        }

        public override bool UseItem(Player player)
        {
            CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 10, player.width, player.height), new Color(255, 255, 255, 50), "Area Charted");
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/CorniferHum"));

                Point center = Main.player[Main.myPlayer].Center.ToTileCoordinates();

                int range = 120;

                for (int i = center.X - range / 2; i < center.X + range / 2; i++)
                {
                    for (int j = center.Y - range / 2; j < center.Y + range / 2; j++)
                    {
                        if (WorldGen.InWorld(i, j))
                            Main.Map.Update(i, j, 255);
                    }
                }
                Main.refreshMap = true;
            }
            return true;
        }
    }
}