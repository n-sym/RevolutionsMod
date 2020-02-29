using Microsoft.Xna.Framework;
using Revolutions.Utils;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolutions.Projectiles
{
    public class Evolutionaries : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Evo");
        }
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.aiStyle = -1;
            projectile.timeLeft = 3;
            projectile.penetrate = -1;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.scale = 1.5f;
            projectile.netImportant = true;
            projectile.minionSlots = 1;
            projectile.minion = true;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 5;

        }
        public override void AI()
        {
            if(projectile.ai[0] > 0) projectile.ai[0]--;
            Player player = Main.player[projectile.owner];
            RevolutionsPlayer modPlayer = player.GetModPlayer<RevolutionsPlayer>();
            if (player.dead)
            {
                modPlayer.evolutionary = false; ;
            }
            if (modPlayer.evolutionary)
            {
                projectile.timeLeft = 2;
            }
            NPC target = null;
            float distance = 1500f;
            float dis2 = 0;
            Random rd = new Random();
            foreach (NPC npc in Main.npc)
            {
                if (Vector2.Distance(npc.Center, projectile.Center) < distance
                    &&npc.type != NPCID.TargetDummy && npc.active
                    && Vector2.Distance(npc.Center, projectile.Center) < 1500f
                    && !npc.friendly
                    && npc.CanBeChasedBy(projectile))
                {
                    distance = Vector2.Distance(npc.Center, projectile.Center);
                    dis2 = Vector2.Distance(npc.Center, player.Center);
                    target = npc;
                }
                if (dis2 < 300f && npc.type != NPCID.TargetDummy && npc.active
                    && !npc.friendly
                    && npc.CanBeChasedBy(projectile))
                {
                    target = npc;
                }
            }
            if (target == null)
            {
                projectile.rotation += 3.14f / 270;
                if (Vector2.Distance(player.Center, projectile.Center) > 110f)
                {
                    projectile.velocity = 0.2f * player.velocity + 0.6f * projectile.oldVelocity + Helper.ToUnitVector(Helper.GetCloser(projectile.Center, player.Center, 3, 4) - projectile.Center) * Vector2.Distance(player.Center, projectile.Center) / 300f;
                }
                projectile.velocity.X += Helper.EntroptPool[projectile.whoAmI + rd.Next(1, 500)] * 0.005f;
                projectile.velocity.Y += Helper.EntroptPool[projectile.whoAmI + rd.Next(501, 1000)] * 0.005f;
                //if(Vector2.Distance(player.Center, projectile.Center) > 500f) projectile.velocity += 2f * player.velocity;
            }
            else if (dis2 < 300f)
            {
                projectile.rotation += 3.14f / 90;
                projectile.velocity = 0.6f * projectile.oldVelocity + Helper.ToUnitVector(target.Center - projectile.Center) * Vector2.Distance(target.Center, projectile.Center) / 50f;
                projectile.velocity.X += Helper.EntroptPool[projectile.whoAmI + rd.Next(1, 500)] * 0.007f;
                projectile.velocity.Y += Helper.EntroptPool[projectile.whoAmI + rd.Next(501, 1000)] * 0.007f;
            }
            else
            {
                projectile.rotation += 3.14f / 150;
                if (Vector2.Distance(player.Center, projectile.Center) > 110f)
                {
                    projectile.velocity = 0.2f * player.velocity + 0.8f * projectile.oldVelocity + Helper.ToUnitVector(Helper.GetCloser(projectile.Center, player.Center, 1, 2) - projectile.Center) * Vector2.Distance(player.Center, projectile.Center) / 600f;
                }
                Vector2 speed = 100 * Helper.ToUnitVector(target.Center - projectile.Center);
                projectile.velocity.X += Helper.EntroptPool[projectile.whoAmI + rd.Next(1, 500)] * 0.01f;
                projectile.velocity.Y += Helper.EntroptPool[projectile.whoAmI + rd.Next(501, 1000)] * 0.01f;
                if (projectile.ai[0] == 0)
                {
                    projectile.ai[0] += 12;
                    Projectile.NewProjectile(projectile.Center, speed, ProjectileID.Bullet, projectile.damage * 2, projectile.knockBack, projectile.owner);
                }
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            crit = true;
        }
    }
}
