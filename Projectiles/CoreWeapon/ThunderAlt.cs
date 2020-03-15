using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Revolutions.Utils;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace Revolutions.Projectiles.CoreWeapon
{
    public class ThunderAlt : PowerProj
    {
        public override void SetDefaults()
        {
            projectile.width = 40;
            projectile.height = 40;
            projectile.melee = true;
            projectile.friendly = true;
            projectile.timeLeft = 16;
            projectile.tileCollide = false;
            projectile.alpha = 255;
            projectile.ignoreWater = true;
            projectile.aiStyle = -1;
            projectile.scale = 1f;
            projectile.penetrate = 2000;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
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
                if (!target.dontTakeDamage && (!target.friendly || target.type == Terraria.ID.NPCID.TargetDummy) && projectile.timeLeft == 16 && target.active && Vector2.Distance(target.position, projectile.position) < distance && !hitednpc.Contains(target) && counter < 9 && (Main.rand.Next(1, 3) == 1 || counter == 0))
                {
                    hitednpc.Add(target);
                    PositionSave[counter + counter2] = target.Center;
                    //PositionSave[counter + counter2 + 1] = target.Center + new Vector2(Main.rand.Next(-30, 30), Main.rand.Next(-30, 30));
                    counter++;
                    //counter2++;
                    distance = 400f;
                }
            }
            if (projectile.timeLeft == 16)
            {
                NPCdistanceComparer com = new NPCdistanceComparer(projectile);
                hitednpc.Sort(com);
            }
            if (PositionSave[status + 1] != Vector2.Zero)
            {
                projectile.Center = Helper.GetCloser(PositionSave[status], PositionSave[status + 1], fix, 4f);
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
            Vector2 ts = PositionSave[0];
            for (int z = 0; z < 8; z++)
            {
                Vector2 current = PositionSave[z];
                if (current != Vector2.Zero)
                {
                    if (PositionSave[z + 1] == Vector2.Zero) continue;
                    target = PositionSave[z + 1];
                    Color color;
                    for (int i = 0; i < Vector2.Distance(current, target) / 40; i++)
                    {
                        Vector2 targetPos = Helper.GetCloser(current, target, i, Vector2.Distance(current, target) / 40);
                        targetPos.X += Helper.EntroptPool[i + 1 + (int)(PositionSave[0].X / 32) + projectile.whoAmI * 2] / 3;
                        targetPos.Y += Helper.EntroptPool[i + 1 + (int)(PositionSave[0].X / 32) + projectile.whoAmI * 2] / 8;
                        Vector2 currentPos = Helper.GetCloser(current, target, i - 1, Vector2.Distance(current, target) / 40);
                        currentPos.X += Helper.EntroptPool[i + (int)(PositionSave[0].X / 32) + projectile.whoAmI * 2] / 3;
                        currentPos.Y += Helper.EntroptPool[i + (int)(PositionSave[0].X / 32) + projectile.whoAmI * 2] / 8;
                        if (i == 0) currentPos = ts;
                        for (int j = 0; j < 30; j++)
                        {
                            if (Helper.Specialname2Color(Main.player[projectile.owner].GetModPlayer<RevolutionsPlayer>().spname) == Color.White)
                            {
                                color = Helper.GetCloserColor(Helper.GetRainbowColorLinear(j + i * 30 + z * 180 + 1100, 1950), Color.White, 8, 9);
                            }
                            else
                            {
                                color = Helper.Specialname2Color(Main.player[projectile.owner].GetModPlayer<RevolutionsPlayer>().spname);
                            }
                            color *= (float)projectile.timeLeft / 16;
                            spriteBatch.Draw(Main.projectileTexture[ModContent.ProjectileType<RareWeapon.MeteowerHelper>()], Helper.GetCloser(currentPos, targetPos, j, 30) - Main.screenPosition, null, Color.Multiply(color, 1), projectile.rotation, drawOrigin, 0.08f, SpriteEffects.None, 0f);
                            Lighting.AddLight(Helper.GetCloser(current, target, j, 20), color.R / 245, color.G / 245, color.B / 245);
                            Lighting.AddLight(Helper.GetCloser(current, target, j, 20), 0.55f, 0.55f, 0.55f);
                            ts = targetPos;
                        }

                    }
                }
            }
            return true;
        }
    }
}
