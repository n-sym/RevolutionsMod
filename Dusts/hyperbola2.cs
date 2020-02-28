using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Revolutions.Dusts
{
    public class hyperbola2 : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.noLight = true;
            dust.frame = new Rectangle(0, 0, 19, 19);
            dust.rotation = 0.7853f;

        }
        int kill = 0;
        public override bool Update(Dust dust)
        {
            if (dust.alpha == 0) dust.position.Y -= 13.43f;
            Lighting.AddLight(dust.position, dust.color.R / 250, dust.color.G / 250, dust.color.B / 250);
            Lighting.AddLight(dust.position, 1.5f, 1.5f, 1.5f);
            //dust.rotation += 0.0314f;
            //dust.scale *= 0.8f;
            dust.alpha += 60;
            if (dust.alpha == 240) dust.active = false;
            return false;
        }
    }
}