using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Revolutions.Utils;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolutions.Projectiles
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
        }
        int j = 0;
        int i = 0;
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
            if (projectile.timeLeft == 150)
            {
                PositionSave[1] = projectile.position;
                Random rd = new Random();
                PositionSave[0].X = rd.Next(-101, 101) / 50;
            }
            projectile.position = projectile.velocity +
                Curve.GetEllipse((30 - projectile.timeLeft) * 0.10472f,
                0.5f * Vector2.Distance(projectile.velocity, target.Center),
                4.2f * projectile.ai[0] * Helper.EntroptPool[(int)projectile.ai[0] * projectile.identity] / 50,
                (player.Center - target.Center).ToRotation())
                - 0.5f * (player.Center - target.Center);
            projectile.rotation = (projectile.position - projectile.oldPosition).ToRotation() + 1.571f;
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
            for (int k = 1; k < 15; k++)
            {
                Vector2 drawPositiona = 0.45f * projectile.oldPos[k - 1] + 0.55f * projectile.position;
                drawPositiona += new Vector2(0f, projectile.gfxOffY - 5f) - Main.screenPosition + drawOrigin;
                Vector2 drawPositionb = 0.45f * projectile.oldPos[k] + 0.55f * projectile.position;
                drawPositionb += new Vector2(0f, projectile.gfxOffY - 5f) - Main.screenPosition + drawOrigin;
                float sizeFix = k + 1;
                sizeFix /= 18;
                sizeFix = 1 - sizeFix;
                Random rd = new Random();
                int a = rd.Next(0, 20);
                int b = rd.Next(1, 2);
                Color color = Helper.GetCloserColor(Helper.GetRainbowColorLinear(k + a, 18 + (b * a)), Color.White, 5, 6);
                color = Color.Multiply(color, sizeFix / 1.7f);
                if (projectile.timeLeft < 28)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        spriteBatch.Draw(Main.projectileTexture[mod.ProjectileType("MeteowerHelper")], Helper.GetCloser(drawPositiona, drawPositionb, i, 6), null,
                        color, projectile.rotation, drawOrigin, projectile.scale * 0.25f, SpriteEffects.None, 0f);
                    }

                }
            }
            return true;
        }

    }
}
