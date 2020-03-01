using Microsoft.Xna.Framework;
using Revolutions.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolutions.Projectiles.ForWater
{
    public class Fire1 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("DevoTools");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.alpha = 255;
            projectile.penetrate = 5;
            projectile.extraUpdates = 2;
            projectile.ranged = true;
        }
        public override void AI()
        {
            if (projectile.wet) projectile.Kill();
            if (projectile.timeLeft > 60)
            {
                projectile.timeLeft = 60;
            }

            if (projectile.ai[0] > 7f)
            {
                float dustscalefix = 1f;
                if (projectile.ai[0] == 8f)
                {
                    dustscalefix = 0.25f;
                }
                else if (projectile.ai[0] == 9f)
                {
                    dustscalefix = 0.5f;
                }
                else if (projectile.ai[0] == 10f)
                {
                    dustscalefix = 0.75f;
                }

                projectile.ai[0] += 1f;
                int b = 6;
                if (b == 6 || Main.rand.Next(2) == 0)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        var pos = new Vector2(projectile.position.X, projectile.position.Y);
                        var width = projectile.width;
                        var height = projectile.height;
                        var speedX = projectile.velocity.X * 0.2f;
                        var speedY = projectile.velocity.Y * 0.2f;
                        var dust = Dust.NewDust(pos, width, height, MyDustId.BlueTorch, speedX, speedY, 0,
                            Color.White, 1f);
                        if (Main.rand.Next(3) != 0 || (b == 75 && Main.rand.Next(3) == 0))
                        {
                            Main.dust[dust].noGravity = true;
                            Main.dust[dust].scale *= 3f;
                            Main.dust[dust].velocity.X *= 2f;
                            Main.dust[dust].velocity.Y *= 2f;
                        }
                        Main.dust[dust].scale *= 1.25f;
                        Main.dust[dust].velocity.X *= 1.2f;
                        Main.dust[dust].velocity.Y *= 1.2f;
                        Main.dust[dust].scale *= dustscalefix;
                        if (b == 75)
                        {
                            Main.dust[dust].velocity += projectile.velocity;
                            if (!Main.dust[dust].noGravity)
                            {
                                Main.dust[dust].velocity *= 0.5f;
                            }
                        }
                    }
                }
            }
            else
            {
                projectile.ai[0] += 1f;
            }

            projectile.rotation += 0.3f * (float)projectile.direction;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(44, 240);
        }
    }
}
