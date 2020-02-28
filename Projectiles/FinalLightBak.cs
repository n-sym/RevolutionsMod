using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Revolutions.Utils;
using System;
using Terraria;
using Terraria.ID;

namespace Revolutions.Projectiles
{
    public class FinalLightBak : PowerProj
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("LightArrow");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;
            projectile.friendly = true;
            projectile.extraUpdates = 1;
            projectile.ranged = true;
            projectile.timeLeft = 600;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.light = 1.1f;
            projectile.penetrate = 20;
            projectile.aiStyle = 1;
            projectile.arrow = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
        }
        int j = 0;
        int i = 0;

        NPC t = null;
        public override void AI()
        {

        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.damage = (int)(projectile.damage * 0.9f);
            Random rd = new Random();
            int a = rd.Next(5, 15);
            int b = rd.Next(1, 2);
            Vector2 pos = 2 * target.Center - projectile.position;
            pos.Y += (target.Center.Y - pos.Y) * 2;
            /*Dust f = Dust.NewDustDirect(projectile.position, 0, 0,
                mod.DustType("hyperbola3"), 0, 0, 0, new Color(233, 233, 255), 0.6f);
            f.rotation *= a;*/
            for (int i = 0; i < 15; i++)
            {
                Dust d = Dust.NewDustDirect(projectile.position, 1, 1, MyDustId.WhiteTrans, 0.4f * projectile.velocity.X, 0.4f * projectile.velocity.Y, 100, Helper.GetCloserColor(Helper.GetRainbowColorLinear(i + a, 30 + (b * a)), Color.White, 1, 5), 0.8f);
                d.noGravity = true;
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {

        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            Player player = Main.player[projectile.owner];
            for (int k = 1; k < projectile.oldPos.Length; k++)
            {
                for (int i = 0; i < 2; i++)
                {
                    Vector2 drawPosition = Helper.GetCloser(projectile.oldPos[k - 1], projectile.oldPos[k], i, 2) - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                    Color color = projectile.GetAlpha(lightColor);
                    color *= ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length) * 0.18f;
                    if (Vector2.Distance(projectile.position, player.position) < 0.5f * Vector2.Distance(Vector2.Zero, new Vector2(Main.screenWidth, Main.screenHeight)))
                    {
                        spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPosition, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
                        spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPosition + new Vector2(2, 2), null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
                        spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPosition + new Vector2(-2, 2), null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
                        spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPosition + new Vector2(2, -2), null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
                        spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPosition + new Vector2(-2, -2), null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
                    }
                }
            }
            return true;
        }
        public override void Kill(int timeLeft)
        {
            base.Kill(timeLeft);
            Random rd = new Random();
            int a = rd.Next(5, 15);
            int b = rd.Next(1, 2);
            for (int i = 0; i < 14; i++)
            {
                Dust d = Dust.NewDustDirect(projectile.position, 2, 2, MyDustId.WhiteTrans, 0.6f * projectile.velocity.X, -0.6f * projectile.velocity.Y, 100, Helper.GetCloserColor(Helper.GetRainbowColorLinear(i + a, 30 + (b * a)), Color.White, 1, 5), 0.8f);
                d.noGravity = true;

                Dust f = Dust.NewDustDirect(projectile.position, 2, 2, MyDustId.WhiteTrans, 0, 0, 100, Helper.GetCloserColor(Helper.GetRainbowColorLinear(i + a, 30 + (b * a)), Color.White, 1, 5), 0.8f);
                f.noGravity = true;

            }
        }

    }
}
