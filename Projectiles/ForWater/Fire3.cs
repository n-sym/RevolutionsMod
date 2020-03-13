using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Revolutions.Projectiles.ForWater
{
    public class Fire3 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("FrozenFlare");
        }
        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.alpha = 255;
            projectile.penetrate = 5;
            projectile.ignoreWater = true;
            projectile.extraUpdates = 2;
            projectile.ranged = true;
            projectile.timeLeft = 60;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 10;
        }
        public override void AI()
        {
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
                for (int i = 0; i < 3; i++)
                {
                    var pos = new Vector2(projectile.position.X, projectile.position.Y);
                    var width = projectile.width;
                    var height = projectile.height;
                    var speedX = projectile.velocity.X * 0.2f;
                    var speedY = projectile.velocity.Y * 0.2f;
                    var dust = Dust.NewDust(pos, width, height, 170, speedX, speedY, 0,
                        Color.White, 1f);
                    if (Main.rand.Next(3) != 0)
                    {
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].scale *= 2f;
                        Main.dust[dust].velocity.X *= 2f;
                        Main.dust[dust].velocity.Y *= 2f;
                    }
                    Main.dust[dust].velocity.X *= 0.5f;
                    Main.dust[dust].velocity.Y *= 0.5f;
                    Main.dust[dust].scale *= dustscalefix;
                    Main.dust[dust].noGravity = true;
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
            target.AddBuff(69, 240);
        }
    }
}
