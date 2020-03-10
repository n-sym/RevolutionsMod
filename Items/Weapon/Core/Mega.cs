using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
namespace Revolutions.Items.Weapon.Core
{
    public class Mega : ModItem
    {
        //int shootint = 0;
        int saveint = 0;
        public class GetMegaShootID
        {
            public static int save1 = 0;
            public static int save2 = 0;
            public static int save3 = 0;
        }
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("66% chance to not consume ammo" + "\n" + "The story's begin.\n" + "Right click to change expaned bullet's ammunition.");
        }
        public override void SetDefaults()
        {
            item.damage = 145;
            item.ranged = true;
            item.width = 40;
            item.crit = 14;
            item.height = 20;
            item.useTime = 5;
            item.useAnimation = 5;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 4;
            item.value = Item.sellPrice(0, 36, 0, 0); ;
            item.rare = 11;
            item.UseSound = SoundID.Item40;
            item.autoReuse = true;
            item.shoot = 10;
            item.shootSpeed = 30f;
            item.useAmmo = AmmoID.Bullet;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useStyle = 5;
                item.useTime = 20;
                item.useAnimation = 20;
                item.damage = 145;
                item.shoot = AmmoID.Bullet;
                item.shootSpeed = 1f;
                item.autoReuse = false;
            }
            else
            {
                item.useStyle = 5;
                item.useTime = 5;
                item.useAnimation = 5;
                item.damage = 145;
                item.shoot = AmmoID.Bullet;
                item.autoReuse = true;
                item.shootSpeed = 30f;
            }
            return base.CanUseItem(player);
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-0.65f, 0.0f);
        }
        public override void UpdateInventory(Player player)
        {
            if (player.HeldItem.type == ModContent.ItemType<Mega>())
            {
                //player.shadowDodge = true;
            }
        }
        public override bool ConsumeAmmo(Player player)
        {
            return Main.rand.NextFloat() >= .66f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Random rd = new Random();
            if (player.altFunctionUse == 2)
            {
                if (saveint == 0)
                {
                    GetMegaShootID.save2 = type;
                    saveint = 1;
                    new Talk(0, Language.GetTextValue("Mods.Revolutions.Mega1"), 180, player);
                }
                else if (saveint == 1)
                {
                    GetMegaShootID.save3 = type;
                    saveint = 2;
                    new Talk(0, Language.GetTextValue("Mods.Revolutions.Mega2"), 180, player);
                }
                else if (saveint == 2)
                {
                    GetMegaShootID.save2 = 0;
                    GetMegaShootID.save3 = 0;
                    saveint = 0;
                    new Talk(0, Language.GetTextValue("Mods.Revolutions.Mega3"), 180, player);
                }

            }
            else
            {
                GetMegaShootID.save1 = type;
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<Projectiles.CoreWeapon.MFL>(), damage, knockBack, player.whoAmI, 1);
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<Projectiles.CoreWeapon.MFL>(), damage, knockBack, player.whoAmI, -1);

            }
            return true;
        }

        /*private void Talk(string message)
        {
            if (Main.netMode != 2)
            {
                string text = message;
                Main.NewText(text, 150, 250, 150);
            }
            else
            {
                NetworkText text = NetworkText.FromKey(message);
                NetMessage.BroadcastChatMessage(text, new Color(150, 250, 150));
            }
        }*/
    }
}
