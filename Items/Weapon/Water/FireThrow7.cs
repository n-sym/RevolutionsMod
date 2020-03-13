using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
namespace Revolutions.Items.Weapon.Water
{
    public class FireThrow7 : ModItem
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
            item.useTime = 4;
            item.width = 50;
            item.height = 18;
            item.shoot = ModContent.ProjectileType<Projectiles.ForWater.Fire5>();
            item.useAmmo = AmmoID.Gel;
            item.UseSound = SoundID.Item34;
            item.damage = 45;
            item.knockBack = 1f;
            item.crit = 8;
            item.shootSpeed = 15f;
            item.noMelee = true;
            item.value = 1500000;
            item.rare = 9;
            item.ranged = true;
        }
        public override bool ConsumeAmmo(Player player)
        {
            if (RevolutionsPlayer.timer % 6 == 0) return true;
            return false;
        }
    }
}
