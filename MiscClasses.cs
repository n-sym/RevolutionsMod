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
    public class DamageType
    {
        public static int Ranged = 1;
        public static int Magic = 2;
        public static int Melee = 3;
        public static int Summon = 4;
        public static int Thrown = 5;
        public static int StarFlare = 6;
    }
}
