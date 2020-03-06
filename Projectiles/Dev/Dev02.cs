using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolutions.Projectiles.Dev
{
    public class Dev02 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("DevoTools");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            projectile.width = 18;
            projectile.height = 18;
            projectile.scale = 0.5f;
            projectile.friendly = true;
            projectile.timeLeft = 180;
            projectile.tileCollide = true;
            projectile.alpha = 255;
            projectile.ignoreWater = true;
            aiType = 1;
        }
        private int rippleCount = 1;
        private int rippleSize = 10;
        private int rippleSpeed = 5;
        private float distortStrength = 200f;

        public override void AI()
        {
            // ai[0] = state
            // 0 = unexploded
            // 1 = exploded

            if (projectile.timeLeft <= 180)
            {
                if (projectile.ai[0] == 0)
                {
                    projectile.ai[0] = 1; // Set state to exploded
                    projectile.alpha = 255; // Make the projectile invisible.
                    projectile.friendly = false; // Stop the bomb from hurting enemies.

                    if (Main.netMode != NetmodeID.Server && !Filters.Scene["Blur"].IsActive())
                    {
                        Filters.Scene.Activate("Blur", projectile.Center).GetShader().UseColor(rippleCount, rippleSize, rippleSpeed).UseTargetPosition(projectile.Center);
                    }
                }

                if (Main.netMode != NetmodeID.Server && Filters.Scene["Shockwave"].IsActive())
                {
                    float progress = (180f - projectile.timeLeft) / 60f;
                    Filters.Scene["Shockwave"].GetShader().UseProgress(progress).UseOpacity(distortStrength * (1 - progress / 3f));
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            if (Main.netMode != NetmodeID.Server && Filters.Scene["Shockwave"].IsActive())
            {
                Filters.Scene["Shockwave"].Deactivate();
            }
        }
    }
}
