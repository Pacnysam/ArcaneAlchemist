using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ArcaneAlchemist.Items.Accessories
{
	public class Gem : ModItem
	{
		public override void SetStaticDefaults() {
            Tooltip.SetDefault("Double tap in any direction to dash");
            DisplayName.SetDefault("Boost Crystal");
        }

		public override void SetDefaults() 
		{
            item.width = 22;
			item.height = 26;
            item.value = Item.sellPrice(0, 20, 0, 0);
            item.rare = ItemRarityID.Expert;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            GemDashPlayer mp = player.GetModPlayer<GemDashPlayer>();

            //If the dash is not active, immediately return so we don't do any of the logic for it
            if (!mp.DashActive)
                return;

            //This is where we set the afterimage effect.  You can replace these two lines with whatever you want to happen during the dash
            //Some examples include:  spawning dust where the player is, adding buffs, making the player immune, etc.
            //Here we take advantage of "player.eocDash" and "player.armorEffectDrawShadowEOCShield" to get the Shield of Cthulhu's afterimage effect
            player.eocDash = mp.DashTimer;
            player.armorEffectDrawShadowEOCShield = true;

            //If the dash has just started, apply the dash velocity in whatever direction we wanted to dash towards
            if (mp.DashTimer == GemDashPlayer.MAX_DASH_TIMER)
            {
                Vector2 newVelocity = player.velocity;

                //Only apply the dash velocity if our current speed in the wanted direction is less than DashVelocity
                if ((mp.DashDir == GemDashPlayer.DashUp && player.velocity.Y > -mp.DashVelocity) || (mp.DashDir == GemDashPlayer.DashDown && player.velocity.Y < mp.DashVelocity))
                {
                    //Y-velocity is set here
                    //If the direction requested was DashUp, then we adjust the velocity to make the dash appear "faster" due to gravity being immediately in effect
                    //This adjustment is roughly 1.3x the intended dash velocity
                    float dashDirection = mp.DashDir == GemDashPlayer.DashDown ? 1 : -1.3f;
                    newVelocity.Y = dashDirection * mp.DashVelocity;
                }
                else if ((mp.DashDir == GemDashPlayer.DashLeft && player.velocity.X > -mp.DashVelocity) || (mp.DashDir == GemDashPlayer.DashRight && player.velocity.X < mp.DashVelocity))
                {
                    //X-velocity is set here
                    int dashDirection = mp.DashDir == GemDashPlayer.DashRight ? 1 : -1;
                    newVelocity.X = dashDirection * mp.DashVelocity;
                }

                player.velocity = newVelocity;
            }

            //Decrement the timers
            mp.DashTimer--;
            mp.DashDelay--;

            if (mp.DashDelay == 0)
            {
                mp.DashDelay = GemDashPlayer.MAX_DASH_DELAY;
                mp.DashTimer = GemDashPlayer.MAX_DASH_TIMER;
                mp.DashActive = false;
            }
        }
    }

    public class GemDashPlayer : ModPlayer
    {
        public static readonly int DashDown = 0;
        public static readonly int DashUp = 1;
        public static readonly int DashRight = 2;
        public static readonly int DashLeft = 3;

        public int DashDir = -1;
        public bool DashActive = false;
        public int DashDelay = MAX_DASH_DELAY;
        public int DashTimer = MAX_DASH_TIMER;
        public readonly float DashVelocity = 15f;
        public static readonly int MAX_DASH_DELAY = 10;
        public static readonly int MAX_DASH_TIMER = 10;

        public override void ResetEffects()
        {
            bool dashAccessoryEquipped = false;

            //This is the loop used in vanilla to update/check the not-vanity accessories
            for (int i = 3; i < 8 + player.extraAccessorySlots; i++)
            {
                Item item = player.armor[i];

                //Set the flag for the ExampleDashAccessory being equipped if we have it equipped OR immediately return if any of the accessories are
                // one of the higher-priority ones
                if (item.type == ItemType<Gem>())
                    dashAccessoryEquipped = true;
                else if (item.type == ItemID.EoCShield || item.type == ItemID.MasterNinjaGear || item.type == ItemID.Tabi)
                    return;
            }

            //If we don't have the ExampleDashAccessory equipped or the player has the Solor armor set equipped, return immediately
            //Also return if the player is currently on a mount, since dashes on a mount look weird, or if the dash was already activated
            if (!dashAccessoryEquipped || player.setSolar || player.mount.Active || DashActive)
                return;

            //When a directional key is pressed and released, vanilla starts a 15 tick (1/4 second) timer during which a second press activates a dash
            //If the timers are set to 15, then this is the first press just processed by the vanilla logic.  Otherwise, it's a double-tap
            if (player.controlDown && player.releaseDown && player.doubleTapCardinalTimer[DashDown] < 15)
                DashDir = DashDown;
            else if (player.controlUp && player.releaseUp && player.doubleTapCardinalTimer[DashUp] < 15)
                DashDir = DashUp;
            else if (player.controlRight && player.releaseRight && player.doubleTapCardinalTimer[DashRight] < 15)
                DashDir = DashRight;
            else if (player.controlLeft && player.releaseLeft && player.doubleTapCardinalTimer[DashLeft] < 15)
                DashDir = DashLeft;
            else
                return;  //No dash was activated, return

            DashActive = true;

            //Here you'd be able to set an effect that happens when the dash first activates
            //Some examples include:  the larger smoke effect from the Master Ninja Gear and Tabi
        }
    }
}