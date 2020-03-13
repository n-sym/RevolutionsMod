using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolutions.Projectiles
{
    public class JustDamageRanged : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vortex Flame");
        }
        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;
            projectile.ranged = true;
            projectile.extraUpdates = 1;
            projectile.friendly = true;
            projectile.timeLeft = 3;
            projectile.tileCollide = false;
            projectile.alpha = 255;
            projectile.ignoreWater = true;
            projectile.aiStyle = -1;
            projectile.scale = 1f;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage += 1 + target.defense / 2;
        }
    }
}
