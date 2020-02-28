using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Revolutions.NPCs
{
    // This ModNPC serves as an example of a complete AI example.
    public class FlutterSlime : ModNPC
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Flutter Slime"); // Automatic from .lang files
            Main.npcFrameCount[npc.type] = 6; // make sure to set this for your modnpcs.
        }

        public override void SetDefaults()
        {
            npc.width = 32;
            npc.height = 32;
            npc.aiStyle = -1; // This npc has a completely unique AI, so we set this to -1.
            npc.damage = 0;
            npc.defense = 0;
            npc.lifeMax = 40000;
            npc.knockBackResist = 0;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            //npc.alpha = 175;
            //npc.color = new Color(0, 80, 255, 100);
            npc.value = Item.sellPrice(0, 36, 0, 0);
            npc.noGravity = true;
            npc.noTileCollide = false;
        }

        /*public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			// we would like this npc to spawn in the overworld.
			return SpawnCondition.OverworldDaySlime.Chance * 0.1f;
		}*/

        // These const ints are for the benefit of the programmer. Organization is key to making an AI that behaves properly without driving you crazy.
        // Here I lay out what I will use each of the 4 npc.ai slots for.
        const int AI_State_Slot = 0;
        const int AI_Timer_Slot = 1;
        const int AI_Flutter_Time_Slot = 2;
        const int AI_Duo_Slot_3 = 3;

        // npc.localAI will also have 4 float variables available to use. With ModNPC, using just a local class member variable would have the same effect.
        const int Local_AI_Unused_Slot_0 = 0;
        const int Local_AI_Unused_Slot_1 = 1;
        const int Local_AI_Unused_Slot_2 = 2;
        const int Local_AI_Duo_Slot_3 = 3;

        // Here I define some values I will use with the State slot. Using an ai slot as a means to store "state" can simplify things greatly. Think flowchart.
        const int State_Asleep = 0;
        const int State_Notice = 1;
        const int State_Jump = 2;
        const int State_Hover = 3;
        const int State_Fall = 4;
        const int State_Duo = 5;

        // This is a property (https://msdn.microsoft.com/en-us/library/x9fsa0sw.aspx), it is very useful and helps keep out AI code clear of clutter.
        // Without it, every instance of "AI_State" in the AI code below would be "npc.ai[AI_State_Slot]". 
        // Also note that without the "AI_State_Slot" defined above, this would be "npc.ai[0]".
        // This is all to just make beautiful, manageable, and clean code.
        public float AI_State
        {
            get { return npc.ai[AI_State_Slot]; }
            set { npc.ai[AI_State_Slot] = value; }
        }

        public float AI_Timer
        {
            get { return npc.ai[AI_Timer_Slot]; }
            set { npc.ai[AI_Timer_Slot] = value; }
        }

        public float AI_FlutterTime
        {
            get { return npc.ai[AI_Flutter_Time_Slot]; }
            set { npc.ai[AI_Flutter_Time_Slot] = value; }
        }

        public class GetAstoryPosition
        {
            public static float X = 0, Y = 0,
                newX = Main.screenWidth * -0.3f,
                newY = Main.screenHeight * -0.3f;
        }
        // AdvancedFlutterSlime will need: float in water, diminishing aggo, spawn projectiles.
        bool first = true;
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
        // Our AI here makes our NPC sit waiting for a player to enter range, jumps to attack, flutter mid-fall to stay afloat a little longer, then falls to the ground. Note that animation should happen in FindFrame
        public override void AI()
        {
            if (first == true)
            {
                GetAstoryPosition.newX = Main.screenWidth * -0.3f; GetAstoryPosition.newY = Main.screenHeight * -0.3f;
                first = false;
            }
            GetAstoryPosition.X = Main.player[0].position.X + GetAstoryPosition.newX + wavex; GetAstoryPosition.Y = Main.player[0].position.Y + GetAstoryPosition.newY + wavey; npc.position.X = GetAstoryPosition.X; npc.position.Y = GetAstoryPosition.Y;
            Projectile projectile = Main.projectile[0];
            float pjtpx = 0, pjtpy = 0, duofz = 0;
            float cd = 0;
            int i = 0;
            danger = false;
            int dangertype = 0;
            string pjtname = "none";
            Player player = Main.player[0];
            float pd = Vector2.Distance(player.position, npc.position);
            float pd2 = 0.03f * (Vector2.Distance(new Vector2(Main.screenWidth, Main.screenHeight), new Vector2(Main.screenWidth * 0.5f, Main.screenHeight * 0.5f)) - pd);
            if (pd >= Vector2.Distance(new Vector2(Main.screenWidth, Main.screenHeight), new Vector2(Main.screenWidth * 0.5f, Main.screenHeight * 0.5f)))
            {
                duofz = 2;
            }
            else if (pd <= 64f)
            {
                duofz = -1;
            }
            else
            {
                duofz = 1;
            }
            Projectile target = Main.projectile[0];
            if (Vector2.Distance(projectile.position, npc.position) < 99f)
            {
                if (projectile.whoAmI == player.whoAmI && projectile.damage > 0)
                {
                    danger = true;
                    target = projectile;
                    if (projectile.velocity.X >= 0)
                    {
                        pjtvelx = -1;
                    }
                    else
                    {
                        pjtvelx = 1;
                    }
                    if (projectile.velocity.Y >= 0)
                    {
                        pjtvely = -1;
                    }
                    else
                    {
                        pjtvely = 1;
                    }
                }
            }

            Talk("test" + "," + AI_State + danger + "," + pjtvelx);
            // The npc starts in the asleep state, waiting for a player to enter range
            Random duord = new Random();
            int duord2 = duord.Next(-66, 67);
            if (danger == true)
            {
                duomode = true;
                angry++;
            }
            else
            {
                if (duomode == true)
                {
                    if (dangertype == 1)
                    {
                        //GetAstoryPosition.newX = plyvelx * 50 + player.position.X;
                        //GetAstoryPosition.newX = plyvely * 50 + player.position.Y;
                    }
                    else
                    {
                        GetAstoryPosition.newX = plyvelx * 50 + GetAstoryPosition.newX;
                        GetAstoryPosition.newY = plyvely * 50 + GetAstoryPosition.newY;
                        duomode = false;
                        /*xstimer = 0;
                        if (xstimer <= 51)
                        {
                            xstimer++;
                            npc.alpha = npc.alpha + 5;
                        }
                        Shanxian(30, 30);
                        xstimer = 0;
                        if (xstimer <= 51)
                        {
                            xstimer++;
                            npc.alpha = npc.alpha - 5;
                        }*/
                    }
                    LightArrow(angry);
                    duotimer2 = 60;
                }
                if (duotimer2 > 0)
                {
                    duotimer2--;
                    npc.dontTakeDamage = true;
                }
                else
                {
                    npc.dontTakeDamage = false;
                }
                if (AI_State == State_Asleep)
                {

                }
                // In this state, a player has been targeted
                else if (AI_State == State_Notice)
                {

                }
                // In this state, we are in the jump. 
                else if (AI_State == State_Jump)
                {

                }
                // In this state, our npc starts to flutter/fly a little to make it's movement a little bit interesting.
                else if (AI_State == State_Hover)
                {

                }
                // In this state, we fall untill we hit the ground. Since npc.noTileCollide is false, our npc collides with ground it lands on and will have a zero y velocity once it has landed.
                else if (AI_State == State_Fall)
                {

                }
                else if (AI_State == State_Duo)
                {

                }
                if (wave == 0)
                {
                    wave = 16;
                    wavemode = wavemode * -1;
                }
                if (player.statLife <= 0)
                {
                    player.statLife = 1;
                    player.active = true;
                }
                wavey = 0.15f * wavemode + wavey;
                wave--;
            }
        }
        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {

            danger = true;
        }
        // Our texture is 32x32 with 2 pixels of padding vertically, so 34 is the vertical spacing.  These are for my benefit and the numbers could easily be used directly in the code below, but this is how I keep code organized.
        const int Frame_Asleep = 0;
        const int Frame_Notice = 1;
        const int Frame_Falling = 2;
        const int Frame_Flutter_1 = 3;
        const int Frame_Flutter_2 = 4;
        const int Frame_Flutter_3 = 5;

        // Here in FindFrame, we want to set the animation frame our npc will use depending on what it is doing.
        // We set npc.frame.Y to x * frameHeight where x is the xth frame in our spritesheet, counting from 0. For convenience, I have defined some consts above.
        public override void FindFrame(int frameHeight)
        {
            // This makes the sprite flip horizontally in conjunction with the npc.direction.
            npc.spriteDirection = npc.direction;

            // For the most part, our animation matches up with our states.
            if (AI_State == State_Asleep)
            {
                // npc.frame.Y is the goto way of changing animation frames. npc.frame starts from the top left corner in pixel coordinates, so keep that in mind.
                npc.frame.Y = Frame_Asleep * frameHeight;
            }
            else if (AI_State == State_Notice)
            {
                // Going from Notice to Asleep makes our npc look like it's crouching to jump.
                if (AI_Timer < 10)
                {
                    npc.frame.Y = Frame_Notice * frameHeight;
                }
                else
                {
                    npc.frame.Y = Frame_Asleep * frameHeight;
                }
            }
            else if (AI_State == State_Jump)
            {
                npc.frame.Y = Frame_Falling * frameHeight;
            }
            else if (AI_State == State_Hover)
            {
                // Here we have 3 frames that we want to cycle through.
                npc.frameCounter++;
                if (npc.frameCounter < 10)
                {
                    npc.frame.Y = Frame_Flutter_1 * frameHeight;
                }
                else if (npc.frameCounter < 20)
                {
                    npc.frame.Y = Frame_Flutter_2 * frameHeight;
                }
                else if (npc.frameCounter < 30)
                {
                    npc.frame.Y = Frame_Flutter_3 * frameHeight;
                }
                else
                {
                    npc.frameCounter = 0;
                }
            }
            else if (AI_State == State_Fall)
            {
                npc.frame.Y = Frame_Falling * frameHeight;
            }

        }
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
        private void Shanxian(float x, float y)
        {
            npc.position = new Vector2(npc.position.X + x, npc.position.Y + y);
        }
        private void LightArrow(int damage)
        {
            Projectile.NewProjectile(new Vector2(npc.Center.X + 10f, npc.Center.Y + 10f), Main.player[0].position, mod.ProjectileType("True_FinalLight"), damage, 0f);
            Projectile.NewProjectile(new Vector2(npc.Center.X - 10f, npc.Center.Y - 10f), Main.player[0].position, mod.ProjectileType("True_FinalLight"), damage, 0f);
        }
    }
}
