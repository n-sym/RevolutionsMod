using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolutions.Projectiles.Dev
{
    public class Dev01 : ModProjectile
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
            projectile.timeLeft = 600;
            projectile.tileCollide = true;
            projectile.alpha = 0;
            projectile.ignoreWater = true;
            aiType = 1;
        }
        public override void AI()
        {
            int x = (int)(projectile.Center.X / 16);
            int y = (int)(projectile.Center.Y / 16);
            Main.tile[x, y].ClearEverything();
            Main.PlaySound(SoundID.Dig, projectile.position);
            Main.tile[x, y + 1].ClearEverything(); Main.tile[x + 1, y].ClearEverything(); Main.tile[x - 1, y].ClearEverything(); Main.tile[x, y - 1].ClearEverything();
            Main.tile[x + 1, y + 1].ClearEverything(); Main.tile[x + 1, y - 1].ClearEverything(); Main.tile[x - 1, y + 1].ClearEverything(); Main.tile[x - 1, y - 1].ClearEverything();
        }
        public override void Kill(int timeLeft)
        {
            int x = (int)(projectile.Center.X / 16);
            int y = (int)(projectile.Center.Y / 16);
            Main.tile[x, y].ClearEverything();
            Main.PlaySound(SoundID.Dig, projectile.position);
            Main.tile[x, y + 1].ClearEverything(); Main.tile[x + 1, y].ClearEverything(); Main.tile[x - 1, y].ClearEverything(); Main.tile[x, y - 1].ClearEverything();
            Main.tile[x + 1, y + 1].ClearEverything(); Main.tile[x + 1, y - 1].ClearEverything(); Main.tile[x - 1, y + 1].ClearEverything(); Main.tile[x - 1, y - 1].ClearEverything();
        }
    }
}
