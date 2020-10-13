using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace ArcaneAlchemist.Items.Lunar
{
    public class HorizonFragment : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Horizon Fragment");
            Tooltip.SetDefault("'The end of space is reflected across this fragment'");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 2000;
            item.rare = 9;
            item.maxStack = 999;

            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, new Vector3(1f, 0.21f, 0.5f) * Main.essScale);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
}