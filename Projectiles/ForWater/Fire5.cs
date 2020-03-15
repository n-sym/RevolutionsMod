using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolutions.Projectiles.ForWater
{
    public class Fire5 : ModProjectile
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
            projectile.penetrate = 10;
            projectile.ignoreWater = true;
            projectile.extraUpdates = 2;
            projectile.ranged = true;
            projectile.timeLeft = 60;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 1;
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
                for (int i = 0; i < 2; i++)
                {
                    var pos = new Vector2(projectile.position.X, projectile.position.Y);
                    var width = projectile.width;
                    var height = projectile.height;
                    var speedX = projectile.velocity.X * 0.2f;
                    var speedY = projectile.velocity.Y * 0.2f;
                    var dust = Dust.NewDust(pos, width, height, DustID.Vortex, speedX, speedY, 0,
                        Color.White, 0.9f);
                    if (Main.rand.Next(3) != 0)
                    {
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].scale *= 3f;
                        Main.dust[dust].velocity.X *= 2f;
                        Main.dust[dust].velocity.Y *= 2f;
                    }
                    Main.dust[dust].velocity.X *= 0.5f;
                    Main.dust[dust].velocity.Y *= 0.5f;
                    Main.dust[dust].scale *= dustscalefix * 0.8f;
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
            if (target.lifeMax / 12000 > 5 && target.lifeMax / 12000 < 20 && target.type != NPCID.TargetDummy)
            {
                Projectile.NewProjectile(target.Center, Vector2.Zero, ModContent.ProjectileType<JustDamageRanged>(), target.lifeMax / 10000, 0, projectile.owner);
            }
            else if (target.lifeMax / 10000 < 5 || target.type == NPCID.TargetDummy)
            {
                Projectile.NewProjectile(target.Center, Vector2.Zero, ModContent.ProjectileType<JustDamageRanged>(), 5, 0, projectile.owner);
            }
            else if (target.lifeMax / 10000 > 20)
            {
                Projectile.NewProjectile(target.Center, Vector2.Zero, ModContent.ProjectileType<JustDamageRanged>(), 20, 0, projectile.owner);
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            var a = Math.Sqrt(new Vector2(target.Hitbox.Width, target.Hitbox.Height).Length());
            damage = (int)(damage * 6.557 / a);
        }
    }
}
