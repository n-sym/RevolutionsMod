using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Revolutions.Utils;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolutions.Projectiles.RareWeapon
{
    public class OathProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Oath");
            Main.projFrames[projectile.type] = 4;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 3; 
            ProjectileID.Sets.TrailingMode[projectile.type] = 0; 
        }
        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 24;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.timeLeft = 300;
            projectile.tileCollide = false;
            projectile.alpha = 255;
            projectile.ignoreWater = true;
            projectile.aiStyle = -1;
            projectile.light = 1f;
            projectile.penetrate = 3;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
        }
        float a;
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + 1.57f;
            if (projectile.frame == 3 && projectile.timeLeft % 3 == 0) projectile.frame = 0;
            else if(projectile.timeLeft % 3 == 0) projectile.frame++;
            if (projectile.penetrate < 0) projectile.alpha += 15;
            if (projectile.timeLeft == 299)
            {
                for (int i = 1; i < 17; i++)
                {
                    Dust d = Dust.NewDustDirect(Curve.GetEllipse(i * 0.3926f, 10, 20, projectile.velocity.ToRotation()) + projectile.position, 0, 0, MyDustId.YellowGoldenFire, 0, 0, 0, Color.White, 1f);
                    d.noGravity = true;
                    d.velocity = Helper.ToUnitVector(d.position - projectile.position);
                }
            }
            if (projectile.alpha < 60)
            {
                for (int i = 0; i < 4; i++)
                {
                    Dust d = Dust.NewDustDirect(projectile.position, 12, 12, MyDustId.YellowGoldenFire, 0, 0, 0, Color.White, 1f);
                    d.noGravity = true;
                }
            }
            projectile.scale = 1.05f + 0.1f * (float)Math.Sin(a);
            a += 0.314f;
            if (projectile.timeLeft >= 290) projectile.alpha = (int)((projectile.timeLeft - 290) * 25.5f);
            if (projectile.ai[0] != 0) projectile.tileCollide = true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.ai[0] != 0)
            {
                if (projectile.penetrate == 1) projectile.penetrate++;
                else
                {
                    Vector2 random = new Vector2(50, 0).RotatedByRandom(6.283);
                    random.X += Main.player[projectile.owner].direction == 1 ? -50 : 50;
                    Projectile.NewProjectile(new Vector2(projectile.ai[0], projectile.ai[1]) + random, Helper.ToUnitVector(target.Center - new Vector2(projectile.ai[0], projectile.ai[1]) - random) * 38, projectile.type, damage, projectile.knockBack, projectile.owner);
                    random = new Vector2(50, 0).RotatedByRandom(6.283);
                    random.X += Main.player[projectile.owner].direction == 1 ? -50 : 50;
                    Projectile.NewProjectile(new Vector2(projectile.ai[0], projectile.ai[1]) + random, Helper.ToUnitVector(target.Center - new Vector2(projectile.ai[0], projectile.ai[1]) - random) * 38, projectile.type, damage, projectile.knockBack, projectile.owner);
                }
            }
            else
            {
                projectile.penetrate = -1;
                projectile.damage = 0;
            }
            projectile.damage = (int)(projectile.damage * 0.95f);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            Player player = Main.player[projectile.owner];
            for (int i = 0; i < 6; i++)
            {
                Vector2 drawPosition = Helper.GetCloser(projectile.position, projectile.oldPos[2], 1, 1f + 0.5f * i) - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor);
                color *= 0.4f;
                if (Vector2.Distance(projectile.position, player.position) < 0.5f * Vector2.Distance(Vector2.Zero, new Vector2(Main.screenWidth, Main.screenHeight)) && projectile.timeLeft < 295)
                {
                    spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPosition, Main.projectileTexture[projectile.type].Frame(1, 4, 0, projectile.frame), color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
                }
            }
            return true;
        }
    }
}
