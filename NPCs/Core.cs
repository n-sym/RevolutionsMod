using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

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
            npc.aiStyle = -10; // This npc has a completely unique AI, so we set this to -1.
            npc.damage = 0;
            npc.friendly = false;
            npc.defense = 0;
            npc.lifeMax = 2500000;
            npc.knockBackResist = 0;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            //npc.alpha = 175;
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
        bool first = true;
        int loop = 0;
        bool duomode = false;
        int duotimer = 0;
        int angry = 0;
        float pjtvelx, pjtvely, plyvelx, plyvely;
        int duotimer2 = 0;
        int backtimer = 0;
        int xstimer;
        int smode = 0;

        float wavex = 0, wavey = 0, wave = 6, wavemode = 1;
        bool danger = false;
        Player target;
        public override void AI()
        {
            target = Main.player[0];
            Lighting.AddLight(npc.Center, 3, 3, 3);
            //AI_Timer++;
            loop++;
            //Talk(Vector2.Distance(target.Center, npc.Center).ToString());
            //npc.scale = 1f + (float)Math.Sin(AI_Timer * 0.05f) * 0.05f;
            //npc.visualOffset.Y = 188 + (float)Math.Sin(AI_Timer * 0.05f) * 19.2f;
            if (Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.P)) smode++;
            if (Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.O)) smode--;
            if (GetCore.attackerexist == 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType("CoreAttacker"), 0, 0.628f * i, 0, i, 1);
                }
                //NPC.NewNPC(0, 0, mod.NPCType("CoreAttacker"), 0, 0, 0, 3);
            }
            GetCore.position = npc.Center;
            GetCore.target = target;

        }
        /*public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            for (float i = 1.1f; i < 1.25f; i += 0.01f)
            {
                Color c = new Color((float)255 / i, (float)255 / i, (float)255 / i);
                spriteBatch.Draw(Main.npcTexture[npc.type], npc.position - Main.screenPosition + new Vector2( npc.width, npc.height), null, c, 0f, new Vector2(npc.width, npc.height), 1, SpriteEffects.None, 0f);
            }
            return true;
        }*/
        private void Talk(string message)
        {
            if (Main.netMode != 2)
            {
                string text = message;
                Main.NewText(text, 150, 250, 150);
            }
            else
            {
                NetworkText text = NetworkText.FromKey(message);
                NetMessage.BroadcastChatMessage(text, new Color(150, 250, 150));
            }
        }

    }
}
