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
        /// <summary>
        /// 自定义UI的中心对齐方式，使用CenterType类
        /// </summary>
        public Point MyCenterType;
        /// <summary>
        /// 自定义UI的ID，默认-1
        /// </summary>
        public int MyID;
        /// <summary>
        /// 自定义UI信息
        /// </summary>
        public object MyCunstomData;
        /// <summary>
        /// 是否在鼠标松开前不重复执行Click事件
        /// </summary>
        public bool UseMouseFix;
        public bool IsMouseUpon;
        /// <summary>
        /// 鼠标是否在UI之上且不在子UI之上
        /// </summary>
        public bool IsFocusOn;
        /// <summary>
        /// 一般不需要修改这个参数，用时获取
        /// </summary>
        public Vector2 MyPosition;
        /// <summary>
        /// 一般不需要修改这个参数，它是中心对齐时的偏移量
        /// </summary>
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
            MyID = -1;
            MyCunstomData = null;
            MyCenterType = CenterType.TopLeft;
            MouseClickMe += Upon;
            MouseUponMe += Upon;
            MouseNotUponMe += NotUpon;
        }
        /// <summary>
        /// 在Draw之前手动调用，更新UI的信息
        /// </summary>
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
        private void Upon() { IsMouseUpon = true; Main.LocalPlayer.mouseInterface = true; }
        private void NotUpon() { IsMouseUpon = false; }
        /// <summary>
        /// 在Active之后自动调用，用于自定义信息
        /// </summary>
        public virtual void CustomActive() { }
        /// <summary>
        /// 附加子UI
        /// </summary>
        public void Append(PowerUIElement child)
        {
            child.parent = this;
            children.Add(child);
        }
        /// <summary>
        /// 移除子UI，如果子UI不存在返回false
        /// </summary>
        public bool Subtract(PowerUIElement child)
        {
            if (children.Contains(child))
            {
                child.parent = null;
                return children.Remove(child);
            }
            return false;
        }
        /// <summary>
        /// 在Active之后手动调用，默认绘制UI的贴图，可以修改
        /// </summary>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (MyTexture != null)
            {
                Vector2 CenterFix = MySize;
                CenterFix.X *= MyCenterType.X == 1 ? 0 : (MyCenterType.X == 2 ? 0.5f : 1);
                CenterFix.Y *= MyCenterType.Y == 1 ? 0 : (MyCenterType.Y == 2 ? 0.5f : 1);
                spriteBatch.Draw(MyTexture, MyPosition - CenterFix, Color.White);
            }
            DrawChildren(spriteBatch);
        }
        /// <summary>
        /// 在Draw之后自动调用，如果重写Draw则需要手动调用，依次执行子UI的Draw方法
        /// </summary>
        public void DrawChildren(SpriteBatch spriteBatch)
        {
            foreach(PowerUIElement child in children)
            {
                if(child != null)child.Draw(spriteBatch);
            }
        }
        /// <summary>
        /// 在Active之后自动调用，依次执行子UI的Active方法
        /// </summary>
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
        public Color MyColor;
        public PowerUIPanel()
        {
            MyColor = Color.White;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Terraria.Utils.DrawInvBG(spriteBatch, MyPosition.X - MyCenterFix.X, MyPosition.Y - MyCenterFix.Y, MySize.X, MySize.Y, MyColor); 
            DrawChildren(spriteBatch);
        }
    }
    internal class PowerUIItemSlot : PowerUIPanel
    {
        public Item MyItem;
        public bool UseSizeReact;
        float SizeFix;
        bool active;
        public PowerUIItemSlot()
        {
            UseSizeReact = true;
            MyItem = new Item();
            MySize = new Vector2(52, 52); 
            MyColor = new Color(63, 82, 151) * 0.7f;
            MouseClickMe += ClickMe;
            MouseUponMe += UponMe;
            MouseNotUponMe += NotUponMe;
            SizeFix = 1f;
        }
        public void ClickMe()
        {
            if (Main.mouseItem.type != 0)
            {
                MyItem = Main.mouseItem;
                Main.mouseItem = new Item();
                Main.soundInstanceGrab.Play();
            }
            else if (Main.mouseItem.type == 0 && MyItem.type != 0)
            {
                Main.mouseItem = MyItem;
                MyItem = new Item();
                Main.soundInstanceGrab.Play();
            }
        }
        public void UponMe()
        { 
            active = Main.mouseItem.type != 0 && UseSizeReact;
            Main.mouseText = true;
        }
        public void NotUponMe() { active = false; }
        public override void CustomActive()
        {
            MySize = new Vector2(52, 52) * SizeFix;
            if (active && SizeFix < 1.08f) SizeFix += 0.02f;
            if (!active && SizeFix > 1f) SizeFix -= 0.02f;
            MyCenterFix = new Vector2((int)Math.Floor(MyCenterFix.X), (int)Math.Floor(MyCenterFix.Y));
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Terraria.Utils.DrawInvBG(spriteBatch, MyPosition.X - MyCenterFix.X, MyPosition.Y - MyCenterFix.Y, MySize.X, MySize.Y, MyColor);
            DrawChildren(spriteBatch);
            if (MyItem.type != 0)
            {
                Texture2D tex = Main.itemTexture[MyItem.type];
                float scale = 0.8f * SizeFix;
                if (tex.Width > 42) scale *= 42f / tex.Width;
                spriteBatch.Draw(tex, MyPosition - MyCenterFix + new Vector2(26, 26) * SizeFix - tex.Size() * scale * 0.5f, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0f);
                if (MyItem.stack != 1) Terraria.Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, MyItem.stack.ToString(), MyPosition.X - MyCenterFix.X + 6 * SizeFix, MyPosition.Y - MyCenterFix.Y + 32 * SizeFix, Color.White, Color.Black, Vector2.Zero, 0.8f * SizeFix);
            }
        }
    }
    internal class PowerUIButton : PowerUIPanel
    {
        public bool UseSizeReact;
        /// <summary>
        /// 调整这个数值，而非MySize
        /// </summary>
        public Vector2 ButtonSize;
        float SizeFix;
        bool active;
        public PowerUIButton()
        {
            UseSizeReact = true;
            MySize = Vector2.Zero;
            MyColor = new Color(63, 82, 151) * 0.7f;
            MouseUponMe += UponMe;
            MouseNotUponMe += NotUponMe;
            SizeFix = 1f;
        }
        public void UponMe() { active = UseSizeReact; }
        public void NotUponMe() { active = false; }
        public override void CustomActive()
        {
            MySize = ButtonSize * SizeFix;
            if (active && SizeFix < 1.08f) SizeFix += 0.02f;
            if (!active && SizeFix > 1f) SizeFix -= 0.02f;
            MyCenterFix = new Vector2((int)Math.Floor(MyCenterFix.X), (int)Math.Floor(MyCenterFix.Y));
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if(MyTexture == null)Terraria.Utils.DrawInvBG(spriteBatch, MyPosition.X - MyCenterFix.X, MyPosition.Y - MyCenterFix.Y, MySize.X, MySize.Y, MyColor);
            else base.Draw(spriteBatch);
            DrawChildren(spriteBatch);
        }
    }
}
