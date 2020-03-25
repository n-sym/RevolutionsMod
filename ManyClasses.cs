using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
    public class PowerUIElement
    {
        public PowerUIElement parent;
        public List<PowerUIElement> children = new List<PowerUIElement>();
        public Vector2 MyPosition;
        public Vector2 MySize;
        public Action MouseUponMe;
        public Action MouseNotUponMe;
        public Action MouseClickMe;
        public Texture2D MyTexture;
        bool MouseDown;
        bool LastMouseStatus;
        public PowerUIElement()
        {
            parent = null;
            MyPosition = Vector2.Zero;
            MySize = Vector2.Zero;
            MyTexture = null;
        }
        public void Active()
        {
            if (MySize == Vector2.Zero && MyTexture != null) MySize = new Vector2(MyTexture.Width, MyTexture.Height);
            if (LastMouseStatus != Main.mouseLeft && Main.mouseLeft) MouseDown = true;
            else MouseDown = false;
            if (Helper.InTheRange(MyPosition, MySize, Main.MouseScreen) && !MouseDown && MouseUponMe != null) MouseUponMe();
            else if (Helper.InTheRange(MyPosition, MySize, Main.MouseScreen) && MouseDown && MouseClickMe != null) MouseClickMe();
            else if (!Helper.InTheRange(MyPosition, MySize, Main.MouseScreen) && MouseNotUponMe!= null) MouseNotUponMe();
            LastMouseStatus = Main.mouseLeft;
        }
        public void Append(PowerUIElement child)
        {
            child.parent = this;
            children.Add(child);
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (MyTexture != null) spriteBatch.Draw(MyTexture, MyPosition, Color.White);
            DrawChildren(spriteBatch);
        }
        public void DrawChildren(SpriteBatch spriteBatch)
        {
            foreach(PowerUIElement child in children)
            {
                if(child != null)child.Draw(spriteBatch);
            }
            //children => child
        }
    }
}
