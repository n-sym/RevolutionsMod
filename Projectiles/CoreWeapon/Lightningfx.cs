using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Revolutions.Items.Accessory;
using Revolutions.Utils;
using System;
using Terraria;
using Terraria.ModLoader;

namespace Revolutions.Projectiles.CoreWeapon
{
    public class Lightningfx : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;
            projectile.friendly = true;
            projectile.timeLeft = 100000;
            projectile.tileCollide = false;
            projectile.alpha = 255;
            projectile.ignoreWater = true;
            projectile.aiStyle = -1;
            //projectile.extraUpdates = 155;
            projectile.scale = 1f;
        }
        /*int i = 0;
        int j = 0;
        int k = 0;
        int fix = 1000;
        Vector2 b = Vector2.Zero;*/
        public override void AI()
        {
            if (!Lightning.LightningCfgs.accexists) projectile.Kill();
            projectile.timeLeft++;
            //Lightning.LightningCfgs.projexists = true;
            /*Player player = Main.player[projectile.owner];
            int a = Helper.EntroptPool[i + 1000 - fix] / 8;
            Vector2 target = Helper.GetCloser(player.Bottom.X, player.position.Y + 42, RevolutionsPlayer.pastCenter[8 * i + 1].X, RevolutionsPlayer.pastPosition[8 * i + 1].Y + 42, 1, 3);
            target.Y += a;
            j++;
            k++;
            
            Color color = Helper.Specialname2Color(Helper.spname);
            if (Lightning.LightningCfgs.ismove)
            {
                Dust dust = Dust.NewDustDirect(projectile.position, 2, 2, mod.DustType("Pixel"), 0, 0, (int)(255 * i / 12), color, 0.45f);
                dust.position = Helper.GetCloser(projectile.position, target, j, 11);
                //dust.alpha = i / 11 * 255;
            }

            if (j == 11)
            {
                j = 0;
                projectile.position = target;
                i++;
            }
            if (i == 15)
            {
                //j = 0;
                i = 0;
                projectile.position = player.Bottom;
            }
            if (k == 600)
            {
                fix--;
                k = 0;
            }
            if (fix == 0)
            {
                fix = 1000;
            }*/
        }
        Vector2 drawPos2 = Vector2.Zero;
        int k = 0;
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Player player = Main.player[projectile.owner];
            Vector2 drawOrigin = new Vector2(1f, 1f);
            drawPos2 = player.Bottom;
            k++;
            for (int i = 0; i < 16; i++)
            {

                Random rd = new Random();
                Vector2 drawPosition = Helper.GetCloser(player.Bottom.X, player.Bottom.Y,
                    player.GetModPlayer<RevolutionsPlayer>().pastCenter[8 * i + 1].X,
                    player.GetModPlayer<RevolutionsPlayer>().pastCenter[8 * i + 1].Y - player.Center.Y + player.Bottom.Y, 1, 3);
                //取直线
                drawPosition = Helper.GetCloser(player.Bottom, drawPosition, i, 16);
                //分段
                drawPosition.Y += Helper.EntroptPool[(int)(k / 2) - i + 1000] / 8;
                //随机数，来自熵池
                float sizeFix = i + 1;
                sizeFix /= 15;
                sizeFix = 1 - sizeFix;
                //实际上修复颜色

                if (Lightning.LightningCfgs.ismove && (
                    Main.tile[(int)(player.Center.X / 16), (int)(player.Bottom.Y / 16) + 9].active() ||
                    Main.tile[(int)(player.Center.X / 16), (int)(player.Bottom.Y / 16) + 8].active() ||
                    Main.tile[(int)(player.Center.X / 16), (int)(player.Bottom.Y / 16) + 7].active() ||
                    Main.tile[(int)(player.Center.X / 16), (int)(player.Bottom.Y / 16) + 6].active() ||
                    Main.tile[(int)(player.Center.X / 16), (int)(player.Bottom.Y / 16) + 5].active() ||
                    Main.tile[(int)(player.Center.X / 16), (int)(player.Bottom.Y / 16) + 4].active() ||
                    Main.tile[(int)(player.Center.X / 16), (int)(player.Bottom.Y / 16) + 3].active() ||
                    Main.tile[(int)(player.Center.X / 16), (int)(player.Bottom.Y / 16) + 2].active() ||
                    Main.tile[(int)(player.Center.X / 16), (int)(player.Bottom.Y / 16) + 1].active() ||
                    Main.tile[(int)(player.Center.X / 16), (int)(player.Bottom.Y / 16)].active()
                    ))
                {
                    for (int j = 0; j < 16; j++)
                    {
                        Color color = Color.White;
                        if (Helper.Specialname2Color(Helper.spname) == Color.White)
                        {
                            color = Helper.GetCloserColor(Helper.GetRainbowColorLinear(i * 15 + j, 225 + rd.Next(-115, 115)), Color.White, 4, 5);
                        }
                        else
                        {
                            color = Helper.Specialname2Color(Helper.spname);
                        }
                        spriteBatch.Draw(Main.projectileTexture[ModContent.ProjectileType<RareWeapon.MeteowerHelper>()], Helper.GetCloser(drawPos2, drawPosition, j, 15) - Main.screenPosition, null, Color.Multiply(color, sizeFix / 2.5f), projectile.rotation, drawOrigin, 0.25f, SpriteEffects.None, 0f);
                        Lighting.AddLight(Helper.GetCloser(drawPos2, drawPosition, j, 15), color.R / 245, color.G / 245, color.B / 245);
                        Lighting.AddLight(Helper.GetCloser(drawPos2, drawPosition, j, 15), 0.55f, 0.55f, 0.55f);
                    }
                }
                drawPos2 = drawPosition;
                //储存这次分段，下次可以确定新分段
            }
            if (k == 600) k = 0;
        }
        public override void Kill(int timeLeft)
        {
            Lightning.LightningCfgs.projexists = false;
        }
    }
}
