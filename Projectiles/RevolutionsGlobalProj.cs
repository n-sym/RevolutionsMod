using Revolutions.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolutions.NPCs
{
    public class RevolutionsGlobalProj : GlobalProjectile
    {
        public override void SetDefaults(Projectile projectile)
        {
            switch (projectile.type)
            {
                case ProjectileID.PulseBolt:
                    projectile.penetrate = 30;
                    projectile.usesLocalNPCImmunity = true;
                    projectile.localNPCHitCooldown = 5;
                    projectile.timeLeft = 450;
                    break;
                case ProjectileID.TerraBeam:
                    projectile.penetrate = 10;
                    projectile.usesLocalNPCImmunity = true;
                    projectile.localNPCHitCooldown = 1;
                    break;
            }
        }
        public override void AI(Projectile projectile)
        {
            switch (projectile.type)
            {

            }
        }
        public override void ModifyHitNPC(Projectile projectile, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            switch (projectile.type)
            {
                case ProjectileID.PulseBolt:
                    Main.player[projectile.owner].ApplyDamageToNPC(target, damage, 0, 0, crit);
                    Main.player[projectile.owner].dpsDamage += damage;
                    damage *= 2;
                    break;
                case ProjectileID.TerraBeam:
                    projectile.damage /= 10;
                    projectile.damage *= 9;
                    break;
                case ProjectileID.FallingStar:
                    if (Main.rand.Next(100) < Main.player[projectile.owner].rangedCrit) crit = true;
                    break;
            }
        }
    }
}
