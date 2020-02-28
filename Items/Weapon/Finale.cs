using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Revolutions.Items.Weapon
{
    public class Finale : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("66% chance to not consume ammo" + "\n" + "The story's end.");
        }
        public override void SetDefaults()
        {
            item.damage = 145;
            item.ranged = true;
            item.width = 40;
            item.crit = 14;
            item.height = 20;
            item.useTime = 8;
            item.useAnimation = 8;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 10;
            item.value = Item.sellPrice(0, 36, 0, 0); ;
            item.rare = 11;
            item.UseSound = SoundID.Item5;
            item.autoReuse = true;
            item.shoot = 10;
            item.shootSpeed = 80f;
            item.useAmmo = AmmoID.Arrow;
        }
        public override Vector2? HoldoutOffset() => new Vector2(-0.65f, 0.0f);
        public override bool ConsumeAmmo(Player player)
        {
            return Main.rand.NextFloat() >= .66f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Random rd = new Random();
            Projectile.NewProjectile(position.X - 12, position.Y - 12, speedX, speedY, type, damage / 2, knockBack, player.whoAmI);
            Projectile.NewProjectile(position.X - 4, position.Y - 4, speedX, speedY, type, damage / 2, knockBack, player.whoAmI);
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("FinalLightAlt"), damage, 1, player.whoAmI);
            position.Y = position.Y + 4;
            position.X = position.X + 4;
            Projectile.NewProjectile(position.X + 8, position.Y + 8, speedX, speedY, type, damage / 2, knockBack, player.whoAmI);
            return true;
        }
    }
}
