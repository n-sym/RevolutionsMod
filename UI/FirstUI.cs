using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Revolutions.Utils;
using Terraria;
using Terraria.Localization;
using Terraria.UI;

namespace Revolutions.UI
{
    internal class FirstUI : UIElement
    {

        public override void Draw(SpriteBatch spriteBatch)
        {
            Player playera = Main.player[0];
            string text = Language.GetTextValue("Mods.Revolutions.Corepower:");
            text += playera.GetModPlayer<RevolutionsPlayer>().corePower[0].ToString();
            Terraria.Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, text, -440 + Main.screenWidth, 6f, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor,
                            (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Transparent, Vector2.Zero, 1f);
            text = Language.GetTextValue("Mods.Revolutions.Starflare:");
            text += playera.GetModPlayer<RevolutionsPlayer>().starFlare[0].ToString() + "/" + playera.GetModPlayer<RevolutionsPlayer>().maxStarFlare.ToString();
            Terraria.Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, text, -440 + Main.screenWidth, 30f, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor,
                            (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Transparent, Vector2.Zero, 1f);

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

        }
    }
}
