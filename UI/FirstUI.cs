using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Revolutions.Utils;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;
using Microsoft.Xna.Framework.Input;
using Terraria.ModLoader;

namespace Revolutions.UI
{
    internal class FirstUI : UIState
    {
        FirstEle firstEle;
        DragableUIPanel dragableUIPanel;
        public static bool cesfTips;
        public override void OnInitialize()
        {
            dragableUIPanel = new DragableUIPanel();
            dragableUIPanel.SetPadding(0);
            dragableUIPanel.Left.Set(Main.screenWidth - 460, 0f);
            dragableUIPanel.Top.Set(6f, 0f);
            dragableUIPanel.Width.Set(170f, 0f);
            dragableUIPanel.Height.Set(47f, 0f);
            dragableUIPanel.BorderColor = Color.Transparent;
            dragableUIPanel.BackgroundColor = Color.Transparent;
            dragableUIPanel.OnMouseOver += TipsCESF;
            dragableUIPanel.OnMouseOut += DelTipsCESF;
            firstEle = new FirstEle();
            dragableUIPanel.Append(firstEle);
            Append(dragableUIPanel);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if(RevolutionsPlayer.logoTimer == 90)
            {
                dragableUIPanel.Left.Set(Main.screenWidth - 460, 0f);
            }
            DrawChildren(spriteBatch);
        }
        public void TipsCESF(UIMouseEvent evt, UIElement listeningElement)
        {
            cesfTips = true;
        }
        public void DelTipsCESF(UIMouseEvent evt, UIElement listeningElement)
        {
            cesfTips = false;
        }
    }

    internal class FirstEle : UIElement
    {
        public override void Draw(SpriteBatch spriteBatch)
        {
            CalculatedStyle innerDimensions = GetInnerDimensions();
            Vector2 myPos = new Vector2(innerDimensions.X, innerDimensions.Y);
            Player playera = Main.LocalPlayer;
            string text = Language.GetTextValue("Mods.Revolutions.Corepower:");
            text += playera.GetModPlayer<RevolutionsPlayer>().corePower[0].ToString();
            Terraria.Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, text, myPos.X, myPos.Y, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor,
                            (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Transparent, Vector2.Zero, 1f);
            text = Language.GetTextValue("Mods.Revolutions.Starflare:");
            text += playera.GetModPlayer<RevolutionsPlayer>().starFlare[0].ToString() + "/" + playera.GetModPlayer<RevolutionsPlayer>().maxStarFlare.ToString();
            Terraria.Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, text, myPos.X, myPos.Y + 24f, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor,
                            (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Transparent, Vector2.Zero, 1f);
            if(FirstUI.cesfTips)
            {
                myPos = Main.MouseWorld - Main.screenPosition;
                text = Language.GetTextValue("Mods.Revolutions.cetip");
                myPos.X -= 0.66f * Helper.GetStringLength(Main.fontMouseText, text, 1f);
                Terraria.Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, text, myPos.X, myPos.Y + 24f, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor,
                            (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Transparent, Vector2.Zero, 1f);
                text = Language.GetTextValue("Mods.Revolutions.sftip");
                Terraria.Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, text, myPos.X, myPos.Y + 48f, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor,
                            (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Transparent, Vector2.Zero, 1f);

            }
            foreach (Player player in Main.player)
            {
                if (player.active)
                {
                    string talk = player.GetModPlayer<RevolutionsPlayer>().nowSaying;
                    float v = Helper.GetStringLength(Main.fontMouseText, talk, 0.8f);
                    if (player.GetModPlayer<RevolutionsPlayer>().talkActive > 0)
                    {
                        Terraria.Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, talk, (player.Center.X - Main.screenPosition.X) / Main.UIScale - (0.5f * v * Main.GameZoomTarget), (player.position.Y - Main.screenPosition.Y) / Main.UIScale - (30 * Main.GameZoomTarget),
                        new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor),
                        Color.Black, new Vector2(0, 0), 0.8f * Main.GameZoomTarget);
                    }

                }
            }
            for (int i = 0; i < RevolutionsPlayer.npctalk.Count; i++)
            {
                string talk = RevolutionsPlayer.npctalk[i].text;
                float v = Helper.GetStringLength(Main.fontMouseText, talk, 0.8f);
                if (RevolutionsPlayer.npctalk[0].timer > 0)
                {
                    NPC n = Main.npc[RevolutionsPlayer.npctalk[i].number];
                    Terraria.Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, talk, (n.Center.X - Main.screenPosition.X) / Main.UIScale - (0.5f * v * Main.GameZoomTarget), (n.position.Y - Main.screenPosition.Y) / Main.UIScale - (30 * Main.GameZoomTarget),
                    new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor),
                    Color.Black, new Vector2(0, 0), 0.8f * Main.GameZoomTarget);
                }
            }
        }
    }
}