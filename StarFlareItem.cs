using Terraria;
using Terraria.ModLoader;

namespace Revolutions
{
    public abstract class SFitem : ModItem
    {
        public int sfCosume;

        public override bool CanUseItem(Player player)
        {
            if (player.GetModPlayer<RevolutionsPlayer>().starFlare[0] > sfCosume)
            {
                player.GetModPlayer<RevolutionsPlayer>().starFlare[0] -= sfCosume;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

