using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
namespace Revolutions.Items.Weapon.Water
{
    public class FireThrow : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault(Language.GetTextValue("ItemTooltip.Flamethrower"));
        }
        public override void SetDefaults()
        {
            item.useStyle = 5;
            item.autoReuse = true;
            item.useAnimation = 33;
            item.useTime = 11;
            item.width = 50;
            item.height = 18;
            item.shoot = 85;
            item.useAmmo = AmmoID.Gel;
            item.UseSound = SoundID.Item34;
            item.damage = 12;
            item.knockBack = 0.3f;
            item.shootSpeed = 4f;
            item.noMelee = true;
            item.value = 100000;
            item.rare = 4;
            item.ranged = true;
        }
    }
}
