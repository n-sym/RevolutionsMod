using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolutions.Items.DevTools
{
    public class WdPlatGen : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Generate worldwide platfrom");
        }
        public override void SetDefaults()
        {
            item.width = 17;
            item.useTurn = false;
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
            for (int i = 0; i <= Main.maxTilesX; i++)
            {
                Main.tile[i, (int)(player.Bottom.Y / 16)].active(true);
                Main.tile[i, (int)(player.Bottom.Y / 16)].type = 19;
            }
            return true;
        }

    }
}
