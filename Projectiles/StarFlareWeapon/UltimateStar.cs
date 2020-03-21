using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Revolutions.Utils;
using Terraria;
using Terraria.ModLoader;

namespace Revolutions.Projectiles.StarFlareWeapon
{
    public class UltimateStar : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ultimate Star");
        }
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.extraUpdates = 2;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.timeLeft = 600;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.aiStyle = -1;
            projectile.scale = 1f;
            projectile.alpha = 255;
            projectile.penetrate = -1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;

        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            //这里以后必有特殊效果




        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.damage = (int)(0.9f * projectile.damage);
            Vector2 pos = 2 * target.Center - projectile.position;
            pos.Y += (target.Center.Y - pos.Y) * 2;
            Color color = Main.player[projectile.owner].GetModPlayer<RevolutionsPlayer>().starFlareColor;
            color = Helper.GetCloserColor(Helper.GetCloserColor(color, Helper.ToGreyColor(color), -2, 1), Color.Black, 4, 7); 
            for (int i = 0; i < 7; i++)
            {
                Dust d = Dust.NewDustDirect(pos, 4, 4, MyDustId.WhiteTrans, 0.6f * projectile.velocity.X, 0.6f * projectile.velocity.Y, 100, Color.White, 0.8f);
                d.noGravity = true;
                Dust e = Dust.NewDustDirect(pos, 4, 4, MyDustId.WhiteTrans, 0.6f * projectile.velocity.X, 0.6f * projectile.velocity.Y, 100, color, 0.8f);
                e.noGravity = true;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width, projectile.height);
            Player player = Main.player[projectile.owner];
            for (int k = 0; k < 25; k++)
            {
                Vector2 drawPosition = Helper.GetCloser(projectile.position - projectile.velocity, projectile.position + projectile.velocity, k, 20);
                drawPosition += drawOrigin - Main.screenPosition + new Vector2(0f, projectile.gfxOffY);
                float sizeFix = 2;
                sizeFix /= 1 + k;
                sizeFix -= 1;
                //实际上修复颜色
                if (Vector2.Distance(projectile.position, player.position) < 0.5f * Vector2.Distance(Vector2.Zero, new Vector2(Main.screenWidth, Main.screenHeight)))
                {
                    Color color = Main.player[projectile.owner].GetModPlayer<RevolutionsPlayer>().starFlareColor;
                    color = new Color(color.R, color.G, color.B, (int)(255 * sizeFix));
                    spriteBatch.Draw(Main.projectileTexture[ModContent.ProjectileType<RareWeapon.MeteowerHelper>()], drawPosition, null, color, projectile.rotation, drawOrigin, projectile.scale * 0.25f, SpriteEffects.None, 0f);
                    Lighting.AddLight(drawPosition, color.R / 245, color.G / 245, color.B / 245);
                }
            }
            return true;
        }

        public override void Kill(int timeLeft)
        {
            base.Kill(timeLeft);
            Color color = Main.player[projectile.owner].GetModPlayer<RevolutionsPlayer>().starFlareColor;
            color = Helper.GetCloserColor(Helper.GetCloserColor(color, Helper.ToGreyColor(color), -2, 1), Color.Black, 4, 7);
            for (int i = 0; i < 7; i++)
            {
                Dust d = Dust.NewDustDirect(projectile.position, 2, 2, MyDustId.WhiteTrans, 0.6f * projectile.velocity.X, -0.6f * projectile.velocity.Y, 100, Color.White, 0.8f);
                d.noGravity = true;
                Dust f = Dust.NewDustDirect(projectile.position, 2, 2, MyDustId.WhiteTrans, 0, 0, 100, Color.White, 0.8f);
                f.noGravity = true;
                Dust e = Dust.NewDustDirect(projectile.position, 2, 2, MyDustId.WhiteTrans, 0.6f * projectile.velocity.X, -0.6f * projectile.velocity.Y, 100, color, 0.8f);
                e.noGravity = true;
                Dust g = Dust.NewDustDirect(projectile.position, 2, 2, MyDustId.WhiteTrans, 0, 0, 100, color, 0.8f);
                g.noGravity = true;
            }
        }
    }
}
