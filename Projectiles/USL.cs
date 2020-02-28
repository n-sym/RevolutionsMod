using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolutions.Projectiles
{
    public class USL : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("USForLogcation");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.extraUpdates = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.timeLeft = 600;
            projectile.tileCollide = true;
            projectile.alpha = 255;
            projectile.ignoreWater = true;
            projectile.aiStyle = 1;
            aiType = ProjectileID.Bullet;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[projectile.owner];
            int lifeplus2 = 0;
            int damage2 = projectile.damage / 2;
            Random rd = new Random();
            if (damage2 == 0)
            {
                lifeplus2 = 1;
            }
            if (damage2 < 1)
            {
                lifeplus2 = 1;
            }
            if (damage2 > 1)
            {
                lifeplus2 = damage2;
            }
            if (lifeplus2 > 4)
            {
                lifeplus2 = 4;
            }
            lifeplus2 = lifeplus2 + rd.Next(-2, 3);
            if (lifeplus2 == 0)
            {
                lifeplus2 = 1;
            }
            if (lifeplus2 < 0)
            {
                lifeplus2 = 1;
            }
            if (target.target != NPCID.TargetDummy)
            {
                if (player.statLife < player.statLifeMax2)
                {
                    player.statLife = lifeplus2 + player.statLife;
                    player.HealEffect(lifeplus2);
                }
            }

        }
    }
}
