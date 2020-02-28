using Microsoft.Xna.Framework;
using Revolutions.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolutions.Projectiles
{
    public class CoreFx : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("FX");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            projectile.width = 1;
            projectile.height = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.extraUpdates = 20;
            projectile.timeLeft = 45000;
            projectile.alpha = 255;
            projectile.ignoreWater = true;
            projectile.aiStyle = -1;
        }
        int j = 0;
        int i = 0;
        Vector2 a = Vector2.Zero;
        Vector2 target = Vector2.Zero;
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            projectile.position = Helper.GetCloser(projectile.position, player.Center, i, 2000) + player.velocity / 20;
            if (Vector2.Distance(projectile.position, player.Center) <= 5) projectile.Kill();
            target = Helper.GetCloser(player.Center, projectile.position, 2, 7);
            Dust dust = Dust.NewDustDirect(Helper.GetCloser(projectile.position, target, j, 30), 1, 1, mod.DustType("Pixel"), 0, 0, 255 - (int)(255 * j / 30), Helper.GetCloserColor(Helper.GetRainbowColorLinear(j, 30), Color.White, 5, 6), 0.4f + 0.2f * j / 30);

            j++;
            if (j == 20)
            {
                j = 0;
                i++;
            }
        }
        /*public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Player player = Main.player[projectile.owner];
            SpriteBatch s = new SpriteBatch(spriteBatch.GraphicsDevice);
            for (int k = 0; k < 1; k++)
            {
                spriteBatch.Draw(Main.projectileTexture[mod.ProjectileType("CoreFx")], Helper.GetCloser(projectile.position, player.position, k, 30), null, Helper.GetCloserColor(Helper.GetRainbowColorLinear(k, 30), Color.White, 5, 6), projectile.rotation, new Vector2(8, 8), 0.5f, SpriteEffects.None, 0f);
                spriteBatch.Draw(Main.projectileTexture[mod.ProjectileType("UltimateStar")], Helper.GetCloser(projectile.position, player.position, k, 30), null, Helper.GetCloserColor(Helper.GetRainbowColorLinear(k, 30), Color.White, 5, 6), projectile.rotation, new Vector2(8, 8), 0.5f, SpriteEffects.None, 0f);

                //Helper.Print(k.ToString());
            }
            spriteBatch.Draw(Main.projectileTexture[mod.ProjectileType("CoreFx")], Helper.GetCloser(projectile.position, player.position, 1, 30), null, Helper.GetCloserColor(Helper.GetRainbowColorLinear(1, 30), Color.White, 5, 6), projectile.rotation, new Vector2(8, 8), 0.5f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Main.projectileTexture[mod.ProjectileType("UltimateStar")], Helper.GetCloser(projectile.position, player.position, 1, 30), null, Helper.GetCloserColor(Helper.GetRainbowColorLinear(1, 30), Color.White, 5, 6), projectile.rotation, new Vector2(8, 8), 0.5f, SpriteEffects.None, 0f);

            return true;
        }*/
    }
}
