using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Revolutions.Utils;
using System;
using Terraria;
using Terraria.ModLoader;

namespace Revolutions.Projectiles.CoreWeapon
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
            float a = (Vector2.Distance(projectile.position, new Vector2(projectile.ai[0], projectile.ai[1])) / 50);
            for (int i = 0; i < 24 - a; i++)
            {

                Vector2 target = Helper.GetCloser(projectile.ai[0] + 600 + Helper.EntroptPool[projectile.damage], projectile.ai[1] - 800 + Helper.EntroptPool[projectile.damage + 100], projectile.ai[0], projectile.ai[1], i, 23);
                target.X += Helper.EntroptPool[i + 1 + (int)PositionSave[0].X] / 5;
                target.Y += Helper.EntroptPool[i + 1000 + (int)PositionSave[0].X] / 4;
                Vector2 current = Helper.GetCloser(projectile.ai[0] + 600 + Helper.EntroptPool[projectile.damage], projectile.ai[1] - 800 + Helper.EntroptPool[projectile.damage + 100], projectile.ai[0], projectile.ai[1], i - 1, 23);
                current.X += Helper.EntroptPool[i + (int)PositionSave[0].X] / 5;
                current.Y += Helper.EntroptPool[i + 999 + (int)PositionSave[0].X] / 4;
                Random rd = new Random();
                Color color = Color.White;

                for (int j = 0; j < 30; j++)
                {
                    if (Helper.Specialname2Color(Main.player[projectile.owner].GetModPlayer<RevolutionsPlayer>().spname) == Color.White)
                    {
                        color = Helper.GetCloserColor(Helper.GetRainbowColorLinear(j + i * 30 + 500, 1720 + rd.Next(-400, 0)), Color.White, 8, 9);
                    }
                    else
                    {
                        color = Helper.Specialname2Color(Main.player[projectile.owner].GetModPlayer<RevolutionsPlayer>().spname);
                    }
                    float sizeFix = i + 1;
                    sizeFix /= 24 / Main.player[projectile.owner].meleeSpeed;
                    color = Color.Multiply(color, sizeFix * projectile.timeLeft / 30);
                    spriteBatch.Draw(Main.projectileTexture[ModContent.ProjectileType<RareWeapon.MeteowerHelper>()], Helper.GetCloser(current, target, j, 30) - Main.screenPosition, null, color, projectile.rotation, drawOrigin, 0.19f * (1.3f - sizeFix), SpriteEffects.None, 0f);
                    Lighting.AddLight(Helper.GetCloser(current, target, j, 20), color.R / 245, color.G / 245, color.B / 245);
                    Lighting.AddLight(Helper.GetCloser(current, target, j, 20), 0.55f, 0.55f, 0.55f);
                }

            }
            return true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if(projectile.penetrate == 2000 && Main.rand.Next(3) == 0)Projectile.NewProjectile(projectile.position, Vector2.Zero, ModContent.ProjectileType<ThunderAlt>(), projectile.damage / 16, 0.1f, projectile.owner, projectile.position.X, projectile.position.Y);
        }
    }
}
