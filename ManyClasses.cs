using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Graphics;
using Revolutions.Utils;
using System;
using System.Collections.Generic;
using Terraria;

namespace Revolutions
{
    public class PlayerWithNum
    {
        public Player player;
        public float num;
    }
    public class StringTimerInt
    {
        public string text = "";
        public int number = 0;
        public int timer = 0;
        public StringTimerInt(string talk, int num, int time)
        {
            text = talk;
            number = num;
            timer = time;
        }
    }
    internal class PowerUIElement
    {
        public PowerUIElement parent;
        public List<PowerUIElement> children = new List<PowerUIElement>();
        public Vector2 MyRelativePos;
        public Vector2 MySize;
        public Action MouseUponMe;
        public Action MouseNotUponMe;
        public Action MouseClickMe;
        public Texture2D MyTexture;
        public Point MyCenterType;
        public int MyID;
        public bool UseMouseFix;
        public bool IsMouseUpon;
        public bool IsFocusOn;
        public Vector2 MyPosition;
        public Vector2 MyCenterFix;
        bool MouseDown;
        bool LastMouseStatus;
        public PowerUIElement()
        {
            UseMouseFix = true;
            parent = null;
            MyPosition = Vector2.Zero;
            MySize = Vector2.Zero;
            MyTexture = null;
            MyCenterType = CenterType.TopLeft;
            MouseClickMe += Upon;
            MouseUponMe += Upon;
            MouseNotUponMe += NotUpon;
        }
        public void Active()
        {
            MyPosition = parent == null ? MyRelativePos : MyRelativePos + parent.MyPosition - parent.MyCenterFix;
            if (MySize == Vector2.Zero && MyTexture != null) MySize = new Vector2(MyTexture.Width, MyTexture.Height);
            if ((LastMouseStatus != Main.mouseLeft && Main.mouseLeft) || (!UseMouseFix && Main.mouseLeft)) MouseDown = true;
            else MouseDown = false;
            MyCenterFix = MySize;
            MyCenterFix.X *= MyCenterType.X == 1 ? 0 : (MyCenterType.X == 2 ? 0.5f : 1);
            MyCenterFix.Y *= MyCenterType.Y == 1 ? 0 : (MyCenterType.Y == 2 ? 0.5f : 1);
            IsFocusOn = Helper.InTheRange(MyPosition - MyCenterFix, MySize, Main.MouseScreen);
            foreach (PowerUIElement child in children)
            {
                if (child != null && child.IsFocusOn) IsFocusOn = false;
            }
            if (IsFocusOn && !MouseDown) MouseUponMe();
            else if (IsFocusOn && MouseDown) MouseClickMe();
            else if (!Helper.InTheRange(MyPosition - MyCenterFix, MySize, Main.MouseScreen)) MouseNotUponMe();
            LastMouseStatus = Main.mouseLeft;
            CustomActive();
            ActiveChildren();
        }
        public void Upon() { IsMouseUpon = true; Main.LocalPlayer.delayUseItem = true; }
        public void NotUpon() { IsMouseUpon = false; }
        public virtual void CustomActive() { }
        public void Append(PowerUIElement child)
        {
            child.parent = this;
            children.Add(child);
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (MyTexture != null)
            {
                Vector2 CenterFix = MySize * Main.UIScale;
                CenterFix.X *= MyCenterType.X == 1 ? 0 : (MyCenterType.X == 2 ? 0.5f : 1);
                CenterFix.Y *= MyCenterType.Y == 1 ? 0 : (MyCenterType.Y == 2 ? 0.5f : 1);
                spriteBatch.Draw(MyTexture, MyPosition - CenterFix, Color.White);
            }
            DrawChildren(spriteBatch);
        }
        public void DrawChildren(SpriteBatch spriteBatch)
        {
            foreach(PowerUIElement child in children)
            {
                if(child != null)child.Draw(spriteBatch);
            }
        }
        public void ActiveChildren()
        {
            foreach (PowerUIElement child in children)
            {
                if (child != null) child.Active();
            }
        }
        public override string ToString()
        {
            return "Position:" + MyPosition.ToString() + "ID:" + MyID.ToString();
        }
    }
    internal class CenterType
    {
        public static Point TopLeft = new Point(1, 1);
        public static Point MiddleLeft = new Point(1, 2);
        public static Point BottomLeft = new Point(1, 3);
        public static Point TopCenter = new Point(2, 1);
        public static Point MiddleCenter = new Point(2, 2);
        public static Point BottomCenter = new Point(2, 3);
        public static Point TopRight = new Point(3, 1);
        public static Point MiddleRight = new Point(3, 2);
        public static Point BottomRight = new Point(3, 3);
    }
    internal class PowerUIText : PowerUIElement
    {
        public string MyText;
        public float MyScale;
        public Color MyMainColor;
        public Color MyBorderColor;
        public DynamicSpriteFont MyFont;
        public PowerUIText()
        {
            MyText = "";
            MyScale = 1f;
            MyMainColor = Color.White;
            MyBorderColor = Color.Black;
            MyFont = Main.fontDeathText;
        }
        public override void CustomActive()
        {
            MySize = Helper.GetStringSize(MyFont, MyText, MyScale);
            MyCenterFix = Helper.GetStringSize(MyFont, MyText, MyScale);
            MyCenterFix.X *= MyCenterType.X == 1 ? 0 : (MyCenterType.X == 2 ? 0.5f : 1);
            MyCenterFix.Y *= MyCenterType.Y == 1 ? 0 : (MyCenterType.Y == 2 ? 0.5f : 1);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Terraria.Utils.DrawBorderStringFourWay(spriteBatch, MyFont, MyText, MyPosition.X - MyCenterFix.X, MyPosition.Y - MyCenterFix.Y, MyMainColor, MyBorderColor, Vector2.Zero, MyScale);
        }
    }
    internal class PowerUITextButton : PowerUIText
    {
        public bool UseColorReact;
        public bool UseSizeReact;
        Color MyAltColor;
        float SizeFix;
        bool active;

