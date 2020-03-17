using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        public static int[] myOldLife = new int[Main.npc.Length];
        public static int[] myTimer= new int[Main.npc.Length];
        public static int[] myTimer2 = new int[Main.npc.Length];
        public override void AI(NPC npc)
        {
            if (Revolutions.Settings.extraAI)
            {
                if (myTalkCD[npc.whoAmI] > 0) myTalkCD[npc.whoAmI]--;
                if (myTimer[npc.whoAmI] > 0) myTimer[npc.whoAmI]--;
                if (myTimer2[npc.whoAmI] > 0) myTimer2[npc.whoAmI]--;
                if (myTimer[npc.whoAmI] < 0) myTimer[npc.whoAmI]++;
                if (myTimer2[npc.whoAmI] < 0) myTimer2[npc.whoAmI]++;
                //一计时器两用，太酷了
                Player target = Main.player[npc.target];
                RevolutionsPlayer revtar = target.GetModPlayer<RevolutionsPlayer>();
                switch (npc.type)
                {
                    case NPCID.DukeFishron:
                        if (Main.rand.Next(1, 600) == 1 && myTalkCD[npc.whoAmI] == 0)
                        {
                            new Talk(npc.whoAmI, Language.GetTextValue("Mods.Revolutions.Talk.DkFsion0" + Main.rand.Next(1, 4).ToString()), 180, null);
                            myTalkCD[npc.whoAmI] = 360;
                        }
                        RevolutionsAI.DukeFishronAI(npc, target, revtar, ref myTimer[npc.whoAmI], ref myTimer2[npc.whoAmI], myOldLife[npc.whoAmI]);
                        break;
                    case NPCID.Plantera:
                        if (Main.rand.Next(1, 600) == 1 && myTalkCD[npc.whoAmI] == 0)
                        {
                            new Talk(npc.whoAmI, Language.GetTextValue("Mods.Revolutions.Talk.Plantera0" + Main.rand.Next(1, 3).ToString()), 180, null);
                            myTalkCD[npc.whoAmI] = 360;
                        }
                        RevolutionsAI.PlanteraAI(npc, target, revtar, ref myTimer[npc.whoAmI], ref myTimer2[npc.whoAmI], myOldLife[npc.whoAmI]);
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
                if (RevolutionsPlayer.timer % 10 == 0 && RevolutionsPlayer.timer != 0) myOldLife[npc.whoAmI] = npc.life;
            }
        }
        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {
            switch (npc.type)
            {
                case NPCID.DukeFishron:
                    RevolutionsAI.DukeFishronDraw(npc, spriteBatch);
                    break;
            }
           return base.PreDraw(npc, spriteBatch, drawColor);
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
        public override void NPCLoot(NPC npc)
        {
            myTalkCD[npc.whoAmI] = 0;
            myTimer[npc.whoAmI] = 0;
        }

        public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
        {
            if (npc.dontTakeDamage) return false;
            return base.CanBeHitByProjectile(npc, projectile);
        }
        public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
        {
            if (npc.dontTakeDamage) return false;
            return base.CanBeHitByItem(npc, player, item);
        }
        public override void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit)
        {
            RevolutionsPlayer revply = target.GetModPlayer<RevolutionsPlayer>();
            if (revply.nowBoss != null && revply.nowBoss.type != NPCID.Plantera) damage = (int)(damage * ((revply.difficulty * revply.difficulty / 13334f) + 0.75f));
            if (revply.nowBoss != null && revply.nowBoss.type == NPCID.Plantera) damage = (int)(damage * ((revply.difficulty * revply.difficulty / 50000f) + 1f));
        }
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            RevolutionsPlayer revply = Main.player[projectile.owner].GetModPlayer<RevolutionsPlayer>();
            switch (npc.type)
            {
                case NPCID.DukeFishron:
                    if (npc.ai[0] == 11 || npc.ai[0] == 12) damage *= 2;
                    break;
            }
        }
    }
}
