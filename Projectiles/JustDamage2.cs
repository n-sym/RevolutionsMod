using Terraria.ID;
using Terraria.ModLoader;

namespace Revolutions.Projectiles
{
    public class JustDamage2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Damage");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;
            projectile.extraUpdates = 1;
            projectile.friendly = true;
            projectile.timeLeft = 3;
            projectile.tileCollide = false;
            projectile.alpha = 255;
            projectile.ignoreWater = true;
            projectile.aiStyle = -1;
            projectile.scale = 1f;
        }
    }
}
