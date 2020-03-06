using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Revolutions.Utils;
using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace Revolutions.UI
{
    internal class SecondUI : UIElement
    {
        int timer = 0;
        int life;
        public SecondUI()
        {
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!Main.playerInventory)
            {
                Texture2D Bar = Revolutions.mod.GetTexture("UI/WhiteLine");
                float scale = 0.5f;
                NPC boss = Main.player[0].GetModPlayer<RevolutionsPlayer>().nowBoss;
                if (boss != null && boss.active)
                {
                    string text = boss.TypeName;
                    if (boss.type == 398) text = Language.GetTextValue("NPCName.MoonLordHead");
                    if (boss.type == 125) text = Language.GetTextValue("BuffName.TwinEyesMinion");
                    if (boss.type == 126) text = Language.GetTextValue("BuffName.TwinEyesMinion");
                    float v = Helper.GetStringLength(Main.fontDeathText, text, 0.8f);
                    Terraria.Utils.DrawBorderStringFourWay(spriteBatch, Main.fontDeathText, text,
                        0.5f * Main.screenWidth - 0.5f * v, 50f, Color.White, Color.Transparent, Vector2.Zero, 0.8f);
                    text = Main.player[0].GetModPlayer<RevolutionsPlayer>().nowBossLifeTrue.ToString() + "/" + Main.player[0].GetModPlayer<RevolutionsPlayer>().nowBossLifeMax.ToString();
                    v = Helper.GetStringLength(Main.fontMouseText, text, 0.8f);
                    float a = (float)(Bar.Width * Helper.GetCloserSingle(Main.player[0].GetModPlayer<RevolutionsPlayer>().nowBossLife
                       , Main.player[0].GetModPlayer<RevolutionsPlayer>().nowBossLifeTrue
                       , Math.Sin(0.1745 * (RevolutionsPlayer.timer2 - 1)), 1) / Main.player[0].GetModPlayer<RevolutionsPlayer>().nowBossLifeMax);
                    Terraria.Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, text,
                        0.5f * (Main.screenWidth + scale * Bar.Width) - v, 70f, Color.White, Color.Transparent, Vector2.Zero, 0.8f);
                    spriteBatch.Draw(Bar, new Vector2(0.5f * Main.screenWidth - 0.5f * scale * Bar.Width, 102.5f), new Rectangle(0, 0, (int)(Bar.Width), Bar.Height), Color.White * 0.33f, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    if (boss.type == ModContent.NPCType<NPCs.Core>()) Bar = Revolutions.mod.GetTexture("UI/RainbowLine");
                    spriteBatch.Draw(Bar, new Vector2(0.5f * Main.screenWidth - 0.5f * scale * Bar.Width, 102.5f), new Rectangle(0, 0, (int)a, Bar.Height), Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                }
                else
                {
                    timer = 0;
                }
            }
        }
    }
}
