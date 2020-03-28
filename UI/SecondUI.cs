using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
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
        //显然是第二个UI
        public SecondUI()
        {
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!Main.playerInventory)
            {
                Texture2D Bar = Revolutions.mod.GetTexture("UI/WhiteLine");
                float scale = 0.5f;
                NPC boss = RevolutionsPlayer.nowBoss;
                RevolutionsPlayer.HthBarTimer -= RevolutionsPlayer.HthBarTimer > 0 ? 1 : 0;
                if ((boss != null && boss.active) != RevolutionsPlayer.lastHthBarStatus && Revolutions.Settings.extraAI && boss.active) RevolutionsPlayer.HthBarTimer = 30;
                float drawPosFix = 0;
                SoundEffect soundEffect = Revolutions.mod.GetSound("Sounds/Custom/sword");
                if (RevolutionsPlayer.HthBarTimer == 10) soundEffect.Play();
                if (RevolutionsPlayer.HthBarTimer != 0) drawPosFix = (1 - (30 - RevolutionsPlayer.HthBarTimer) * (30 - RevolutionsPlayer.HthBarTimer) / 900f) * Main.screenWidth * Main.UIScale;
                if (boss != null && boss.active)
                {
                    string text = boss.TypeName;
                    if (boss.type == 398) text = Language.GetTextValue("NPCName.MoonLordHead");
                    if (boss.type == 125) text = Language.GetTextValue("BuffName.TwinEyesMinion");
                    if (boss.type == 126) text = Language.GetTextValue("BuffName.TwinEyesMinion");
                    //月主和魔眼的名字特殊处理
                    float v = Helper.GetStringLength(Main.fontDeathText, text, 0.8f);
                    Terraria.Utils.DrawBorderStringFourWay(spriteBatch, Main.fontDeathText, text,
                        0.5f * Main.screenWidth - 0.5f * v - drawPosFix, 50f, Color.White, Color.Transparent, Vector2.Zero, 0.8f);
                    text = RevolutionsPlayer.nowBossLifeTrue.ToString() + "/" + RevolutionsPlayer.nowBossLifeMax.ToString();
                    int posfix = 0;
                    if (v > 269) posfix = 45;
                    v = Helper.GetStringLength(Main.fontMouseText, text, 0.8f);
                    float a = (float)(Bar.Width * Helper.GetCloserSingle(RevolutionsPlayer.nowBossLife
                       , RevolutionsPlayer.nowBossLifeTrue
                       , Math.Sin(0.1745 * (RevolutionsPlayer.timer2 - 1)), 1) / RevolutionsPlayer.nowBossLifeMax);
                    Terraria.Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, text,
                        0.5f * (Main.screenWidth + scale * Bar.Width) - v + drawPosFix, 72f + posfix, Color.White, Color.Transparent, Vector2.Zero, 0.8f);
                    spriteBatch.Draw(Bar, new Vector2(0.5f * Main.screenWidth - 0.5f * scale * Bar.Width + drawPosFix, 102.5f), new Rectangle(0, 0, (int)(Bar.Width), Bar.Height), Color.White * 0.33f, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    if (boss.type == ModContent.NPCType<NPCs.Core>()) Bar = Revolutions.mod.GetTexture("UI/RainbowLine");
                    spriteBatch.Draw(Bar, new Vector2(0.5f * Main.screenWidth - 0.5f * scale * Bar.Width + drawPosFix, 102.5f), new Rectangle(0, 0, (int)a, Bar.Height), Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                }
                RevolutionsPlayer.lastHthBarStatus = (boss != null && boss.active);
            }
        }
    }
}