        public PowerUITextButton()
        {
            UseColorReact = true;
            UseSizeReact = true;
            MyAltColor = Color.White;
            MouseClickMe += ClickMe;
            MouseUponMe += UponMe;
            MouseNotUponMe += NotUponMe;
            SizeFix = 1f;
        }
        public void ClickMe() { Main.soundInstanceMenuTick.Play(); }
        public void UponMe() { if(!active)Main.soundInstanceMenuTick.Play();  active = true; }
        public void NotUponMe() { active = false; }
        public override void CustomActive()
        {
            if (active && SizeFix < 1.12f) SizeFix += 0.03f;
            if (!active && SizeFix > 1f) SizeFix -= 0.03f;
            MyAltColor = Helper.GetCloserColor(MyMainColor, Color.Gray, 1.12f - (UseColorReact ? SizeFix : 1.12f), 0.12f);
            MySize = Helper.GetStringSize(MyFont, MyText, MyScale * (UseSizeReact ? SizeFix : 1f));
            MyCenterFix = Helper.GetStringSize(MyFont, MyText, MyScale * SizeFix);
            MyCenterFix.X *= MyCenterType.X == 1 ? 0 : (MyCenterType.X == 2 ? 0.5f : 1);
            MyCenterFix.Y *= MyCenterType.Y == 1 ? 0 : (MyCenterType.Y == 2 ? 0.5f : 1);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
           Terraria.Utils.DrawBorderStringFourWay(spriteBatch, MyFont, MyText, MyPosition.X - MyCenterFix.X, MyPosition.Y - MyCenterFix.Y, MyAltColor, MyBorderColor, Vector2.Zero, MyScale * SizeFix);
        }
    }
    internal class PowerUIPanel : PowerUIElement
    {
        public Color MyMainColor;
        public Color MyBorderColor;
        public PowerUIPanel()
        {
            MyMainColor = Color.White;
            MyBorderColor = Color.Black;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 CenterFix = MySize;
            CenterFix.X *= MyCenterType.X == 1 ? 0 : (MyCenterType.X == 2 ? 0.5f : 1);
            CenterFix.Y *= MyCenterType.Y == 1 ? 0 : (MyCenterType.Y == 2 ? 0.5f : 1);
            spriteBatch.Draw(Main.magicPixel, MyPosition - CenterFix, new Rectangle(0, 0, (int)MySize.X, 2), MyBorderColor);
            spriteBatch.Draw(Main.magicPixel, MyPosition - CenterFix + new Vector2(0, MySize.Y), new Rectangle(0, 0, (int)MySize.X + 2, 2), MyBorderColor);
            spriteBatch.Draw(Main.magicPixel, MyPosition - CenterFix, new Rectangle(0, 0, 2, (int)MySize.Y), MyBorderColor);
            spriteBatch.Draw(Main.magicPixel, MyPosition - CenterFix + new Vector2(MySize.X, 0), new Rectangle(0, 0, 2, (int)MySize.Y), MyBorderColor);
            spriteBatch.Draw(Main.magicPixel, MyPosition - CenterFix + new Vector2(2, 2), new Rectangle(2, 2, (int)MySize.X - 2, (int)MySize.Y - 2), MyMainColor);
            DrawChildren(spriteBatch);
        }
    }
}
