using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Revolutions.Utils;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolutions.Projectiles
{
    public class FinalLightBoss : PowerProj
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
            projectile.hostile = true;
            projectile.timeLeft = 25;
            projectile.tileCollide = false;
            projectile.alpha = 255;
            projectile.ignoreWater = true;
            projectile.aiStyle = -1;
            projectile.light = 0.5f;
            projectile.scale = 0.8f;
        }
        public override bool ShouldUpdatePosition()
        {
            return false;
        }
        NPC t = null;
        public override void AI()
        {
            Player player = null;
            float distance = 800f;
            foreach (Player p in Main.player)
            {
                if(Vector2.Distance(p.Center, projectile.Center) < distance)
                {
                    distance = Vector2.Distance(p.Center, projectile.Center);
                    player = p;
                }
            }
            int fix = player.GetModPlayer<RevolutionsPlayer>().difficulty - 60;
            fix *= -1;
            if (fix < 0) fix = 0;
            if(player != null) projectile.position = Helper.GetCloser(projectile.velocity, player.GetModPlayer<RevolutionsPlayer>().pastCenter[fix] + player.GetModPlayer<RevolutionsPlayer>().pastSpeed[fix] * fix + 0.5f * new Vector2(Helper.EntroptPool[projectile.whoAmI], Helper.EntroptPool[100 + projectile.whoAmI]), 25 - projectile.timeLeft, 23);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
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
                sizeFix /= 18;
                sizeFix = 1 - sizeFix;
                Random rd = new Random();
                int a = rd.Next(0, 20);
                int b = rd.Next(1, 2);
                Color color = Helper.GetCloserColor(Helper.GetRainbowColorLinear(k + a, 18 + (b * a)), Color.White, 5, 6);
                color = Color.Multiply(color, sizeFix / 1.7f);
                for (int i = 0; i < 9; i++)
                {
                    spriteBatch.Draw(Main.projectileTexture[mod.ProjectileType("MeteowerHelper")], Helper.GetCloser(drawPositiona, drawPositionb, i, 8), null,
                    color, projectile.rotation, drawOrigin, projectile.scale * 0.25f, SpriteEffects.None, 0f);
                }

            }
            return true;
        }

    }
}
