using Microsoft.Xna.Framework;
using Revolutions.Utils;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Revolutions.NPCs
{
    public class RevolutionsGlobalNPC : GlobalNPC
    {
        public override void AI(NPC npc)
        {
            switch (npc.type)
            {
                case NPCID.DukeFishron:
                    if (Main.rand.Next(1, 600) == 1) new Talk(npc.whoAmI, Language.GetTextValue("Mods.Revolutions.Talk.DkFsion0" + Main.rand.Next(1, 4).ToString()), 180, null);
                    if (npc.ai[0] == -1 || npc.ai[0] == 4 || npc.ai[0] == 9 || npc.ai[0] == 10) npc.dontTakeDamage = true;
                    else npc.dontTakeDamage = false;
                    if (npc.ai[0] == 6 && npc.ai[2] == 20)
                    {
                        Projectile.NewProjectile(npc.Center + new Vector2(0, 20).RotatedBy(npc.rotation), Helper.ToUnitVector(Main.player[npc.target].Center + Main.player[npc.target].velocity * 30 - npc.Center) * 10, ModContent.ProjectileType<Projectiles.OriBoss.WaterArrow>(), npc.damage / 4, 6);
                        Projectile.NewProjectile(npc.Center + new Vector2(0, -20).RotatedBy(npc.rotation), Helper.ToUnitVector(Main.player[npc.target].Center + Main.player[npc.target].velocity * 30 - npc.Center) * 10, ModContent.ProjectileType<Projectiles.OriBoss.WaterArrow>(), npc.damage / 4, 6);
                    }
                    if (npc.ai[0] == 10 && npc.ai[0] == 11 && npc.ai[0] == 12) ;
                    break;
                case NPCID.Plantera:
                    if (Main.rand.Next(1, 600) == 1) new Talk(npc.whoAmI, Language.GetTextValue("Mods.Revolutions.Talk.Plantera0" + Main.rand.Next(1, 3).ToString()), 180, null);
                    break;
            }
        }
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            switch (npc.type)
            {
                case NPCID.DukeFishron:
                    if (npc.ai[0] == 11 || npc.ai[0] == 12) damage *= 2;
                    break;
            }
        }
    }
}
