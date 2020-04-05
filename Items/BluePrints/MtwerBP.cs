using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Revolutions.Items.BluePrints
{
    public class MtwerBP : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault(Language.GetTextValue("Mods.Revolution.BluePrint"));
        }
        public override void SetDefaults()
        {
            item.value = Item.sellPrice(0, 15, 0, 0);
            item.rare = 7;
            item.maxStack = 999;
            item.consumable = true;
        }
        public override bool CanRightClick()
        {
            Main.LocalPlayer.GetModPlayer<RevolutionsPlayer>().bluePrint[BluePrintID.Meteower] = true;
            Main.soundInstanceItem[37].Play();
            return true;
        }
    }
}
