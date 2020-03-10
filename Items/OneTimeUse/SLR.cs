using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolutions.Items.OneTimeUse
{
    public class SLR : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Start Slime Rain");
        }
        public override void SetDefaults()
        {
            item.maxStack = 30;
            item.width = 17;
            item.useTurn = false;
            item.consumable = true;
            item.height = 19;
            item.useTime = 8;
            item.useAnimation = 8;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.value = Item.sellPrice(0, 0, 10, 0); ;
            item.rare = 5;
            item.autoReuse = false;
        }
        int timer = 0;
        public override bool UseItem(Player player)
        {
            Main.StartSlimeRain();
            return true;
        }

    }
}
