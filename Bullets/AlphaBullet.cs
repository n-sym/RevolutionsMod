using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Revolutions.Projectiles.RareWeapon;
using Revolutions.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Revolutions.Bullets
{
    class AlphaBullet : PowerBullet
    {
        public AlphaBullet()
        {
            timeLeft = 30;
            size = new Vector2(12, 12);
        }
        float a, b, m, n;
        int t;
        bool x = false;
        public override void AI()
        {
            t++;
            if (x == false)
            {
                x = true;
                m = velocity.X;
                n = velocity.Y;
                a = extra[0] - velocity.X;
                b = extra[1] - velocity.Y;
            }
            velocity.X = t / 15.45f * a + m;
            velocity.Y = t / 15.45f * b + n;
            //实际上可以用Helper.GetCloser()重写，但是我懒得这么做
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 drawOrigin = new Vector2(0, 0);
            for (int k = 0; k < 10; k++)
            {
                if (oldPosition[k] == Vector2.Zero) break;
                Vector2 drawPosition = 0.35f * oldPosition[k] + 0.65f * position;
                drawPosition -= Main.screenPosition;
                float sizeFix = 2;
                sizeFix /= 1 + k;
                sizeFix -= 1;
                float sizeFix2 = 11;
                sizeFix2 /= 1 + k;
                sizeFix2 -= 1;
                Color color = new Color(240, 127, 225);
                color = new Color(color.R, color.G, color.B, (int)(255 * sizeFix)) * sizeFix2 * 0.9f;
                spriteBatch.Draw(Main.projectileTexture[ModContent.ProjectileType<Projectiles.RareWeapon.MeteowerHelper>()], drawPosition, null, color, 0, drawOrigin, 0.3f, SpriteEffects.None, 0f);
            }
        }
    }
}
