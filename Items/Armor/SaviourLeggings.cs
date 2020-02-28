using Terraria;
using Terraria.ModLoader;

namespace Revolutions.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class SaviourLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("15% increased movement speed");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 10000;
            item.rare = 12;
            item.defense = 20;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.15f;
        }

    }
}