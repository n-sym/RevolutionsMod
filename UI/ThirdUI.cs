using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Revolutions.Utils;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace Revolutions.UI
{
    internal class ThirdUI : UIState
    {
        UIPanel UIPanel1;
        UIText UIText1;
        UIText Blurset;
        UIText ADset;
        UIText MTset;
        UIText AUset;
        UIText Exit;
        public static bool cesfTips;
        public override void OnInitialize()
        {
            UIPanel1 = new UIPanel();
            UIPanel1.SetPadding(0);
            UIPanel1.Left.Set(Main.screenWidth / 2 - 320, 0f);
            UIPanel1.Top.Set(Main.screenHeight / 2 - 240, 0f);
            UIPanel1.Width.Set(640f, 0f);
            UIPanel1.Height.Set(480f, 0f);
            //UIPanel1.BorderColor = Color.Black;
            UIPanel1.BackgroundColor = new Color(63, 82, 151) * 0.7f;
            UIText1 = new UIText(Language.GetTextValue("Mods.Revolutions.UI.RevoSets"), 0.7f, true);
            UIText1.Top.Set(25, 0f);
            UIText1.Left.Set(320 - 0.5f * Helper.GetStringLength(Main.fontDeathText, UIText1.Text, 0.7f), 0f);
            UIPanel1.Append(UIText1);
            Blurset = new UIText(Language.GetTextValue("Mods.Revolutions.UI.Blur" + Revolutions.Settings.blur.ToString()));
            Blurset.Top.Set(80, 0f);
            Blurset.Left.Set(320 - 0.5f * Helper.GetStringLength(Main.fontDeathText, Blurset.Text, 0.4f), 0f);
            Blurset.OnClick += Click;
            Blurset.OnMouseOver += Hover;
            UIPanel1.Append(Blurset);
            ADset = new UIText(Language.GetTextValue("Mods.Revolutions.UI.AutoDoor" + Revolutions.Settings.autodoor.ToString()));
            ADset.Top.Set(120, 0f);
            ADset.Left.Set(320 - 0.5f * Helper.GetStringLength(Main.fontDeathText, ADset.Text, 0.4f), 0f);
            ADset.OnClick += Click;
            ADset.OnMouseOver += Hover;
            UIPanel1.Append(ADset);
            MTset = new UIText(Language.GetTextValue("Mods.Revolutions.UI.Mutter" + Revolutions.Settings.mutter.ToString()));
            MTset.Top.Set(160, 0f);
            MTset.Left.Set(320 - 0.5f * Helper.GetStringLength(Main.fontDeathText, MTset.Text, 0.4f), 0f);
            MTset.OnClick += Click;
            MTset.OnMouseOver += Hover;
            UIPanel1.Append(MTset);
            AUset = new UIText(Language.GetTextValue("Mods.Revolutions.UI.AutoReuse" + Revolutions.Settings.autoreuse.ToString()));
            AUset.Top.Set(200, 0f);
            AUset.Left.Set(320 - 0.5f * Helper.GetStringLength(Main.fontDeathText, AUset.Text, 0.4f), 0f);
            AUset.OnClick += Click;
            AUset.OnMouseOver += Hover;
            UIPanel1.Append(AUset);
            Exit = new UIText(Language.GetTextValue("LegacyMenu.118"), 0.4f, true);
            Exit.Top.Set(440, 0f);
            Exit.Left.Set(320 - 0.5f * Helper.GetStringLength(Main.fontDeathText, Exit.Text, 0.4f), 0f);
            Exit.OnClick += Click;
            Exit.OnMouseOver += Hover;
            UIPanel1.Append(Exit);
            Append(UIPanel1);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (RevolutionsPlayer.logoTimer == 90)
            {
                UIPanel1.Left.Set(Main.screenWidth / 2 - 320, 0f);
                UIPanel1.Top.Set(Main.screenHeight / 2 - 240, 0f);
            }
            Blurset.SetText(Language.GetTextValue("Mods.Revolutions.UI.Blur" + Revolutions.Settings.blur.ToString()), 0.4f, true);
            if (Blurset.IsMouseHovering) Blurset.TextColor = Color.White; else Blurset.TextColor = Color.Gray;
            ADset.SetText(Language.GetTextValue("Mods.Revolutions.UI.AutoDoor" + Revolutions.Settings.autodoor.ToString()), 0.4f, true);
            if (ADset.IsMouseHovering) ADset.TextColor = Color.White; else ADset.TextColor = Color.Gray;
            MTset.SetText(Language.GetTextValue("Mods.Revolutions.UI.Mutter" + Revolutions.Settings.mutter.ToString()), 0.4f, true);
            if (MTset.IsMouseHovering) MTset.TextColor = Color.White; else MTset.TextColor = Color.Gray;
            AUset.SetText(Language.GetTextValue("Mods.Revolutions.UI.AutoReuse" + Revolutions.Settings.autoreuse.ToString()), 0.4f, true);
            if (AUset.IsMouseHovering) AUset.TextColor = Color.White; else AUset.TextColor = Color.Gray;

            if (Exit.IsMouseHovering) Exit.TextColor = Color.White; else Exit.TextColor = Color.Gray;
            if (Main.inFancyUI) DrawChildren(spriteBatch);
        }
        public void Click(UIMouseEvent evt, UIElement listeningElement)
        {
            Main.soundInstanceMenuTick.Play();
            switch(listeningElement.Top.GetValue(0f))
            {
                case 80:
                    Revolutions.Settings.blur = !Revolutions.Settings.blur;
                    break;
                case 120:
                    Revolutions.Settings.autodoor = !Revolutions.Settings.autodoor;
                    break;
                case 160:
                    Revolutions.Settings.mutter = !Revolutions.Settings.mutter;
                    break;
                case 200:
                    Revolutions.Settings.autoreuse = !Revolutions.Settings.autoreuse;
                    break;
                case 440:
                    Main.InGameUI = new UserInterface();
                    Main.inFancyUI = false;
                    Main.playerInventory = true;
                    Main.soundMenuClose.Play();
                    break;
            }
        }
        public void Hover(UIMouseEvent evt, UIElement listeningElement)
        {
            Main.soundInstanceMenuTick.Play();
        }
    }
}
