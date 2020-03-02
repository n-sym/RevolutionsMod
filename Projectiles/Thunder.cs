using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Revolutions.Utils;
using System;
using Terraria;

namespace Revolutions.Projectiles
{
    public class Thunder : PowerProj
    {
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 40;
            projectile.melee = true;
            projectile.friendly = true;
            projectile.timeLeft = 30;
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
            projectile.position = Helper.GetCloser(projectile.ai[0] + 600, projectile.ai[1] - 800, projectile.ai[0], projectile.ai[1], fix, 15);

            if (fix < 15)
            {
                fix++;
                projectile.timeLeft++;
            }

        }
        int b = 0;
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(1f, 1f);
            if (projectile.timeLeft == 30)
            {
                PositionSave[0].X = projectile.whoAmI + projectile.damage + (int)(projectile.ai[0] / 16);
            }
            if (PositionSave[0].X > 9000) PositionSave[0].X = 0;
            float a = (Vector2.Distance(projectile.position, new Vector2(projectile.ai[0], projectile.ai[1])) / 67);
            for (int i = 0; i < 16 - a; i++)
            {

                Vector2 target = Helper.GetCloser(projectile.ai[0] + 600 + Helper.EntroptPool[projectile.damage], projectile.ai[1] - 800 + Helper.EntroptPool[projectile.damage + 100], projectile.ai[0], projectile.ai[1], i, 15);
                target.X += Helper.EntroptPool[i + 1 + (int)PositionSave[0].X] / 3;
                target.Y += Helper.EntroptPool[i + 1000 + (int)PositionSave[0].X] / 4;
                Vector2 current = Helper.GetCloser(projectile.ai[0] + 600 + Helper.EntroptPool[projectile.damage], projectile.ai[1] - 800 + Helper.EntroptPool[projectile.damage + 100], projectile.ai[0], projectile.ai[1], i - 1, 15);
                current.X += Helper.EntroptPool[i + (int)PositionSave[0].X] / 3;
                current.Y += Helper.EntroptPool[i + 999 + (int)PositionSave[0].X] / 4;
                Random rd = new Random();
                Color color = Color.White;

                for (int j = 0; j < 30; j++)
                {
                    if (Helper.Specialname2Color(Helper.spname) == Color.White)
                    {
                        color = Helper.GetCloserColor(Helper.GetRainbowColorLinear(450 - (j + i * 30), 450 + rd.Next(-100, 100)), Color.White, 9, 10);
                    }
                    else
                    {
                        color = Helper.Specialname2Color(Helper.spname);
                    }
                    float sizeFix = i + 1;
                    sizeFix /= 15 / Main.player[projectile.owner].meleeSpeed;
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
