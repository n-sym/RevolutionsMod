using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Revolutions
{
    public abstract class PowerProj : ModProjectile
    {
        public Vector2[] PositionSave = { Vector2.Zero, Vector2.Zero, Vector2.Zero };
        public Vector2[] HyperOldPositon = new Vector2[31];
    }
}

