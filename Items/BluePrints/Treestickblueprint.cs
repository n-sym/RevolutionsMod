using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
namespace Revolutions.Items.BluePrints
{
    public class Treestickblueprint : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault(Language.GetTextValue("Mods.Revolutions.BluePrint"));
        }
        public override void SetDefaults()
        {
            item.value = Item.sellPrice(0, 15, 0, 0); ;
            item.rare = 7;
            item.maxStack = 999;
            item.consumable = true;
        }
        public override bool CanRightClick()
        {
            Main.LocalPlayer.GetModPlayer<RevolutionsPlayer>().bluePrint[BluePrintID.LeavesWand] = true;
            Main.soundInstanceItem[37].Play();
            return true;
        }
    }
}
