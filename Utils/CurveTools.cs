using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace Revolutions
{
    public class Curve
    {
        public static Vector2 GetEllipse(float radian, float a, float b, float rotate)
        {
            return new Vector2(a * (float)Math.Cos(radian), b * (float)Math.Sin(radian)).RotatedBy(rotate);
        }
        public static Vector2 GetHyperbola(float radian, float a, float b, float rotate)
        {
            return new Vector2(a / (float)Math.Cos(radian), b * (float)Math.Tan(radian)).RotatedBy(rotate);
        }
    }

}
