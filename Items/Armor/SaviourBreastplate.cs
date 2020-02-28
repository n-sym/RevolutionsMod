using Terraria;
using Terraria.ModLoader;

namespace Revolutions.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class SaviourBreastplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault("16% increased damage");
        }
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 10000;
            item.rare = 12;
            item.defense = 30;
        }
        public override void UpdateEquip(Player player)
        {
            player.allDamage += 0.2f;
        }

    }
}