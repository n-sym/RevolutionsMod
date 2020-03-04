using Revolutions.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolutions.Items.DevTools
{
    public class Throw2Brk : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Break Tiles");
        }
        public override void SetDefaults()
        {
            item.useTime = 5;
            item.useAnimation = 5;
            item.scale = 0.5f;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item1;
            item.value = Item.sellPrice(0, 0, 10, 0); ;
            item.rare = 5;
            item.shootSpeed = 30f;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<Projectiles.Dev.Dev01>();
        }
        int timer = 0;

    }
}
