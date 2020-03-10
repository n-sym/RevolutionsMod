using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Revolutions.Utils;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolutions.Projectiles
{
    public class CoreFx : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("FX");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            projectile.width = 1;
            projectile.height = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.timeLeft = 16;
            projectile.alpha = 255;
            projectile.ignoreWater = true;
            projectile.aiStyle = -1;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            projectile.position = Helper.GetCloser(projectile.position, player.itemLocation + 0.5f * new Vector2(player.direction * player.itemWidth, player.itemHeight), 16 - projectile.timeLeft, 40) + player.velocity;

        }
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width, projectile.height);
            for (int k = 0; k < projectile.oldPos.Length - 1; k++)
            {
                Vector2 drawPositiona = 0.45f * projectile.oldPos[k] + 0.55f * projectile.position;
                drawPositiona -= Main.screenPosition - drawOrigin;
                Vector2 drawPositionb = 0.45f * projectile.oldPos[k + 1] + 0.55f * projectile.position;
                drawPositionb -= Main.screenPosition - drawOrigin;
                if (projectile.oldPos[k + 1] == Vector2.Zero) drawPositionb += 0.45f * projectile.oldPos[k];
                else if (drawPositionb == Vector2.Zero) drawPositionb = drawPositiona;
                float sizeFix = k + 1;
                sizeFix /= projectile.oldPos.Length;
                sizeFix = 1 - sizeFix;
                Random rd = new Random();
                int a = rd.Next(0, 20);
                int b = rd.Next(1, 2);
                Color color = Helper.GetCloserColor(Helper.GetRainbowColorLinear(k + a, 18 + (b * a)), Color.White, 5, 6);
                color = Color.Multiply(color, sizeFix / 2.5f);
                for (int i = 0; i < 9; i++)
                {
                    spriteBatch.Draw(Main.projectileTexture[ModContent.ProjectileType<RareWeapon.MeteowerHelper>()], Helper.GetCloser(drawPositiona, drawPositionb, i, 8), null,
                    color, projectile.rotation, drawOrigin, projectile.scale * 0.17f, SpriteEffects.None, 0f);
                }
            }
        }
    }
}
