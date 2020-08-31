using Terraria.ModLoader;


namespace ArcaneAlchemist.Items.Empowerments
{
	public class LifelessPearl : ModItem
	{
		public override void SetStaticDefaults() {
            Tooltip.SetDefault("This pearl is dull and pale.");
        }

		public override void SetDefaults() 
		{
            item.width = 16;
			item.height = 16;
            item.value = 0;
			item.rare = -1;
        }
	}
}