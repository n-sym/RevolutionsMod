using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Revolutions.UI;
using Revolutions.Utils;
//using ReLogic.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace Revolutions
{
    public class Revolutions : Mod
    {
        public static Mod mod;
        public static ModHotKey TimeTravelingPotion;
        public static bool TTPHP;
        public Revolutions()
        {
        }
        public override void Load()
        {
            mod = this;
            TimeTravelingPotion = RegisterHotKey("Time Traveling Potion", "Q");
            //Helper.EntroptPool = new int[65537];
            Helper.EntroptPool = new int[10001];
            for (int i = 0; i < 10000; i++)
            {
                Random rd = new Random(i * i * i - i * i);
                Helper.EntroptPool[i] = rd.Next(-101, 101);
            }
            Helper.WriteXML("12345", "12345");
        }
        public override void Unload()
        {
            Helper.EntroptPool = new int[0];
            TimeTravelingPotion = null;
        }
        public override void PostDrawInterface(SpriteBatch spriteBatch)
        {
            FirstUI firstUI = new FirstUI();
            firstUI.Draw(spriteBatch);
            SecondUI secondUI = new SecondUI();
            secondUI.Draw(spriteBatch);
        }
        public override void HotKeyPressed(string name)
        {

        }

    }
}

