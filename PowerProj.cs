using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Revolutions
{
    public abstract class PowerProj : ModProjectile
    {
        public Vector2[] PositionSave = { Vector2.Zero, Vector2.Zero, Vector2.Zero };
        public Vector2[] HyperOldPositon = new Vector2[31];
        public int[] timer = { 0, 0, 0, 0, 0 };
        public bool[] switches = { false, false, false, false, false };
    }
}

