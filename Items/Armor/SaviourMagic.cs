using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Revolutions.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class SaviourMagic : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("This is a modded helmet.");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 10000;
            item.rare = 12;
            item.defense = 2;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<SaviourBreastplate>() && legs.type == ItemType<SaviourLeggings>();
        }
        public override void UpdateEquip(Player player)
        {
            player.magicDamage += 0.4f;
            player.magicCrit += 25;
            player.statManaMax2 += 120;

        }
        public override void UpdateArmorSet(Player player)
        {
            player.GetModPlayer<RevolutionsPlayer>().saviourexist = true;
            player.setBonus = Language.GetTextValue("Mods.Revolutions.SaviourBonus") +"\n" + Language.GetTextValue(Main.ReversedUpDownArmorSetBonuses ? "Key.UP" : "Key.DOWN");
        }


    }
}