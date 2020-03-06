using Terraria;
using Terraria.ModLoader;

namespace Revolutions.Dusts
{
    public class Pixel : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.noLight = true;
        }
        public override bool Update(Dust dust)
        {
            Lighting.AddLight(dust.position, dust.color.R / 250, dust.color.G / 250, dust.color.B / 250);
            Lighting.AddLight(dust.position, 0.75f, 0.75f, 0.75f);
            if (!dust.noLight) dust.active = false;
            dust.noLight = false;
            return false;
        }
    }
}