using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
namespace Revolutions.Items.Weapon.Water
{
    public class FireThrow2 : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault(Language.GetTextValue("ItemTooltip.Flamethrower"));
        }
        public override void SetDefaults()
        {
            item.useStyle = 5;
            item.autoReuse = true;
            item.useAnimation = 32;
            item.useTime = 8;
            item.width = 50;
            item.height = 18;
            item.shoot = 85;
            item.useAmmo = AmmoID.Gel;
            item.UseSound = SoundID.Item34;
            item.damage = 17;
            item.knockBack = 0.3f;
            item.shootSpeed = 6f;
            item.noMelee = true;
            item.value = 150000;
            item.rare = 4;
            item.ranged = true;
        }
    }
}
