using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
namespace Revolutions.Utils
{
    class NPCdistanceComparer : IComparer<NPC>
    {
        Entity e = null;
        public NPCdistanceComparer(Entity entity)
        {
            e = entity;
        }
        public int Compare(NPC x, NPC y)
        {
            try
            {
                if (Vector2.Distance(x.Center, e.Center) > Vector2.Distance(y.Center, e.Center))
                {
                    return -1;
                }
                else if (Vector2.Distance(x.Center, e.Center) < Vector2.Distance(y.Center, e.Center))
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }
    }
}
