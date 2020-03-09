using Revolutions.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Revolutions.NPCs
{
    public class RevolutionsGlobalNPC : GlobalNPC
    {
        public override void AI(NPC npc)
        {
            switch(npc.type)
            {
                case NPCID.DukeFishron:
                    if(Main.rand.Next(1, 600) == 1)new Talk(npc.whoAmI, Language.GetTextValue("Mods.Revolutions.Talk.DkFsion0" + Main.rand.Next(1, 4).ToString()), 180, null);
                    if (npc.ai[0] == -1 || npc.ai[0] == 4 || npc.ai[0] == 9) npc.dontTakeDamage = true;
                    if (npc.ai[0] == 6 && npc.ai[2] == 20)
                    {

                        Projectile.NewProjectile(npc.Center + new Vector2(0, 20).RotatedBy(npc.rotation), Helper.ToUnitVector(Main.player[npc.target].Center + Main.player[npc.target].velocity * 30 - npc.Center) * 10, ModContent.ProjectileType<Projectiles.WaterArrow>(), npc.damage / 2, 6);
                        Projectile.NewProjectile(npc.Center + new Vector2(0, -20).RotatedBy(npc.rotation), Helper.ToUnitVector(Main.player[npc.target].Center + Main.player[npc.target].velocity * 30 - npc.Center) * 10, ModContent.ProjectileType<Projectiles.WaterArrow>(), npc.damage / 2, 6);

                    }
                    else npc.dontTakeDamage = false;
                    break;
                case NPCID.Plantera:
                    if (Main.rand.Next(1, 600) == 1) new Talk(npc.whoAmI, Language.GetTextValue("Mods.Revolutions.Talk.Plantera0" + Main.rand.Next(1, 3).ToString()), 180, null);
                    break;
            }
        }
    }
}
