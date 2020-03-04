using Microsoft.Xna.Framework;
using Revolutions.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolutions.Items.DevTools
{
    public class FxTest1 : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Test");
        }
        public override void SetDefaults()
        {
            item.useTime = 20;
            item.useAnimation = 20;
            item.scale = 0.5f;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item1;
            item.value = Item.sellPrice(0, 0, 10, 0); ;
            item.rare = 5;
            item.shootSpeed = 0f;
            item.autoReuse = false;
            item.shoot = ModContent.ProjectileType<Projectiles.Dev.Dev02>();
        }
        int timer = 0;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position = Main.MouseWorld;
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
    }
}
