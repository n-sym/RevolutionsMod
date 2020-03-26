using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        UIText SpColorSet;
        UIText ExtraAISet;
        UIText Exit;
        bool MyStatus = false;
        int MyOffset = 0;
        public static bool cesfTips;
        public override void OnInitialize()
        {
            UIPanel1 = new UIPanel();
            UIPanel1.SetPadding(0);
            UIPanel1.Left.Set(Main.screenWidth / 2 - 300, 0f);
            UIPanel1.Top.Set(Main.screenHeight / 2 - 210, 0f);
            UIPanel1.Width.Set(600f, 0f);
            UIPanel1.Height.Set(420f, 0f);
            //UIPanel1.BorderColor = Color.Black;
            UIPanel1.BackgroundColor = new Color(63, 82, 151) * 0.7f;
            UIText1 = new UIText(Language.GetTextValue("Mods.Revolutions.UI.RevoSets"), 0.7f, true);
            UIText1.Top.Set(25, 0f);
            UIText1.Left.Set(300 - 0.5f * Helper.GetStringLength(Main.fontDeathText, UIText1.Text, 0.7f), 0f);
            UIPanel1.Append(UIText1);
            BlurSet = new UIText(Language.GetTextValue("Mods.Revolutions.UI.Blur" + Revolutions.Settings.blur.ToString()));
            BlurSet.Top.Set(80, 0f);
            BlurSet.Left.Set(300 - 0.5f * Helper.GetStringLength(Main.fontDeathText, BlurSet.Text, 0.4f), 0f);
            BlurSet.OnClick += Click;
            BlurSet.OnMouseOver += Hover;
            UIPanel1.Append(BlurSet);
            AutoDoorSet = new UIText(Language.GetTextValue("Mods.Revolutions.UI.AutoDoor" + Revolutions.Settings.autodoor.ToString()));
            AutoDoorSet.Top.Set(120, 0f);
            AutoDoorSet.Left.Set(300 - 0.5f * Helper.GetStringLength(Main.fontDeathText, AutoDoorSet.Text, 0.4f), 0f);
            AutoDoorSet.OnClick += Click;
            AutoDoorSet.OnMouseOver += Hover;
            UIPanel1.Append(AutoDoorSet);
            MutterSet = new UIText(Language.GetTextValue("Mods.Revolutions.UI.Mutter" + Revolutions.Settings.mutter.ToString()));
            MutterSet.Top.Set(160, 0f);
            MutterSet.Left.Set(300 - 0.5f * Helper.GetStringLength(Main.fontDeathText, MutterSet.Text, 0.4f), 0f);
            MutterSet.OnClick += Click;
            MutterSet.OnMouseOver += Hover;
            UIPanel1.Append(MutterSet);
            AutoReuseSet = new UIText(Language.GetTextValue("Mods.Revolutions.UI.AutoReuse" + Revolutions.Settings.autoreuse.ToString()));
            AutoReuseSet.Top.Set(200, 0f);
            AutoReuseSet.Left.Set(300 - 0.5f * Helper.GetStringLength(Main.fontDeathText, AutoReuseSet.Text, 0.4f), 0f);
            AutoReuseSet.OnClick += Click;
            AutoReuseSet.OnMouseOver += Hover;
            UIPanel1.Append(AutoReuseSet);
            HthBarSet = new UIText(Language.GetTextValue("Mods.Revolutions.UI.HealthBar" + Revolutions.Settings.hthbar.ToString()));
            HthBarSet.Top.Set(320, 0f);
            HthBarSet.Left.Set(300 - 0.5f * Helper.GetStringLength(Main.fontDeathText, HthBarSet.Text, 0.4f), 0f);
            HthBarSet.OnClick += Click;
            HthBarSet.OnMouseOver += Hover;
            UIPanel1.Append(HthBarSet);
            RangeIndexSet = new UIText(Language.GetTextValue("Mods.Revolutions.UI.RangeIndex" + Revolutions.Settings.rangeIndex.ToString()));
            RangeIndexSet.Top.Set(280, 0f);
            RangeIndexSet.Left.Set(300 - 0.5f * Helper.GetStringLength(Main.fontDeathText, RangeIndexSet.Text, 0.4f), 0f);
            RangeIndexSet.OnClick += Click;
            RangeIndexSet.OnMouseOver += Hover;
            UIPanel1.Append(RangeIndexSet);
            SpColorSet = new UIText(Language.GetTextValue("Mods.Revolutions.UI.SpColor" + Revolutions.Settings.spcolor.ToString()));
            SpColorSet.Top.Set(360, 0f);
            SpColorSet.Left.Set(300 - 0.5f * Helper.GetStringLength(Main.fontDeathText, SpColorSet.Text, 0.4f), 0f);
            SpColorSet.OnClick += Click;
            SpColorSet.OnMouseOver += Hover;
            if (Helper.Name2Specialname(Main.LocalPlayer.name) != "none") UIPanel1.Append(SpColorSet);
            ExtraAISet = new UIText(Language.GetTextValue("Mods.Revolutions.UI.ExtraAI" + Revolutions.Settings.extraAI.ToString()));
            ExtraAISet.Top.Set(240, 0f);
            ExtraAISet.Left.Set(300 - 0.5f * Helper.GetStringLength(Main.fontDeathText, ExtraAISet.Text, 0.4f), 0f);
            ExtraAISet.OnClick += Click;
            ExtraAISet.OnMouseOver += Hover;
            UIPanel1.Append(ExtraAISet);
            Exit = new UIText(Language.GetTextValue("LegacyMenu.118"), 0.4f, true);
            Exit.Top.Set(440, 0f);
            Exit.Left.Set(300 - 0.5f * Helper.GetStringLength(Main.fontDeathText, Exit.Text, 0.4f), 0f);
            Exit.OnClick += Click;
            Exit.OnMouseOver += Hover;
            UIPanel1.Append(Exit);
            if (Revolutions.Settings.hthbar) { MyStatus = true; MyOffset = 50; }
            Append(UIPanel1);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (MyStatus && MyOffset < 50) MyOffset += 2;
            if (!MyStatus && MyOffset > 0) MyOffset -= 2;
            var CanUseStatus =  MyStatus ? 50 - MyOffset : MyOffset;
            CanUseStatus *= MyStatus ? -CanUseStatus : CanUseStatus;
            if (MyStatus) CanUseStatus += 2500;
            if(Helper.Name2Specialname(Main.LocalPlayer.name) != "none")
            {
                UIPanel1.Top.Set(Main.screenHeight / 2 - 230 - (CanUseStatus / 100), 0f);
                UIPanel1.Height.Set(460f + (CanUseStatus / 50), 0f);
            }
            else
            {
                UIPanel1.Top.Set(Main.screenHeight / 2 - 210 - (CanUseStatus / 100), 0f);
                UIPanel1.Height.Set(420f + (CanUseStatus / 50), 0f);
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
            ExtraAISet.SetText(Language.GetTextValue("Mods.Revolutions.UI.ExtraAI" + Revolutions.Settings.extraAI.ToString()), 0.4f, true);
            if (ExtraAISet.IsMouseHovering) ExtraAISet.TextColor = Color.White; else ExtraAISet.TextColor = Color.Gray; 
            SpColorSet.SetText(Language.GetTextValue("Mods.Revolutions.UI.SpColor" + Revolutions.Settings.spcolor.ToString()), 0.4f, true);
            if (SpColorSet.IsMouseHovering) SpColorSet.TextColor = Color.White; else SpColorSet.TextColor = Color.Gray;
            SpColorSet.Top.Set(UIPanel1.Height.GetValue(0) - 80, 0);
            if (Exit.IsMouseHovering) Exit.TextColor = Color.White; else Exit.TextColor = Color.Gray;
            Exit.Top.Set(UIPanel1.Height.GetValue(0) - 40, 0);
            RemoveAllChildren();
            Append(UIPanel1);
            if (Main.inFancyUI) DrawChildren(spriteBatch);
        }
        public void Click(UIMouseEvent evt, UIElement listeningElement)
        {
            if (!Main.ingameOptionsWindow)
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
                    case 320:
                        Revolutions.Settings.hthbar = !Revolutions.Settings.hthbar;
                        MyStatus = Revolutions.Settings.hthbar;
                        break;
                    case 280:
                        Revolutions.Settings.rangeIndex++;
                        if (Revolutions.Settings.rangeIndex == 3) Revolutions.Settings.rangeIndex = 0;
                        break;
                    case 240:
                        Revolutions.Settings.extraAI = !Revolutions.Settings.extraAI;
                        break;
                        
                }
                if(listeningElement.Top.GetValue(0f) == listeningElement.Parent.Height.GetValue(0f) - 40)
                {
                    Main.InGameUI = new UserInterface();
                    Main.inFancyUI = false;
                    Main.playerInventory = true;
                    Main.soundMenuClose.Play();
                }
                if (listeningElement.Top.GetValue(0f) == listeningElement.Parent.Height.GetValue(0f) - 80)
                {
                    Revolutions.Settings.spcolor = !Revolutions.Settings.spcolor;
                }
            }
        }
        public void Hover(UIMouseEvent evt, UIElement listeningElement)
        {
            if (!Main.ingameOptionsWindow) Main.soundInstanceMenuTick.Play();
        }
    }
}
