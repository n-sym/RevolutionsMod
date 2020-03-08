using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Revolutions.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolutions.Projectiles
{
    public class WaterArrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ultimate Star");
        }
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.extraUpdates = 1;
            projectile.hostile = true;
            projectile.timeLeft = 600;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.scale = 1f;
            projectile.alpha = 255;
            projectile.aiStyle = -1;
        }
        public override void AI()
        {
            for(int i = 0;i < 10; i++)
            {
                Dust d = Dust.NewDustDirect(projectile.Center, 14, 14, MyDustId.Water);
                d.noGravity = true;
                Dust e = Dust.NewDustDirect(projectile.Center, 14, 14, MyDustId.BlueCircle);
                e.noGravity = true;
            }
        }
    }
}
