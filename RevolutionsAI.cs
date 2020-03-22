using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Revolutions.Utils;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Revolutions
{
    public class RevolutionsAI
    {
        public static void PlanteraAI(NPC npc, Player target, RevolutionsPlayer revtar, ref int myTimer, ref int myTimer2, int myOldLife)
        {
            if (npc.life == npc.lifeMax) myOldLife = npc.lifeMax;
            if (target.HeldItem.noMelee)
            {
                if (myTimer2 == 0 && Main.rand.Next(420) == 1 && npc.life > 12000)
                {
                    npc.dontTakeDamage = true;
                    myTimer2 += 60;
                }
                else if (npc.life < 12000)
                {
                    if (myTimer2 > 0) npc.dontTakeDamage = false;
                    myTimer2 = 0;
                }
                if (myTimer2 == 0)
                {
                    if (myTimer == 0 && ((myOldLife - npc.life > 833) || (npc.life < 12000 && Main.rand.Next(npc.life / 100) == 1)))
                    {
                        npc.dontTakeDamage = true;
                        myTimer += 60;
                    }
                    int a = 0;
                    int b = 0;
                    if (myTimer > 30) npc.alpha = (int)((60 - myTimer) * 8.5f);
                    if (myTimer == 30)
                    {
                        while (true)
                        {
                            a = Main.rand.Next(-1, 2);
                            if (a != 0) break;
                        }
                        while (true)
                        {
                            b = Main.rand.Next(-1, 2);
                            if (b != 0) break;
                        }
                        npc.position = 2 * target.position - npc.position + new Vector2(Main.rand.Next(200, 500) * a, Main.rand.Next(200, 500) * b);

                    }
                    if (myTimer < 30) npc.alpha = (int)(myTimer * 8.5f);
                    if (myTimer == 1)
                    {
                        npc.dontTakeDamage = false;
                        myTimer -= 120 - (revtar.difficulty / 2);
                    }
                }
                else
                {
                    if (myTimer != 0) npc.dontTakeDamage = false;
                    myTimer = 0;
                    if (myTimer2 > 30) npc.alpha = (int)((60 - myTimer2) * 8.5f);
                    if (myTimer2 == 30) npc.Center = target.Center + 10 * target.velocity;
                    if (myTimer2 < 30) npc.alpha = (int)(myTimer2 * 8.5f);
                    if (myTimer2 == 1) npc.dontTakeDamage = false;
                }
            }
            else
            {
                if (myTimer == 0 && ((myOldLife - npc.life > 833) || (npc.life < 12000 && Main.rand.Next(npc.life / 100) == 1)))
                {
                    npc.dontTakeDamage = true;
                    myTimer += 60;
                }
                int a = 0;
                int b = 0;
                if (myTimer > 30) npc.alpha = (int)((60 - myTimer) * 8.5f);
                if (myTimer == 30)
                {
                    while (true)
                    {
                        a = Main.rand.Next(-1, 2);
                        if (a != 0) break;
                    }
                    while (true)
                    {
                        b = Main.rand.Next(-1, 2);
                        if (b != 0) break;
                    }
                    npc.position += new Vector2(Main.rand.Next(300, 700) * a, Main.rand.Next(300, 700) * b);

                }
                if (myTimer < 30) npc.alpha = (int)(myTimer * 8.5f);
                if (myTimer == 1)
                {
                    npc.dontTakeDamage = false;
                    myTimer -= 180 - (revtar.difficulty / 3);
                }
            }
            if (myTimer > 60) myTimer = 60;
            if (myTimer2 > 60) myTimer2 = 60;
        }
        public static void DukeFishronAI(NPC npc, Player target, RevolutionsPlayer revtar, ref int myTimer, ref int myTimer2, int myOldLife)
        {
            if (npc.ai[0] == -1 || npc.ai[0] == 4 || npc.ai[0] == 9 || npc.ai[0] == 10) npc.dontTakeDamage = true;
            else npc.dontTakeDamage = false;
            if (npc.ai[0] == 6 && npc.ai[2] == 20)
            {
                Projectile.NewProjectile(npc.Center + new Vector2(0, 20).RotatedBy(npc.rotation), Helper.ToUnitVector(Main.player[npc.target].Center + Main.player[npc.target].velocity * 30 - npc.Center) * 10, ModContent.ProjectileType<Projectiles.OriBoss.WaterArrow>(), npc.damage / 4, 6);
                Projectile.NewProjectile(npc.Center + new Vector2(0, -20).RotatedBy(npc.rotation), Helper.ToUnitVector(Main.player[npc.target].Center + Main.player[npc.target].velocity * 30 - npc.Center) * 10, ModContent.ProjectileType<Projectiles.OriBoss.WaterArrow>(), npc.damage / 4, 6);
            }
            if ((npc.velocity - target.velocity).ToRotation() < 0) npc.velocity = Helper.GetCloser(npc.velocity, target.velocity + npc.velocity, 1, 40);
            if (npc.ai[0] == 6) npc.velocity = Helper.GetCloser(Helper.ToUnitVector(npc.velocity), Helper.ToUnitVector(target.Center - npc.Center), revtar.difficulty / 30, 25) * npc.velocity.Length();
        }
        public static void DukeFishronDraw(NPC npc, SpriteBatch spriteBatch)
        {
        }
    }
}
