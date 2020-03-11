using Terraria;
using Terraria.ModLoader;

namespace Revolutions.NPCs
{
    public class RevolutionsGlobalItem : GlobalItem
    {
        public override bool CanUseItem(Item item, Player player)
        {
            if (!item.autoReuse && !item.channel && Revolutions.Settings.autoreuse) item.autoReuse = true;
            return base.CanUseItem(item, player);
        }
    }
}
