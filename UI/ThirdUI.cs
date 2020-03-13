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
        UIText BlurSet;
        UIText AutoDoorSet;
        UIText MutterSet;
        UIText AutoReuseSet;
        UIText HthBarSet;
        UIText RangeIndexSet;
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
            BlurSet = new UIText(Language.GetTextValue("Mods.Revolutions.UI.Blur" + Revolutions.Settings.blur.ToString()));
            BlurSet.Top.Set(80, 0f);
            BlurSet.Left.Set(320 - 0.5f * Helper.GetStringLength(Main.fontDeathText, BlurSet.Text, 0.4f), 0f);
            BlurSet.OnClick += Click;
            BlurSet.OnMouseOver += Hover;
            UIPanel1.Append(BlurSet);
            AutoDoorSet = new UIText(Language.GetTextValue("Mods.Revolutions.UI.AutoDoor" + Revolutions.Settings.autodoor.ToString()));
            AutoDoorSet.Top.Set(120, 0f);
            AutoDoorSet.Left.Set(320 - 0.5f * Helper.GetStringLength(Main.fontDeathText, AutoDoorSet.Text, 0.4f), 0f);
            AutoDoorSet.OnClick += Click;
            AutoDoorSet.OnMouseOver += Hover;
            UIPanel1.Append(AutoDoorSet);
            MutterSet = new UIText(Language.GetTextValue("Mods.Revolutions.UI.Mutter" + Revolutions.Settings.mutter.ToString()));
            MutterSet.Top.Set(160, 0f);
            MutterSet.Left.Set(320 - 0.5f * Helper.GetStringLength(Main.fontDeathText, MutterSet.Text, 0.4f), 0f);
            MutterSet.OnClick += Click;
            MutterSet.OnMouseOver += Hover;
            UIPanel1.Append(MutterSet);
            AutoReuseSet = new UIText(Language.GetTextValue("Mods.Revolutions.UI.AutoReuse" + Revolutions.Settings.autoreuse.ToString()));
            AutoReuseSet.Top.Set(200, 0f);
            AutoReuseSet.Left.Set(320 - 0.5f * Helper.GetStringLength(Main.fontDeathText, AutoReuseSet.Text, 0.4f), 0f);
            AutoReuseSet.OnClick += Click;
            AutoReuseSet.OnMouseOver += Hover;
            UIPanel1.Append(AutoReuseSet);
            HthBarSet = new UIText(Language.GetTextValue("Mods.Revolutions.UI.HealthBar" + Revolutions.Settings.hthbar.ToString()));
            HthBarSet.Top.Set(240, 0f);
            HthBarSet.Left.Set(320 - 0.5f * Helper.GetStringLength(Main.fontDeathText, HthBarSet.Text, 0.4f), 0f);
            HthBarSet.OnClick += Click;
            HthBarSet.OnMouseOver += Hover;
            UIPanel1.Append(HthBarSet);
            RangeIndexSet = new UIText(Language.GetTextValue("Mods.Revolutions.UI.RangeIndex" + Revolutions.Settings.rangeIndex.ToString()));
            RangeIndexSet.Top.Set(280, 0f);
            RangeIndexSet.Left.Set(320 - 0.5f * Helper.GetStringLength(Main.fontDeathText, RangeIndexSet.Text, 0.4f), 0f);
            RangeIndexSet.OnClick += Click;
            RangeIndexSet.OnMouseOver += Hover;
            UIPanel1.Append(RangeIndexSet);
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
            BlurSet.SetText(Language.GetTextValue("Mods.Revolutions.UI.Blur" + Revolutions.Settings.blur.ToString()), 0.4f, true);
            if (BlurSet.IsMouseHovering) BlurSet.TextColor = Color.White; else BlurSet.TextColor = Color.Gray;
            AutoDoorSet.SetText(Language.GetTextValue("Mods.Revolutions.UI.AutoDoor" + Revolutions.Settings.autodoor.ToString()), 0.4f, true);
            if (AutoDoorSet.IsMouseHovering) AutoDoorSet.TextColor = Color.White; else AutoDoorSet.TextColor = Color.Gray;
            MutterSet.SetText(Language.GetTextValue("Mods.Revolutions.UI.Mutter" + Revolutions.Settings.mutter.ToString()), 0.4f, true);
            if (MutterSet.IsMouseHovering) MutterSet.TextColor = Color.White; else MutterSet.TextColor = Color.Gray;
            AutoReuseSet.SetText(Language.GetTextValue("Mods.Revolutions.UI.AutoReuse" + Revolutions.Settings.autoreuse.ToString()), 0.4f, true);
            if (AutoReuseSet.IsMouseHovering) AutoReuseSet.TextColor = Color.White; else AutoReuseSet.TextColor = Color.Gray;
            HthBarSet.SetText(Language.GetTextValue("Mods.Revolutions.UI.HealthBar" + Revolutions.Settings.hthbar.ToString()), 0.4f, true);
            if (HthBarSet.IsMouseHovering) HthBarSet.TextColor = Color.White; else HthBarSet.TextColor = Color.Gray;
            RangeIndexSet.SetText(Language.GetTextValue("Mods.Revolutions.UI.RangeIndex" + Revolutions.Settings.rangeIndex.ToString()), 0.4f, true);
            if (RangeIndexSet.IsMouseHovering) RangeIndexSet.TextColor = Color.White; else RangeIndexSet.TextColor = Color.Gray;

            if (Exit.IsMouseHovering) Exit.TextColor = Color.White; else Exit.TextColor = Color.Gray;
            if (Main.inFancyUI) DrawChildren(spriteBatch);
        }
        public void Click(UIMouseEvent evt, UIElement listeningElement)
        {
            Main.soundInstanceMenuTick.Play();
            switch (listeningElement.Top.GetValue(0f))
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
                case 240:
                    Revolutions.Settings.hthbar = !Revolutions.Settings.hthbar;
                    break;
                case 280:
                    Revolutions.Settings.rangeIndex++;
                    if (Revolutions.Settings.rangeIndex == 3) Revolutions.Settings.rangeIndex = 0;
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
