using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolutions.NPCs
{
    public class RevolutionsGlobalBuff : GlobalBuff
    {
        public override void Update(int type, NPC npc, ref int buffIndex)
        {
            switch(type)
            {
                case BuffID.OnFire:
                case BuffID.Frostburn:
                case BuffID.CursedInferno:
                    npc.lifeRegen -= (int)(npc.lifeMax * 0.012f);
                    break;
            }
        }
    }
}
