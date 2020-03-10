using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolutions.NPCs
{
    // This ModNPC serves as an example of a complete AI example.
    public class DPSslime : ModNPC
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Flutter Slime"); // Automatic from .lang files
            //Main.npcFrameCount[npc.type] = 6; // make sure to set this for your modnpcs.
        }
        public override void SetDefaults()
        {
            npc.width = 320;
            npc.height = 320;
            npc.aiStyle = -1; // This npc has a completely unique AI, so we set this to -1.
            npc.damage = 0;
            npc.defense = 0;
            npc.lifeMax = 2000000000;
            npc.knockBackResist = 0;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            //npc.alpha = 175;
            //npc.color = new Color(0, 80, 255, 100);
            npc.value = Item.sellPrice(0, 0, 0, 0);
            npc.noGravity = true;
            npc.noTileCollide = false;
        }
        public override void AI()
        {
            Texture2D Bar = Revolutions.mod.GetTexture("UI/WhiteLine");
            SpriteBatch sprite = Main.spriteBatch;
            sprite.Begin();
            sprite.Draw(Bar, new Vector2(100, 100), new Rectangle(0, 0, (int)(Bar.Width), Bar.Height), Color.White * 0.33f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            sprite.End();
            if (Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.P)) npc.scale += 0.1f;
            if (Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.O)) npc.scale -= 0.1f;
        }
        /*public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[npc.type].Width, npc.height);
            Main.spriteBatch.End();
            Vector2 drawPosition = npc.position;
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);

            drawPosition += -Main.screenPosition;
                    spriteBatch.Draw(Main.projectileTexture[ModContent.ProjectileType<RareWeapon.MeteowerHelper>()], drawPosition, null, new Color((126), (int)(171), (int)(243), (int)(255 * npc.scale)), npc.rotation, drawOrigin, 10f, SpriteEffects.None, 0f);
         
            return false;
        }*/
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (npc.spriteDirection == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            drawColor.A = (byte)(npc.scale * 255);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);

            // Retrieve reference to shader


            float extraDrawY = Main.NPCAddHeight(npc.whoAmI);
            Vector2 origin = new Vector2(Main.npcTexture[npc.type].Width / 2, Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type] / 2);
            Main.spriteBatch.Draw(Main.npcTexture[npc.type],
                new Vector2(npc.position.X - Main.screenPosition.X + npc.width / 2 - (float)Main.npcTexture[npc.type].Width * npc.scale / 2f + origin.X * npc.scale,
                npc.position.Y - Main.screenPosition.Y + npc.height - Main.npcTexture[npc.type].Height * npc.scale / Main.npcFrameCount[npc.type] + 4f + extraDrawY + origin.Y * npc.scale + npc.gfxOffY),
                npc.frame,
                npc.GetAlpha(drawColor), npc.rotation, origin, npc.scale, spriteEffects, 0f);

            // Restart spriteBatch to reset applied shaders
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.Transform);

            // Prevent Vanilla drawing
            return false;
        }


    }
}
