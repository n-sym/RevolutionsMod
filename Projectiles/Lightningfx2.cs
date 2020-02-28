using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Revolutions.Utils;
using System;
using Terraria;

namespace Revolutions.Projectiles
{
    public class Lightningfx2 : PowerProj
    {
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 40;
            projectile.friendly = true;
            projectile.timeLeft = 31;
            projectile.tileCollide = false;
            projectile.alpha = 255;
            projectile.ignoreWater = true;
            projectile.aiStyle = -1;
            projectile.scale = 1f;
            projectile.penetrate = 2000;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
        }
        int fix = 0;
        public override void AI()
        {
        }
        int b = 0;
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(1f, 1f);
            if (projectile.timeLeft == 30)
            {
                PositionSave[0].X = projectile.position.X / 16;
            }

            if (PositionSave[0].X > 9000) PositionSave[0].X = 0;
            float a = projectile.timeLeft;
            a = a * a * 0.015f;
            for (int i = 0; i < 16 - a; i++)
            {
                Vector2 target = Helper.GetCloser(projectile.position.X, projectile.position.Y + 23, projectile.ai[0], projectile.ai[1], i, 15);
                target.X += Helper.EntroptPool[i + 1 + (int)PositionSave[0].X] / 3;
                target.Y += Helper.EntroptPool[i + 1 + (int)PositionSave[0].X] / 8;
                Vector2 current = Helper.GetCloser(projectile.position.X, projectile.position.Y + 23, projectile.ai[0], projectile.ai[1], i - 1, 15);
                current.X += Helper.EntroptPool[i + (int)PositionSave[0].X] / 3;
                current.Y += Helper.EntroptPool[i + (int)PositionSave[0].X] / 8;
                for (int j = 0; j < 31; j++)
                {
                    Random rd = new Random();
                    Color color = Color.White;
                    if (Helper.Specialname2Color(Helper.spname) == Color.White)
                    {
                        color = Helper.GetCloserColor(Helper.GetRainbowColorLinear(470 - (j + i * 30), 470 + rd.Next(-100, 100)), Color.White, 9, 10);
                    }
                    else
                    {
                        color = Helper.Specialname2Color(Helper.spname);
                    }
                    float sizeFix = i + 1;
                    sizeFix /= 15;
                    color = Color.Multiply(color, sizeFix * projectile.timeLeft / 30);
                    spriteBatch.Draw(Main.projectileTexture[mod.ProjectileType("MeteowerHelper")], Helper.GetCloser(current, target, j, 30) - Main.screenPosition, null, Color.Multiply(color, 1), projectile.rotation, drawOrigin, 0.19f, SpriteEffects.None, 0f);
                    Lighting.AddLight(Helper.GetCloser(current, target, j, 20), color.R / 245, color.G / 245, color.B / 245);
                    Lighting.AddLight(Helper.GetCloser(current, target, j, 20), 0.55f, 0.55f, 0.55f);
                }

            }
            return true;
        }
    }
}
