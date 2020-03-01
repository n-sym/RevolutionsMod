using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Revolutions.Utils;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolutions.Projectiles
{
    public class Evolutionaries : PowerProj
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Evo");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.friendly = true;
            projectile.aiStyle = -1;
            projectile.timeLeft = 3;
            projectile.penetrate = -1;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.light = 1f;
            projectile.scale = 0.8f;
            projectile.netImportant = true;
            projectile.minionSlots = 1;
            projectile.minion = true;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 5;

        }
        public override void AI()
        {
            if(timer[0] > 0) timer[0]--;
            if (switches[0]) timer[1]++;
            else timer[1] = 0;
            Player player = Main.player[projectile.owner];
            RevolutionsPlayer modPlayer = player.GetModPlayer<RevolutionsPlayer>();
            if (player.dead)
            {
                modPlayer.evolutionary = false; ;
            }
            if (modPlayer.evolutionary)
            {
                projectile.timeLeft = 2;
            }
            NPC target = null;
            float dis2 = 1800f;
            Random rd = new Random();
            foreach (NPC npc in Main.npc)
            {
                if (Vector2.Distance(player.Center, npc.Center) < dis2
                    &&npc.type != NPCID.TargetDummy && npc.active
                    && Vector2.Distance(player.Center, projectile.Center) < 800f
                    && !npc.friendly
                    && npc.CanBeChasedBy(projectile))
                {
                    dis2 = Vector2.Distance(npc.Center, player.Center);
                    target = npc;
                }
            }
            if (target == null)
            {
                projectile.rotation += 3.14f / 90;
                if (Vector2.Distance(player.Center, projectile.Center) > 300f)
                {
                    projectile.velocity = 0.3f * player.velocity + 0.5f * projectile.velocity + Helper.ToUnitVector(player.Center - projectile.Center) * Vector2.Distance(player.Center, projectile.Center) / 120f;
                    switches[0] = false;
                }
                if (Vector2.Distance(player.Center, projectile.Center) > 250f && Vector2.Distance(player.Center, projectile.Center) < 300f)
                {
                    projectile.velocity = 0.2f * player.velocity + 0.5f * projectile.velocity + Helper.ToUnitVector(player.Center - projectile.Center) * Vector2.Distance(player.Center, projectile.Center) / 60f;
                    PositionSave[0].X = (projectile.Center - player.Center).ToRotation();
                    switches[0] = true;
                }
                if (Vector2.Distance(player.Center, projectile.Center) < 250f)
                {
                    projectile.velocity = 0.4f * player.velocity + 0.5f * projectile.velocity + 7 * Curve.DerivativeGetEllipse(PositionSave[0].X + (timer[1] / 7) , 1, 1f, projectile.ai[0]) + Helper.ToUnitVector(player.Center - projectile.Center) * 3f;
                    switches[0] = true;
                }
                //projectile.velocity.X += Helper.EntroptPool[projectile.whoAmI + rd.Next(1, 500)] * 0.005f;
                //projectile.velocity.Y += Helper.EntroptPool[projectile.whoAmI + rd.Next(501, 1000)] * 0.005f;
                //if(Vector2.Distance(player.Center, projectile.Center) > 500f) projectile.velocity += 2f * player.velocity;
            }
            else if (dis2 < 450f)
            {
                projectile.rotation += 3.14f / 30;
                projectile.velocity = 0.6f * projectile.oldVelocity + Helper.ToUnitVector(target.Center - projectile.Center) * Vector2.Distance(target.Center, projectile.Center) / 15f;
                projectile.velocity.X += Helper.EntroptPool[projectile.whoAmI + rd.Next(1, 500)] * 0.02f;
                projectile.velocity.Y += Helper.EntroptPool[projectile.whoAmI + rd.Next(501, 1000)] * 0.02f;
            }
            else
            {
                projectile.rotation += 3.14f / 60;
                if (Vector2.Distance(player.Center, projectile.Center) > 110f)
                {
                    projectile.velocity = 0.2f * player.velocity + 0.8f * projectile.oldVelocity + Helper.ToUnitVector(Helper.GetCloser(projectile.Center, player.Center, 1, 2 + rd.Next(0, 2)) - projectile.Center) * Vector2.Distance(player.Center, projectile.Center) / 600f;
                }
                Vector2 speed = 100 * Helper.ToUnitVector(target.Center - projectile.Center);
                projectile.velocity.X += Helper.EntroptPool[projectile.whoAmI + rd.Next(1, 500)] * 0.007f;
                projectile.velocity.Y += Helper.EntroptPool[projectile.whoAmI + rd.Next(501, 1000)] * 0.007f;
                if (timer[0] == 0)
                {
                    timer[0] += 12;
                    Main.PlaySound(SoundID.Item9);
                    Projectile.NewProjectile(projectile.Center + projectile.velocity, projectile.Center + projectile.velocity, ModContent.ProjectileType<FinalLightSummon>(), projectile.damage * 3 / 2, projectile.knockBack, projectile.owner, 0, target.whoAmI);
                }
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = (int)(0.66f * damage);
            crit = true;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            Player player = Main.player[projectile.owner];
            for (int k = 1; k < projectile.oldPos.Length; k++)
            {
                for (int i = 0; i < 2; i++)
                {
                    Vector2 drawPosition = Helper.GetCloser(projectile.oldPos[k - 1], projectile.oldPos[k], i, 2) - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                    Color color = projectile.GetAlpha(lightColor);
                    color *= ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length) * 0.18f;
                    spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPosition, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
                    spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPosition + new Vector2(2, 2), null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
                    spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPosition + new Vector2(-2, 2), null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
                    spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPosition + new Vector2(2, -2), null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
                    spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPosition + new Vector2(-2, -2), null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
                }
            }
            return true;
        }
    }
}
