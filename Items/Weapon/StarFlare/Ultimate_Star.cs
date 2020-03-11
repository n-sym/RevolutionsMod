using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;

namespace Revolutions.Items.Weapon.StarFlare
{
    public class Ultimate_Star : SFitem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("FW-01");
        }
        int shoottime = 0;
        public override void SetDefaults()
        {
            item.damage = 256;
            item.crit = 16;
            item.ranged = true;
            item.width = 40;
            item.height = 20;
            item.useTime = 6;
            item.useAnimation = 6;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 8;
            item.value = Item.sellPrice(0, 8, 0, 0);
            item.rare = 12;
            item.UseSound = SoundID.Item41;
            item.autoReuse = true;
            item.shoot = 10;
            item.shootSpeed = 40f;
            sfCosume = 2;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-0.65f, -0.1f);
        }

        int likechain = 20;
        int timer = 0;
        float sfix;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (timer == 0)
            {
                shoottime = 0;
            }
            if (shoottime < 501)
            {
                shoottime++;
            }
            if (shoottime < 501)
            {
                item.useTime = -shoottime / 100 + 6;
            }
            if (shoottime < 501)
            {
                likechain = -shoottime / 25 + 20;
            }
            if (shoottime < 501)
            {
                item.damage = (shoottime * 512 + 256000) / 512;
            }
            if (shoottime < 501)
            {
                item.shootSpeed = shoottime / 20 + 30;
            }
            if (shoottime < 501)
            {
                sfix = 1 + shoottime / 250;
            }
            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(likechain));
            speedX = perturbedSpeed.X / sfix;
            speedY = perturbedSpeed.Y / sfix;
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("UltimateStar"), damage, knockBack, player.whoAmI);
            player.itemRotation = (float)Math.Atan(speedY / speedX);
            timer = 60;

            return false;
        }
        public override void HoldItem(Player player)
        {
            if (timer > 0)
            {
                timer--;
            }
        }
    }
}
