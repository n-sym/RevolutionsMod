using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Revolutions.Utils;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolutions.NPCs
{
    public class CoreAttacker : PowerNPC
    {

        public override void SetStaticDefaults()
        {
            NPCID.Sets.TrailCacheLength[npc.type] = 5;
            NPCID.Sets.TrailingMode[npc.type] = 0;
        }
        public override void SetDefaults()
        {
            npc.width = 61;
            npc.height = 61;
            npc.friendly = false;
            npc.aiStyle = -1;
            npc.damage = 120;
            npc.defense = 50;
            npc.lifeMax = 7500;
            npc.knockBackResist = 0;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.value = Item.sellPrice(0, 0, 0, 0);
            npc.noGravity = true;
            npc.noTileCollide = false;
        }

        public override void AI()
        {
            Lighting.AddLight(npc.Center, 1f, 1f, 1f);
            npc.rotation += 0.0314f;
            Core.GetCore.attackerexist = 1;
            Vector2 position = Core.GetCore.position;
            Player target = Core.GetCore.target;
            Difficulty.player = target;
            float size = 0.002f * npc.ai[2] * npc.ai[2] + 1;
            switch (npc.ai[3])
            {
                case 0:
                    //转圈圈
                    npc.Center = new Vector2(900 * (float)Math.Cos(1.5 * npc.ai[0]) * size + position.X
                    , 900 * (float)Math.Sin(1 * npc.ai[0]) * size + position.Y);
                    npc.ai[0] += 0.03f;
                    if (Vector2.Distance(target.position, position) < 750)
                    {
                        npc.ai[1] = 0;
                        npc.ai[3] = 2;
                    }
                    break;
                case 1:
                    npc.Center = Helper.GetCloser(npc.Center, (target.Center + target.velocity - position) / 1.5f * size + position + 
                        new Vector2(Helper.EntroptPool[npc.whoAmI], Helper.EntroptPool[100 + npc.whoAmI]), (float)Math.Abs(20 * Math.Sin(npc.ai[1] / 19.6f)), 20);
                    if (npc.ai[1] % 12 == 0 && npc.ai[1] != 0)
                    {
                        Projectile.NewProjectile(npc.Center, npc.Center, ModContent.ProjectileType<Projectiles.FinalLightBoss>(), npc.damage / 5, 3, target.whoAmI);
                    }
                    npc.ai[1]++;
                    if (npc.ai[1] > 60)
                    {
                        npc.ai[1] = 0;
                        npc.ai[3] = 4;
                    }
                    if (Vector2.Distance(target.Center, position) >= 2200)
                    {
                        npc.ai[3] = 4;
                        npc.ai[1] = 0;
                    }
                    break;
                case 2:
                    if (npc.ai[1] == 37) npc.ai[3] = 1;
                    npc.Center = Helper.GetCloser(npc.Center, (target.Center + target.velocity - position) / 1.5f * size + position, npc.ai[1], 36);
                    npc.ai[1]++;
                    break;
                case 3:
                    if (npc.ai[1] == 37) npc.ai[3] = 0;
                    npc.Center = Helper.GetCloser(npc.Center.X, npc.Center.Y, 900 * (float)Math.Cos(1.5 * npc.ai[0]) * size + position.X, 900 * (float)Math.Sin(1 * npc.ai[0]) * size + position.Y, npc.ai[1], 36);
                    npc.ai[1]++;
                    break;
                case 4:
                    //等cd
                    if (npc.ai[1] < 37) npc.position = Helper.GetCloser(npc.position.X, npc.position.Y, 530 * (float)Math.Cos(1.5 * npc.ai[0]) * size + position.X, 530 * (float)Math.Sin(1 * npc.ai[0]) * size + position.Y, npc.ai[1], 36);
                    if (npc.ai[1] >= 37 && npc.ai[1] < 324)
                    {
                        npc.position.X = 630 * (float)Math.Cos(1.5 * npc.ai[0]) * size + position.X;
                        npc.position.Y = 630 * (float)Math.Sin(1 * npc.ai[0]) * size + position.Y;
                        npc.ai[0] += 0.03f;
                    }
                    if (npc.ai[1] >= 324 && npc.ai[1] < 361) npc.position = Helper.GetCloser(npc.position.X, npc.position.Y, 530 * (float)Math.Cos(1.5 * npc.ai[0]) * size + position.X, 530 * (float)Math.Sin(1 * npc.ai[0]) * size + position.Y, npc.ai[1] - 324, 36);
                    npc.ai[1]++;
                    if (npc.ai[1] == 361)
                    {
                        npc.ai[1] = 0;
                        npc.ai[3] = 1;
                    }
                    break;
                case -1:
                    //等cd
                    if (npc.ai[1] < 37) npc.position = Helper.GetCloser(npc.position.X, npc.position.Y, 530 * (float)Math.Cos(1.5 * npc.ai[0]) * size + position.X, 530 * (float)Math.Sin(1 * npc.ai[0]) * size + position.Y, npc.ai[1], 36);
                    if (npc.ai[1] >= 37 && npc.ai[1] < -37 + 240 * npc.ai[2])
                    {
                        npc.position.X = 630 * (float)Math.Cos(1.5 * npc.ai[0]) * size + position.X;
                        npc.position.Y = 630 * (float)Math.Sin(1 * npc.ai[0]) * size + position.Y;
                        npc.ai[0] += 0.03f;
                    }
                    if (npc.ai[1] >= -37 + 240 * npc.ai[2] && npc.ai[1] < 240 * npc.ai[2]) npc.position = Helper.GetCloser(npc.position.X, npc.position.Y, 530 * (float)Math.Cos(1.5 * npc.ai[0]) * size + position.X, 530 * (float)Math.Sin(1 * npc.ai[0]) * size + position.Y, npc.ai[1] + 37 - (240 * npc.ai[2]), 36);
                    npc.ai[1]++;
                    if (npc.ai[1] == 240 * npc.ai[2])
                    {
                        npc.ai[1] = 0;
                        npc.ai[3] = 1;
                    }
                    break;
                case 100:
                case 102:
                case 104:
                    //冲三次
                    if (npc.ai[1] == 0) PositionSave[0] = npc.Center;
                    npc.Center = Helper.GetCloser(PositionSave[0], target.Center + 250 * Helper.ToUnitVector(target.Center - PositionSave[0]) + Difficulty.num * 0.3f * target.velocity, -14 * (float)Math.Cos(npc.ai[1] * 0.1122f) + 14, 28);
                    npc.ai[1]++;
                    if (npc.ai[1] == 28)
                    {
                        npc.ai[1] = 35;
                        npc.ai[3]++;
                    }
                    break;
                case 101:
                case 103:
                    npc.ai[1]--;
                    if (npc.ai[1] == 0) npc.ai[3]++;
                    break;
                case 105:
                    npc.ai[1] = 40;
                    npc.ai[3] = 101;
                    break;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * 0.5f, npc.height * 0.5f);
            //Player player = Main.player[npc.owner];
            for (int k = 1; k < npc.oldPos.Length; k++)
            {
                for (int i = 0; i < 3; i++)
                {
                    Vector2 drawPosition = Helper.GetCloser(npc.oldPos[k - 1], npc.oldPos[k], i, 3) - Main.screenPosition + drawOrigin + new Vector2(0f, npc.gfxOffY);
                    Color color = npc.GetAlpha(lightColor);
                    color *= ((float)(npc.oldPos.Length - k) / (float)npc.oldPos.Length) * 0.18f;
                    spriteBatch.Draw(Main.npcTexture[npc.type], drawPosition, null, color, npc.rotation, drawOrigin, npc.scale, SpriteEffects.None, 0f);
                    spriteBatch.Draw(Main.npcTexture[npc.type], drawPosition + new Vector2(2, 2), null, color, npc.rotation, drawOrigin, npc.scale, SpriteEffects.None, 0f);
                    spriteBatch.Draw(Main.npcTexture[npc.type], drawPosition + new Vector2(-2, 2), null, color, npc.rotation, drawOrigin, npc.scale, SpriteEffects.None, 0f);
                    spriteBatch.Draw(Main.npcTexture[npc.type], drawPosition + new Vector2(2, -2), null, color, npc.rotation, drawOrigin, npc.scale, SpriteEffects.None, 0f);
                    spriteBatch.Draw(Main.npcTexture[npc.type], drawPosition + new Vector2(-2, -2), null, color, npc.rotation, drawOrigin, npc.scale, SpriteEffects.None, 0f);
                }
            }
            return true;
        }
    }
}
