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
        public static int[] myTalkCD = new int[Main.npc.Length];
        public override void AI(NPC npc)
        {

            switch (npc.type)
            {
                case NPCID.DukeFishron:
                    Player target = Main.player[npc.target];
                    RevolutionsPlayer revtar = target.GetModPlayer<RevolutionsPlayer>();
                    if (Main.rand.Next(1, 600) == 1 && myTalkCD[npc.whoAmI] == 0)
                    {
                        new Talk(npc.whoAmI, Language.GetTextValue("Mods.Revolutions.Talk.DkFsion0" + Main.rand.Next(1, 4).ToString()), 180, null);
                        myTalkCD[npc.whoAmI] = 360;
                    }
                    if (npc.ai[0] == -1 || npc.ai[0] == 4 || npc.ai[0] == 9 || npc.ai[0] == 10) npc.dontTakeDamage = true;
                    else npc.dontTakeDamage = false;
                    if (npc.ai[0] == 6 && npc.ai[2] == 20)
                    {
                        Projectile.NewProjectile(npc.Center + new Vector2(0, 20).RotatedBy(npc.rotation), Helper.ToUnitVector(Main.player[npc.target].Center + Main.player[npc.target].velocity * 30 - npc.Center) * 10, ModContent.ProjectileType<Projectiles.OriBoss.WaterArrow>(), npc.damage / 4, 6);
                        Projectile.NewProjectile(npc.Center + new Vector2(0, -20).RotatedBy(npc.rotation), Helper.ToUnitVector(Main.player[npc.target].Center + Main.player[npc.target].velocity * 30 - npc.Center) * 10, ModContent.ProjectileType<Projectiles.OriBoss.WaterArrow>(), npc.damage / 4, 6);
                    }
                    if ((npc.velocity - target.velocity).ToRotation() < 0) npc.velocity = Helper.GetCloser(npc.velocity, target.velocity + npc.velocity, 1, 40);
                    if (npc.ai[0] == 6) npc.velocity = Helper.GetCloser(Helper.ToUnitVector(npc.velocity), Helper.ToUnitVector(target.Center - npc.Center), 1 + revtar.difficulty / 28, 20) * npc.velocity.Length();
                    break;
                case NPCID.Plantera:
                    if (Main.rand.Next(1, 600) == 1 && myTalkCD[npc.whoAmI] == 0)
                    {
                        new Talk(npc.whoAmI, Language.GetTextValue("Mods.Revolutions.Talk.Plantera0" + Main.rand.Next(1, 3).ToString()), 180, null);
                        myTalkCD[npc.whoAmI] = 360;
                    }
                    break;
                case NPCID.Guide:
                    if (Main.rand.Next(1, 100) == 1 && myTalkCD[npc.whoAmI] == 0 && Main.dayTime)
                    {
                        new Talk(npc.whoAmI, Language.GetTextValue("Mods.Revolutions.Talk.Guide1" + Main.rand.Next(1, 3).ToString()), 180, null);
                        myTalkCD[npc.whoAmI] = 600;
                    }
                    if (Main.rand.Next(1, 100) == 1 && myTalkCD[npc.whoAmI] == 0 && !Main.dayTime)
                    {
                        new Talk(npc.whoAmI, Language.GetTextValue("Mods.Revolutions.Talk.Guide3" + Main.rand.Next(1, 3).ToString()), 180, null);
                        myTalkCD[npc.whoAmI] = 600;
                    }
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
        public override void TownNPCAttackProj(NPC npc, ref int projType, ref int attackDelay)
        {
            if (NPC.downedMoonlord)
            {
                switch (npc.type)
                {
                    case NPCID.Guide:
                        projType = ProjectileID.HolyArrow;
                        if (myTalkCD[npc.whoAmI] == 0)
                        {
                            new Talk(npc.whoAmI, Language.GetTextValue("Mods.Revolutions.Talk.Guide5" + Main.rand.Next(1, 3).ToString()), 180, null);
                            myTalkCD[npc.whoAmI] = 200;
                        }
                        break;
                    case NPCID.ArmsDealer:
                        projType = ProjectileID.BulletHighVelocity;
                        break;
                }
            }
        }
        public override void TownNPCAttackStrength(NPC npc, ref int damage, ref float knockback)
        {
            if (NPC.downedMoonlord)
            {
                switch (npc.type)
                {
                    case NPCID.Guide:
                        damage = 56;
                        knockback = 4f;
                        break;
                    case NPCID.ArmsDealer:
                        damage = 40;
                        knockback = 2f;
                        break;
                    case NPCID.Dryad:
                        damage = 30;
                        knockback = 1f;
                        break;
                    case NPCID.Demolitionist:
                        damage = 400;
                        break;
                }
            }
        }
        public override void DrawTownAttackGun(NPC npc, ref float scale, ref int item, ref int closeness)
        {
            if (NPC.downedMoonlord)
            {
                switch (npc.type)
                {
                    case NPCID.Guide:
                        item = ItemID.HallowedRepeater;
                        break;
                    case NPCID.ArmsDealer:
                        item = ItemID.Megashark;
                        break;
                }
            }
        }
        public override void TownNPCAttackCooldown(NPC npc, ref int cooldown, ref int randExtraCooldown)
        {
            if (NPC.downedMoonlord)
            {
                switch (npc.type)
                {
                    case NPCID.Guide:
                        cooldown = 19;
                        randExtraCooldown = Main.rand.Next(0, 5);
                        break;
                    case NPCID.ArmsDealer:
                        cooldown = 10;
                        randExtraCooldown = Main.rand.Next(0, 5);
                        break;
                }
            }
        }
    }
}
