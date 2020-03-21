using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Revolutions.Utils;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolutions.Projectiles.StarFlareWeapon
{
    public class USFx : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("FX");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            projectile.width = 1;
            projectile.height = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.timeLeft = 50;
            projectile.alpha = 255;
            projectile.ignoreWater = true;
            projectile.aiStyle = -1;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            projectile.position = Helper.GetCloser(projectile.position, player.Center + player.direction * new Vector2(player.HeldItem.width, player.HeldItem.height).RotatedBy(player.itemRotation) + player.velocity, 1, 20 - projectile.ai[0]);

        }
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width, projectile.height);
            Player player = Main.player[projectile.owner];
            Vector2 Center = player.itemLocation + 0.5f * new Vector2(player.itemWidth, player.itemHeight) + player.velocity - Main.screenPosition;
            for (int k = 1; k < projectile.oldPos.Length - 1; k++)
            {
                if (projectile.oldPos[k - 1] == Vector2.Zero) break;
                Vector2 drawPositiona = 0.45f * projectile.oldPos[k] + 0.55f * projectile.position;
                drawPositiona -= Main.screenPosition - drawOrigin;
                Vector2 drawPositionb = 0.45f * projectile.oldPos[k + 1] + 0.55f * projectile.position;
                drawPositionb -= Main.screenPosition - drawOrigin;
                if (projectile.oldPos[k + 1] == Vector2.Zero) drawPositionb += 0.45f * projectile.oldPos[k];
                else if (drawPositionb == Vector2.Zero) drawPositionb = drawPositiona;
                float sizeFix = k + 1;
                if (k < projectile.oldPos.Length / 2)
                {
                    sizeFix /= 0.5f * projectile.oldPos.Length;
                }
                else
                {
                    sizeFix -= projectile.oldPos.Length / 2;
                    sizeFix /= 0.5f * projectile.oldPos.Length;
                    sizeFix = 1 - sizeFix;
                }
                Color color = Main.player[projectile.owner].GetModPlayer<RevolutionsPlayer>().starFlareColor;
                color = Color.White;
                //color = Color.Multiply(color, sizeFix);
                for (int i = 0; i < 9; i++)
                {
                    Vector2 targetpos = Helper.GetCloser(drawPositiona, drawPositionb, i, 8);
                    spriteBatch.Draw(Main.projectileTexture[ModContent.ProjectileType<RareWeapon.MeteowerHelper>()], targetpos, null,
                    color, projectile.rotation, drawOrigin, 0.125f * sizeFix, SpriteEffects.None, 0f);
                    Lighting.AddLight(targetpos + Main.screenPosition, color.R / 245, color.G / 245, color.B / 245);
                    Lighting.AddLight(targetpos + Main.screenPosition, 0.55f, 0.55f, 0.55f);
                }
                /*for (int i = 0; i < 9; i++)
                {
                    Vector2 targetpos = Helper.GetCloser(drawPositiona, drawPositionb, i, 8);
                    targetpos.X += 2 * (Center.X - targetpos.X);
                    spriteBatch.Draw(Main.projectileTexture[ModContent.ProjectileType<RareWeapon.MeteowerHelper>()], targetpos, null,
                    color, projectile.rotation, drawOrigin, 0.125f, SpriteEffects.None, 0f);
                    Lighting.AddLight(targetpos + Main.screenPosition, color.R / 245, color.G / 245, color.B / 245);
                    Lighting.AddLight(targetpos + Main.screenPosition, 0.55f, 0.55f, 0.55f);
                }
                for (int i = 0; i < 9; i++)
                {
                    Vector2 targetpos = Helper.GetCloser(drawPositiona, drawPositionb, i, 8);
                    targetpos.Y += 2 * (Center.Y - targetpos.Y);
                    spriteBatch.Draw(Main.projectileTexture[ModContent.ProjectileType<RareWeapon.MeteowerHelper>()], targetpos, null,
                    color, projectile.rotation, drawOrigin, 0.125f, SpriteEffects.None, 0f);
                    Lighting.AddLight(targetpos + Main.screenPosition, color.R / 245, color.G / 245, color.B / 245);
                    Lighting.AddLight(targetpos + Main.screenPosition, 0.55f, 0.55f, 0.55f);
                }
                for (int i = 0; i < 9; i++)
                {
                    Vector2 targetpos = Helper.GetCloser(drawPositiona, drawPositionb, i, 8);
                    targetpos += 2 * (Center - targetpos);
                    spriteBatch.Draw(Main.projectileTexture[ModContent.ProjectileType<RareWeapon.MeteowerHelper>()], targetpos, null,
                    color, projectile.rotation, drawOrigin, 0.125f, SpriteEffects.None, 0f);
                    Lighting.AddLight(targetpos + Main.screenPosition, color.R / 245, color.G / 245, color.B / 245);
                    Lighting.AddLight(targetpos + Main.screenPosition, 0.55f, 0.55f, 0.55f);
                }*/
            }
        }
    }
}
