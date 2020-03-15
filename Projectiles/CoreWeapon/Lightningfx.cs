using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
            if (!Main.player[projectile.owner].GetModPlayer<RevolutionsPlayer>().lightning) projectile.Kill();
            if (Main.player[projectile.owner].position != Main.player[projectile.owner].oldPosition)
            {
                projectile.ai[0] = 1;
                if (Main.player[projectile.owner].position.X == Main.player[projectile.owner].GetModPlayer<RevolutionsPlayer>().pastPosition[5].X) projectile.ai[0] = 0;
            }
            else
            {
                projectile.ai[0] = 0;
            }
            projectile.timeLeft++;
        }
        Vector2 drawPos2 = Vector2.Zero;
        int k = 0;
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Player player = Main.player[projectile.owner];
            Vector2 drawOrigin = new Vector2(1f, 1f);
            drawPos2 = player.Bottom;
            float safelength = player.velocity.Length() * 1.5f;
            if (safelength > 74) safelength = 74;
            if (!Main.gamePaused) k++;
            for (int i = 0; i < safelength; i++)
            {
                Random rd = new Random();
                Vector2 drawPosition = Helper.GetCloser(player.Bottom.X, player.Bottom.Y,
                    player.GetModPlayer<RevolutionsPlayer>().pastCenter[8 * i + 1].X,
                    player.GetModPlayer<RevolutionsPlayer>().pastCenter[8 * i + 1].Y - player.Center.Y + player.Bottom.Y, 1, 3);
                //取直线
                drawPosition = Helper.GetCloser(player.Bottom, drawPosition, i, 16);
                //分段
                drawPosition.Y += Helper.EntroptPool[(int)(k / 2) - i + 1000] / 8;
                drawPosition.X += Helper.EntroptPool[(int)(k / 2) - i + 500] / 16;
                //随机数，来自熵池
                float sizeFix = i + 1;
                sizeFix /= safelength;
                sizeFix = 1 - sizeFix;
                //实际上修复颜色
                if (projectile.ai[0] == 1 && (
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
                    for (int j = 0; j < 15; j++)
                    {
                        Color color = Color.White;
                        if (Helper.Specialname2Color(Main.player[projectile.owner].GetModPlayer<RevolutionsPlayer>().spname) == Color.White)
                        {
                            color = Helper.GetCloserColor(Helper.GetRainbowColorLinear((int)safelength * 15 - i * 15 - j + 180, (int)safelength * 15 + 200), Color.White, 6, 7);
                        }
                        else
                        {
                            color = Helper.Specialname2Color(Main.player[projectile.owner].GetModPlayer<RevolutionsPlayer>().spname);
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
            Main.player[projectile.owner].GetModPlayer<RevolutionsPlayer>().lightningproj = false;
        }
    }
}
