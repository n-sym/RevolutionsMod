using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Revolutions.Items.Weapon
{
    public class LightArrow : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("...");
        }
        public override void SetDefaults()
        {
            item.damage = 18;
            item.ranged = true;
            item.width = 34;
            item.height = 14;
            item.useTime = 10;
            item.consumable = true;
            item.maxStack = 999;
            item.shoot = ProjectileType<Projectiles.FinalLightBak>();
            item.value = Item.sellPrice(0, 2, 0, 0); ;
            item.rare = 11;
            item.knockBack = 3f;
            item.shootSpeed = 20f;
            item.ammo = AmmoID.Arrow;
        }

    }
}
