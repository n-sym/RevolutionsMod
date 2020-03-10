using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Revolutions.Utils;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolutions.Projectiles.CoreWeapon
{
    public class FinalLightSummon : PowerProj
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("LightArrow");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 16;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;
            projectile.friendly = true;
            projectile.minion = true;
            projectile.timeLeft = 30;
            projectile.tileCollide = false;
            projectile.alpha = 255;
            projectile.ignoreWater = true;
            projectile.aiStyle = -1;
            projectile.light = 0.5f;
            projectile.scale = 0.8f;
            projectile.penetrate = 2000;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
        }
        public override bool ShouldUpdatePosition()
        {
            return false;
        }
        NPC t = null;
        public override void AI()
        {
            if (Main.npc.Length < projectile.ai[1]) projectile.Kill();
            NPC target = Main.npc[(int)projectile.ai[1]];
            Player player = Main.player[projectile.owner];
            projectile.position = Helper.GetCloser(projectile.velocity, target.Center, 30 - projectile.timeLeft, 28);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (target.whoAmI == projectile.ai[1])
            {
                projectile.damage = 0;
                Random rd = new Random();
                int a = rd.Next(5, 15);
                int b = rd.Next(1, 2);
                Dust f = Dust.NewDustDirect(projectile.position, 0, 0,
                    mod.DustType("hyperbola3"), 0, 0, 0, new Color(233, 233, 255), 0.9f);
                f.rotation *= a / 10;
                for (int i = 0; i < 30; i++)
                {
                    Dust d = Dust.NewDustDirect(projectile.position, 2, 2, MyDustId.WhiteTrans, 0, 0, 100,
                        Helper.GetCloserColor(Helper.GetRainbowColorLinear(i + a, 30 + (b * a)), Color.White, 1, 5), 0.8f);
                    d.noGravity = true;
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            NPC target = Main.npc[(int)projectile.ai[1]];
            if (timeLeft == 0) Projectile.NewProjectile(target.Center, Vector2.Zero, ModContent.ProjectileType<JustDamage2>(), projectile.damage, 0, projectile.owner);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width, projectile.height);
            for (int k = 0; k < projectile.oldPos.Length - 1; k++)
            {
                Vector2 drawPositiona = 0.45f * projectile.oldPos[k] + 0.55f * projectile.position;
                drawPositiona -= Main.screenPosition - drawOrigin;
                Vector2 drawPositionb = 0.45f * projectile.oldPos[k + 1] + 0.55f * projectile.position;
                drawPositionb -= Main.screenPosition - drawOrigin;
                if (projectile.oldPos[k + 1] == Vector2.Zero) drawPositionb += 0.45f * projectile.oldPos[k];
                else if (drawPositionb == Vector2.Zero) drawPositionb = drawPositiona;
                float sizeFix = k + 1;
                sizeFix /= projectile.oldPos.Length;
                sizeFix = 1 - sizeFix;
                Random rd = new Random();
                int a = rd.Next(0, 20);
                int b = rd.Next(1, 2);
                Color color = Helper.GetCloserColor(Helper.GetRainbowColorLinear(k + a, 18 + (b * a)), Color.White, 5, 6);
                color = Color.Multiply(color, sizeFix / 1.7f);
                for (int i = 0; i < 9; i++)
                {
                    spriteBatch.Draw(Main.projectileTexture[ModContent.ProjectileType<RareWeapon.MeteowerHelper>()], Helper.GetCloser(drawPositiona, drawPositionb, i, 8), null,
                    color, projectile.rotation, drawOrigin, projectile.scale * 0.25f, SpriteEffects.None, 0f);
                }

            }
            return true;
        }

    }
}
