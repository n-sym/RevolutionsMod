using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Revolutions.Utils;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Revolutions.Projectiles.RareWeapon;

namespace Revolutions.NPCs
{
    // This ModNPC serves as an example of a complete AI example.
    public class Core : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Core of The Revolutionary Engine");
        }

        public override void SetDefaults()
        {
            npc.width = 188;
            npc.height = 188;
            npc.boss = true;
            npc.aiStyle = -1; // This npc has a completely unique AI, so we set this to -1.
            npc.damage = 0;
            npc.friendly = false;
            npc.defense = 0;
            npc.lifeMax = 2500000;
            npc.knockBackResist = 0;
            npc.HitSound = SoundID.NPCHit3;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.alpha = 255;
            //npc.color = new Color(0, 80, 255, 100);
            npc.value = Item.sellPrice(0, 36, 0, 0);
            npc.noGravity = true;
            npc.noTileCollide = true;
            //npc.rotation = 3.141f * 0.25f;
        }

        public class GetCore
        {
            public static Vector2 position;
            public static Player target;
            public static int attackerexist;
        }

        // AdvancedFlutterSlime will need: float in water, diminishing aggo, spawn projectiles.
        Player target;
        public override void AI()
        {
            target = Main.player[0];
            Lighting.AddLight(npc.Center, 3, 3, 3);
            //AI_Timer++;
            //Talk(Vector2.Distance(target.Center, npc.Center).ToString());
            //npc.scale = 1f + (float)Math.Sin(AI_Timer * 0.05f) * 0.05f;
            //npc.visualOffset.Y = 188 + (float)Math.Sin(AI_Timer * 0.05f) * 19.2f;
            if (npc.aiStyle == -1)
            {
                for (int i = 0; i < 8; i++)
                {
                    NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType("CoreAttacker"), 0, 0.628f * i, 0, i, 1);
                }
                npc.aiStyle = -2;
                //NPC.NewNPC(0, 0, mod.NPCType("CoreAttacker"), 0, 0, 0, 3);
            }
            GetCore.position = npc.Center;
            GetCore.target = target;

        }
        float drawtimer = 0;
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (!Main.gamePaused) drawtimer += 0.02f;
            Color c = new Color(255, 255, 255, 255);
            float x = (float)Math.Asin(Math.Abs(Math.Sin(drawtimer))) / 15.71f + 0.9f;
            float rota = (npc.Center - Main.player[0].Center).ToRotation();
            for (int alpha = 0; alpha < 211; alpha++)
            {
                float zeta = alpha + 1;
                zeta /= 210;
                zeta = 1 - zeta;
                zeta *= 0.8f;
                spriteBatch.Draw(Main.projectileTexture[ModContent.ProjectileType<MeteowerHelper>()], npc.Center - Main.screenPosition + new Vector2(4f, 4f).RotatedBy(rota) * alpha, null, c * zeta, 0f, new Vector2(1, 1), 0.33f, SpriteEffects.None, 0);
                spriteBatch.Draw(Main.projectileTexture[ModContent.ProjectileType<MeteowerHelper>()], npc.Center - Main.screenPosition - new Vector2(4f, 4f).RotatedBy(rota) * alpha, null, c * zeta, 0f, new Vector2(1, 1), 0.33f, SpriteEffects.None, 0);
            }
            rota += 1.57f;
            for (int alpha = 0; alpha < 161; alpha++)
            {
                float zeta = alpha + 1;
                zeta /= 160;
                zeta = 1 - zeta;
                zeta *= 0.8f;
                spriteBatch.Draw(Main.projectileTexture[ModContent.ProjectileType<MeteowerHelper>()], npc.Center - Main.screenPosition + new Vector2(4f, 4f).RotatedBy(rota) * alpha, null, c * zeta, 0f, new Vector2(1, 1), 0.33f, SpriteEffects.None, 0);
                spriteBatch.Draw(Main.projectileTexture[ModContent.ProjectileType<MeteowerHelper>()], npc.Center - Main.screenPosition - new Vector2(4f, 4f).RotatedBy(rota) * alpha, null, c * zeta, 0f, new Vector2(1, 1), 0.33f, SpriteEffects.None, 0);
            }
            for (int y = 1; y < 1353; y += 8)
            {
                float a = y + 1;
                a /= 1352;
                a = 1 - a;
                if (y > 500) a *= 0f;
                Vector2 pos = new Vector2(y, 1352 / y);
                int b = y + (int)(Math.Abs(Math.Sin(drawtimer)) * 10816);
                Color d = Helper.GetCloserColor(Helper.GetRainbowColorLinear(b, 21632), c, 5, 6) * a;
                Color e = Helper.GetCloserColor(Helper.GetRainbowColorLinear(b + 1352, 21632), c, 5, 6) * a;
                Color f = Helper.GetCloserColor(Helper.GetRainbowColorLinear(b + 2704, 21632), c, 5, 6) * a;
                Color g = Helper.GetCloserColor(Helper.GetRainbowColorLinear(b + 4056, 21632), c, 5, 6) * a;
                Color h = Helper.GetCloserColor(Helper.GetRainbowColorLinear(b + 5408, 21632), c, 5, 6) * a;
                Color i = Helper.GetCloserColor(Helper.GetRainbowColorLinear(b + 6760, 21632), c, 5, 6) * a;
                Color j = Helper.GetCloserColor(Helper.GetRainbowColorLinear(b + 8112, 21632), c, 5, 6) * a;
                Color k = Helper.GetCloserColor(Helper.GetRainbowColorLinear(b + 9464, 21632), c, 5, 6) * a;
                Color l = c * a;
                for (float z = pos.Y; z > 0; z -= 8)
                {
                    spriteBatch.Draw(Main.magicPixel, npc.Center - Main.screenPosition + new Vector2(pos.X, pos.Y - z) * x, new Rectangle(0, 0, 8, 8), l, 0f, new Vector2(1, 1), 1, SpriteEffects.None, 0);
                    spriteBatch.Draw(Main.magicPixel, npc.Center - Main.screenPosition - new Vector2(-pos.X, pos.Y - z) * x, new Rectangle(0, 0, 8, 8), l, 0f, new Vector2(1, 1), 1, SpriteEffects.None, 0);
                    spriteBatch.Draw(Main.magicPixel, npc.Center - Main.screenPosition - new Vector2(pos.X, -pos.Y + z) * x, new Rectangle(0, 0, 8, 8), l, 0f, new Vector2(1, 1), 1, SpriteEffects.None, 0);
                    spriteBatch.Draw(Main.magicPixel, npc.Center - Main.screenPosition + new Vector2(-pos.X, -pos.Y + z) * x, new Rectangle(0, 0, 8, 8), l, 0f, new Vector2(1, 1), 1, SpriteEffects.None, 0);
                    spriteBatch.Draw(Main.magicPixel, npc.Center - Main.screenPosition + new Vector2(pos.Y - z, pos.X) * x, new Rectangle(0, 0, 8, 8), l, 0f, new Vector2(1, 1), 1, SpriteEffects.None, 0);
                    spriteBatch.Draw(Main.magicPixel, npc.Center - Main.screenPosition - new Vector2(pos.Y - z, -pos.X) * x, new Rectangle(0, 0, 8, 8), l, 0f, new Vector2(1, 1), 1, SpriteEffects.None, 0);
                    spriteBatch.Draw(Main.magicPixel, npc.Center - Main.screenPosition - new Vector2(-pos.Y + z, pos.X) * x, new Rectangle(0, 0, 8, 8), l, 0f, new Vector2(1, 1), 1, SpriteEffects.None, 0);
                    spriteBatch.Draw(Main.magicPixel, npc.Center - Main.screenPosition + new Vector2(-pos.Y + z, -pos.X) * x, new Rectangle(0, 0, 8, 8), l, 0f, new Vector2(1, 1), 1, SpriteEffects.None, 0);

                }
                spriteBatch.Draw(Main.magicPixel, npc.Center - Main.screenPosition + new Vector2(pos.X, pos.Y) * x, new Rectangle(0, 0, 8, 8), d, 0f, new Vector2(1, 1), 1, SpriteEffects.None, 0);
                spriteBatch.Draw(Main.magicPixel, npc.Center - Main.screenPosition + new Vector2(pos.X, -pos.Y) * x, new Rectangle(0, 0, 8, 8), k, 0f, new Vector2(1, 1), 1, SpriteEffects.None, 0);
                spriteBatch.Draw(Main.magicPixel, npc.Center - Main.screenPosition + new Vector2(-pos.X, pos.Y) * x, new Rectangle(0, 0, 8, 8), g, 0f, new Vector2(1, 1), 1, SpriteEffects.None, 0);
                spriteBatch.Draw(Main.magicPixel, npc.Center - Main.screenPosition + new Vector2(-pos.X, -pos.Y) * x, new Rectangle(0, 0, 8, 8), h, 0f, new Vector2(1, 1), 1, SpriteEffects.None, 0);
                spriteBatch.Draw(Main.magicPixel, npc.Center - Main.screenPosition + new Vector2(pos.Y, pos.X) * x, new Rectangle(0, 0, 8, 8), e, 0f, new Vector2(1, 1), 1, SpriteEffects.None, 0);
                spriteBatch.Draw(Main.magicPixel, npc.Center - Main.screenPosition + new Vector2(-pos.Y, pos.X) * x, new Rectangle(0, 0, 8, 8), f, 0f, new Vector2(1, 1), 1, SpriteEffects.None, 0);
                spriteBatch.Draw(Main.magicPixel, npc.Center - Main.screenPosition + new Vector2(pos.Y, -pos.X) * x, new Rectangle(0, 0, 8, 8), j, 0f, new Vector2(1, 1), 1, SpriteEffects.None, 0);
                spriteBatch.Draw(Main.magicPixel, npc.Center - Main.screenPosition + new Vector2(-pos.Y, -pos.X) * x, new Rectangle(0, 0, 8, 8), i, 0f, new Vector2(1, 1), 1, SpriteEffects.None, 0);
                //spriteBatch.Draw(Main.magicPixel, npc.Center - Main.screenPosition + Curve.GetHyperbola(i, 70, 70, -0.7854f), new Rectangle(0, 0, 8, 8), Color.White, 0f, new Vector2(1, 1), 1, SpriteEffects.None, 0);
            }
            return true;
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<Projectiles.Dev.Dev02>(), 0, 0);
            base.BossLoot(ref name, ref potionType);
        }
    }
}
