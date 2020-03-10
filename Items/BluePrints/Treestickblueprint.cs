using Terraria;
using Terraria.ModLoader;
namespace Revolutions.Items.BluePrints
{
    public class Treestickblueprint : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("LW");
        }
        public override void SetDefaults()
        {
            item.value = Item.sellPrice(0, 15, 0, 0); ;
            item.rare = 7;
            item.material = true;
        }
    }
}
