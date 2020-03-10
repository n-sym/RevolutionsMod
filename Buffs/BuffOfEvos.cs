using Terraria;
using Terraria.ModLoader;

namespace Revolutions.Buffs
{
    public class BuffOfEvos : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("BOE");
            Description.SetDefault("BOE");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            RevolutionsPlayer modPlayer = player.GetModPlayer<RevolutionsPlayer>();
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.CoreWeapon.Evolutionaries>()] > 0)
            {
                modPlayer.evolutionary = true;
            }
            if (!modPlayer.evolutionary)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            else
            {
                player.buffTime[buffIndex] = 9999;
            }
        }
    }

}
