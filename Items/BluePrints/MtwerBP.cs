using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Revolutions.Items.BluePrints
{
    public class MtwerBP : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("BP");
        }
        public override void SetDefaults()
        {
            item.value = Item.sellPrice(0, 15, 0, 0); ;
            item.rare = 6;
            item.material = true;
        }

    }
}
