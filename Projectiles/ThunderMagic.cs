using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Revolutions.Utils;
using System;
using System.Collections.Generic;
using Terraria;

namespace Revolutions.Projectiles
{
    public class ThunderMagic : PowerProj
    {
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.magic = true;
            projectile.friendly = true;
            projectile.timeLeft = 90;
            projectile.tileCollide = false;
            projectile.alpha = 255;
            projectile.ignoreWater = true;
            projectile.aiStyle = -1;
            projectile.scale = 1f;
            projectile.penetrate = 2000;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 1;
        }
        int fix = 0;
        List<NPC> hitednpc = new List<NPC>();
        NPC npc = null;
        public override bool ShouldUpdatePosition()
        {
            return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

        }
        int counter = 1;
        int counter2 = 0;
        int status = 0;
        float distance = 800f;
        public override void AI()
        {
            PositionSave[0] = new Vector2(projectile.ai[0], projectile.ai[1]);
            foreach (NPC target in Main.npc)
            {
                if (!target.dontTakeDamage && (!target.friendly || target.type == Terraria.ID.NPCID.TargetDummy) && projectile.timeLeft == 90 && target.active && Vector2.Distance(target.position, projectile.position) < distance && !hitednpc.Contains(target) && counter < 9)
                {
                    hitednpc.Add(target);
                    PositionSave[counter + counter2] = target.Center;
                    PositionSave[counter + counter2 + 1] = target.Center + new Vector2(Main.rand.Next(-30, 30), Main.rand.Next(-30, 30));
                    counter++;
                    counter2++;
                    distance = 600f;
                }
            }
            if (PositionSave[status + 1] != Vector2.Zero)
            {
                projectile.Center = Helper.GetCloser(PositionSave[status], PositionSave[status + 1], fix, 3.5f);
                status++;
                fix++;
                if (fix == 5) fix = 0;
            }
            else
            {
                status = 0;
            }
            if (status == counter + counter2) status = 0;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(1f, 1f);
            Vector2 target;
            foreach (Vector2 current in PositionSave)
            {
                if (current != Vector2.Zero)
                {
                    if (PositionSave[Array.IndexOf(PositionSave, current) + 1] == Vector2.Zero) continue;
                    target = PositionSave[Array.IndexOf(PositionSave, current) + 1];
                    Color color = Color.White;
                    for (int i = 0; i < Vector2.Distance(current, target) / 3; i++)
                    {
                        if (Helper.Specialname2Color(Helper.spname) == Color.White)
                        {
                            color = Helper.GetCloserColor(Helper.GetRainbowColorLinear(i + Array.IndexOf(PositionSave, current) * 30, hitednpc.Count * 60 + Main.rand.Next(-100, 100)), Color.White, 8, 9);
                        }
                        else
                        {
                            color = Helper.Specialname2Color(Helper.spname);
                        }
                        float sizeFix = i * Array.IndexOf(PositionSave, current) + 20 * Array.IndexOf(PositionSave, current) + 1;
                        sizeFix /= hitednpc.Count * 150;
                        sizeFix += 0.2f;
                        color *= sizeFix * (float)projectile.timeLeft / 90;
                        Vector2 drawPos = Helper.GetCloser(current, target, i, Vector2.Distance(current, target) / 3);
                        spriteBatch.Draw(Main.projectileTexture[mod.ProjectileType("MeteowerHelper")], drawPos - Main.screenPosition, null, color, projectile.rotation, drawOrigin, 0.19f, SpriteEffects.None, 0f);
                        Lighting.AddLight(drawPos, color.R / 245, color.G / 245, color.B / 245);
                        Lighting.AddLight(drawPos, 0.55f, 0.55f, 0.55f);
                    }
                }
            }
            return true;
        }
    }
}
