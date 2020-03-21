using Microsoft.Xna.Framework;
using Revolutions.Projectiles.StarFlareWeapon;
using Revolutions.Utils;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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
            item.useTime = 1;
            item.useAnimation = 1;
            item.useStyle = 5;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.knockBack = 8;
            item.value = Item.sellPrice(0, 8, 0, 0);
            item.rare = 12;
            item.UseSound = SoundID.Item41;
            item.autoReuse = true;
            item.shoot = 10;
            item.shootSpeed = 40f;
            //sfCosume = 2;
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
            for (int i = 0; i < 3; i++)
            {
                Projectile.NewProjectile(position + new Vector2(200, 0).RotatedByRandom(6.283), Vector2.Zero, ModContent.ProjectileType<USFx>(), 0, 0, player.whoAmI, (float)shoottime / 200 * 5 + 3);
            }
            if (timer == 0)
            {
                shoottime = 0;
            }
            if (shoottime < 201)
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
            timer = 60;

            return false;
        }
        int timer2 = 0;
        public override void HoldItem(Player player)
        {
            if (timer > 0)
            {
                timer--;
            }
            Dust dust = Dust.NewDustPerfect(player.Center + player.direction * new Vector2(player.HeldItem.width, player.HeldItem.height).RotatedBy(player.itemRotation) + player.velocity, ModContent.DustType<Dusts.hyperbola4>(), null, 0, default, 0.25f + (float)shoottime / 500);
            dust.scale = 0.25f + (float)(shoottime) / 500;
            dust.alpha = 255 - (int)(0.25f + (float)(shoottime) * 255);
            dust.position.Y -= 89f * dust.scale;
            if (timer2 > 0)
            {
                timer2--;
            }
            /*if (timer2 == 0)
            {
                for(int i = 0; i < 21; i++)
                {
                    Projectile.NewProjectile(player.Center + new Vector2(200, 0).RotatedBy(6.283 / 20 * i), Vector2.Zero, ModContent.ProjectileType<USFx>(), 0, 0, player.whoAmI, (float)shoottime / 500 * 4 + 8);
                }
                timer2 += 20 - (shoottime / 500 * 10);
            }*/
        }
    }
}
