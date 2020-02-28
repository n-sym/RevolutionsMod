using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Revolutions.Dusts
{
    public class LightFadeOut : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.noLight = true;
        }
        public override bool Update(Dust dust)
        {
            dust.velocity = Vector2.Zero;
            Lighting.AddLight(dust.position, dust.color.R / 250, dust.color.G / 250, dust.color.B / 250);
            Lighting.AddLight(dust.position, 0.75f, 0.75f, 0.75f);
            if (dust.alpha == 252) dust.active = false;
            dust.alpha += 4;
            return false;
        }
    }
}