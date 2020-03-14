using Microsoft.Xna.Framework;
using Revolutions.Utils;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Revolutions.Items.Accessory

{
    public class Lightning : ModItem
    {
        public class LightningCfgs
        {
            public static bool ismove;
            public static bool projexists = false;
            public static bool accexists = false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightning");
            Tooltip.SetDefault("16% increased damage\n8% increased critical strike chance\nNot moving lets above effects double\n100 increased maximum life\nWearer's movement speed is greatly increased\n Allows player to teleport by double tapping a direction");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(10, 8));
            //ItemID.Sets.AnimatesAsSoul[item.type] = true;
            //ItemID.Sets.ItemIconPulse[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.value = Item.sellPrice(0, 72, 0, 0);
            item.rare = 11;
            item.accessory = true;
        }
        int timera = 0;
        //A按下的计时器
        int timerd = 0;
        int countera = 0;
        //计时器的计数器，为了实现功能
        int counterd = 0;
        int cd = 0;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {

            player.GetModPlayer<RevolutionsPlayer>().lightning = !hideVisual;
            if (player.dash != 0) cd = 2;
            if (cd > 0) cd--;
            if (player.GetModPlayer<RevolutionsPlayer>().aPTimer == 1 || player.GetModPlayer<RevolutionsPlayer>().aPTimer == 2) timera++;
            else timera = 0;
            if (timera > 1) countera += 12;
            if (player.GetModPlayer<RevolutionsPlayer>().dPTimer == 1 || player.GetModPlayer<RevolutionsPlayer>().dPTimer == 2) timerd++;
            else timerd = 0;
            if (timerd > 1) counterd += 12;
            Color color = Helper.Specialname2Color(player.GetModPlayer<RevolutionsPlayer>().spname);
            if (countera > 12 && cd == 0)
            {
                countera = 0;
                cd = 90;
                if (player.GetModPlayer<RevolutionsPlayer>().dPTimer == 0)
                {
                    Vector2 vector2 = new Vector2(player.position.X - Math.Abs(25 * player.velocity.X) - 200, player.position.Y + 17 * player.velocity.Y);
                    if (!Main.tile[(int)(vector2.X / 16), (int)(vector2.Y / 16)].active() || !Main.tileSolid[Main.tile[(int)(vector2.X / 16), (int)(vector2.Y / 16)].type] || Main.tile[(int)(vector2.X / 16), (int)(vector2.Y / 16)].actuator())
                    {
                        for (int i = 0; i < 71; i++)
                        {
                            Dust d = Dust.NewDustDirect(player.position, player.height, player.width, MyDustId.WhiteTrans, 0, 0, 100, color, 0.8f);
                            d.noGravity = true;
                        }
                        Projectile.NewProjectile(player.Center, Vector2.Zero, mod.ProjectileType("Lightningfx2"), 0, 0, player.whoAmI, vector2.X + 13 + (player.velocity.X * 10), vector2.Y + 23 + (player.velocity.Y * 10));
                        player.Teleport(vector2, -1);
                        player.direction = -1;
                        player.velocity.X = -Math.Abs(player.velocity.X);
                        Main.PlaySound(SoundID.Item6, player.position);
                        for (int i = 0; i < 71; i++)
                        {
                            Dust d = Dust.NewDustDirect(player.Center, player.height, player.width, MyDustId.WhiteTrans, 0, 0, 100, color, 0.8f);
                            d.noGravity = true;
                        }
                    }
                    else
                    {
                        new Talk(0, Language.GetTextValue("Mods.Revolutions.CannotTPto"), 180, player);
                    }
                }
                else
                {
                    Vector2 vector2 = new Vector2(player.position.X + Math.Abs(25 * player.velocity.X) + 200, player.position.Y + 17 * player.velocity.Y);
                    if (!Main.tile[(int)(vector2.X / 16), (int)(vector2.Y / 16)].active() || !Main.tileSolid[Main.tile[(int)(vector2.X / 16), (int)(vector2.Y / 16)].type] || Main.tile[(int)(vector2.X / 16), (int)(vector2.Y / 16)].actuator())
                    {
                        for (int i = 0; i < 71; i++)
                        {
                            Dust d = Dust.NewDustDirect(player.Center, player.height, player.width, MyDustId.WhiteTrans, 0, 0, 100, color, 0.8f);
                            d.noGravity = true;
                        }
                        Projectile.NewProjectile(player.Center, Vector2.Zero, mod.ProjectileType("Lightningfx2"), 0, 0, player.whoAmI, vector2.X + 13 + (player.velocity.X * 10), vector2.Y + 23 + (player.velocity.Y * 10));
                        player.Teleport(vector2, -1);
                        Main.PlaySound(SoundID.Item6, player.position);
                        for (int i = 0; i < 71; i++)
                        {
                            Dust d = Dust.NewDustDirect(player.Center, player.height, player.width, MyDustId.WhiteTrans, 0, 0, 100, color, 0.8f);
                            d.noGravity = true;
                        }
                    }
                    else
                    {
                        new Talk(0, Language.GetTextValue("Mods.Revolutions.CannotTPto"), 180, player);
                    }
                }
            }
            if (counterd > 12 && cd == 0)
            {
                counterd = 0;
                cd = 90;
                if (player.GetModPlayer<RevolutionsPlayer>().aPTimer == 0)
                {
                    Vector2 vector2 = new Vector2(player.position.X + Math.Abs(25 * player.velocity.X) + 200, player.position.Y + 17 * player.velocity.Y);
                    if (!Main.tile[(int)(vector2.X / 16), (int)(vector2.Y / 16)].active() || !Main.tileSolid[Main.tile[(int)(vector2.X / 16), (int)(vector2.Y / 16)].type] || Main.tile[(int)(vector2.X / 16), (int)(vector2.Y / 16)].actuator())
                    {
                        for (int i = 0; i < 71; i++)
                        {
                            Dust d = Dust.NewDustDirect(player.position, player.height * 1, player.width * 1, MyDustId.WhiteTrans, 0, 0, 100, color, 0.8f);
                            d.noGravity = true;
                        }
                        Projectile.NewProjectile(player.Center, Vector2.Zero, mod.ProjectileType("Lightningfx2"), 0, 0, player.whoAmI, vector2.X + 13 + (player.velocity.X * 10), vector2.Y + 23 + (player.velocity.Y * 10));
                        player.Teleport(vector2, -1);
                        player.direction = -1;
                        player.velocity.X = Math.Abs(player.velocity.X);
                        Main.PlaySound(SoundID.Item6, player.position);
                        for (int i = 0; i < 71; i++)
                        {
                            Dust d = Dust.NewDustDirect(player.position, player.height * 1, player.width * 1, MyDustId.WhiteTrans, 0, 0, 100, color, 0.8f);
                            d.noGravity = true;
                        }
                    }
                    else
                    {
                        new Talk(0, Language.GetTextValue("Mods.Revolutions.CannotTPto"), 180, player);
                    }
                }
                else
                {
                    Vector2 vector2 = new Vector2(player.position.X - Math.Abs(25 * player.velocity.X) - 200, player.position.Y + 17 * player.velocity.Y);
                    if (!Main.tile[(int)(vector2.X / 16), (int)(vector2.Y / 16)].active() || !Main.tileSolid[Main.tile[(int)(vector2.X / 16), (int)(vector2.Y / 16)].type] || Main.tile[(int)(vector2.X / 16), (int)(vector2.Y / 16)].actuator())
                    {
                        for (int i = 0; i < 71; i++)
                        {
                            Dust d = Dust.NewDustDirect(player.position, player.height * 1, player.width * 1, MyDustId.WhiteTrans, 0, 0, 100, color, 0.8f);
                            d.noGravity = true;
                        }
                        Projectile.NewProjectile(player.Center, Vector2.Zero, mod.ProjectileType("Lightningfx2"), 0, 0, player.whoAmI, vector2.X + 13 + (player.velocity.X * 10), vector2.Y + 23 + (player.velocity.Y * 10));
                        player.Teleport(vector2, -1);
                        Main.PlaySound(SoundID.Item6, player.position);
                        for (int i = 0; i < 71; i++)
                        {
                            Dust d = Dust.NewDustDirect(player.position, player.height * 1, player.width * 1, MyDustId.WhiteTrans, 0, 0, 100, color, 0.8f);
                            d.noGravity = true;
                        }
                    }
                    else
                    {
                        new Talk(0, Language.GetTextValue("Mods.Revolutions.CannotTPto"), 180, player);
                    }
                }
            }
            if (countera > 0) countera--;
            if (counterd > 0) counterd--;
            if (player.GetModPlayer<RevolutionsPlayer>().lightning)
            {
                if (!player.GetModPlayer<RevolutionsPlayer>().lightningproj)
                {
                    Projectile.NewProjectile(player.Bottom.X, player.Bottom.Y, 0, 0, mod.ProjectileType("Lightningfx"), 0, 0, player.whoAmI);
                    player.GetModPlayer<RevolutionsPlayer>().lightning = true;
                }
            }
            player.maxRunSpeed *= 3.15f;
            player.allDamage *= 1.16f;
            player.magicCrit += 8;
            player.rangedCrit += 8;
            player.meleeCrit += 8;
            player.thrownCrit += 8;
            player.maxMinions += 3;
            player.statLifeMax2 = player.statLifeMax2 + 100;
            if (player.position != player.oldPosition)
            {
                LightningCfgs.ismove = true;
                if (player.position.X == player.GetModPlayer<RevolutionsPlayer>().pastPosition[5].X) LightningCfgs.ismove = false;
            }
            else
            {
                LightningCfgs.ismove = false;
                player.maxRunSpeed *= 3.15f;
                player.allDamage *= 1.16f;
                player.magicCrit += 8;
                player.rangedCrit += 8;
                player.meleeCrit += 8;
                player.thrownCrit += 8;
            }
        }
    }
}