using Terraria.ModLoader;


namespace ArcaneAlchemist.Items
{
	public class SplashGem : ModItem
	{
		public override void SetStaticDefaults() {
            Tooltip.SetDefault("Shiny blue gem, reminds you of the ocean.");
        }

		public override void SetDefaults() 
		{
            item.width = 18;
			item.height = 16;
            item.value = 500;
			item.rare = 1;
            item.maxStack = 999;
        }
	}
}