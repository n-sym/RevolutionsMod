using Microsoft.Xna.Framework;
using Revolutions.Utils;
using System;
using System.Collections.Generic;
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
            projectile.width = 30;
            projectile.height = 30;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.alpha = 255;
            projectile.penetrate = 1000;
            projectile.ignoreWater = true;
            projectile.extraUpdates = 2;
            projectile.ranged = true;
            projectile.timeLeft = 60;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
        }
        List<NPC> hited = new List<NPC>();
        NPC tar = null;
        public override void AI()
        {
            float distance = 500f;
            foreach(NPC npc in Main.npc)
            {
                if(npc.active && Vector2.Distance(npc.Center, projectile.position) < distance && !hited.Contains(npc) && npc.CanBeChasedBy())
                {
                    distance = Vector2.Distance(npc.Center, projectile.position);
                    tar = npc;
                }
            }
            if(tar != null)
            {
                projectile.velocity = Helper.GetCloser(Helper.ToUnitVector(projectile.velocity), Helper.ToUnitVector(tar.Center - projectile.Center + tar.velocity * 15), 1, 30) * projectile.velocity.Length();
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
            int a = target.lifeMax;
            if (!hited.Contains(target)) hited.Add(target);
            if (RevolutionsPlayer.nowBoss != null && target.boss) a = RevolutionsPlayer.nowBossLifeMax;
            if (a / 12000 > 5 && a / 12000 < 20 && target.type != NPCID.TargetDummy)
            {
                Projectile.NewProjectile(target.Center, Vector2.Zero, ModContent.ProjectileType<JustDamageRanged>(), a / 10000, 0, projectile.owner);
            }
            else if (a / 10000 < 5 || target.type == NPCID.TargetDummy)
            {
                Projectile.NewProjectile(target.Center, Vector2.Zero, ModContent.ProjectileType<JustDamageRanged>(), 5, 0, projectile.owner);
            }
            else if (a / 10000 > 20)
            {
                Projectile.NewProjectile(target.Center, Vector2.Zero, ModContent.ProjectileType<JustDamageRanged>(), 20, 0, projectile.owner);
            }
        }
    }
}
