using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Revolutions.Projectiles.RareWeapon
{
    public class MeteowerHelper : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Meteower Meteor");
            //ProjectileID.Sets.TrailCacheLength[projectile.type] = 5; 
            //ProjectileID.Sets.TrailingMode[projectile.type] = 0; 
        }
        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.timeLeft = 15;
            projectile.tileCollide = true;
            projectile.alpha = 255;
            projectile.ignoreWater = true;
            projectile.aiStyle = -1;
            projectile.scale = 0.6f;
            projectile.light = 0.35f;
        }
        int d;
        Vector2 s;
        public override void AI()
        {
            if (d == 0)
            {
                d = projectile.damage;
                s = projectile.velocity;
            }
            projectile.velocity = Vector2.Zero;
            projectile.damage = 0;
            Player player = Main.player[projectile.owner];
            Vector2 Speed2 = s.RotatedByRandom(MathHelper.ToRadians(90));
            Projectile.NewProjectile(projectile.position.X, projectile.position.Y, Speed2.X, Speed2.Y, mod.ProjectileType("Meteower_Meteor"), d, projectile.knockBack, player.whoAmI, s.X, s.Y);
            Lighting.AddLight(player.Center, 126 * 0.0035f, 171 * 0.0035f, 243 * 0.0035f);
        }
    }
}
