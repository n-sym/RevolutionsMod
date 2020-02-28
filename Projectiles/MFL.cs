using Microsoft.Xna.Framework;
using Revolutions.Items.Weapon;
using Revolutions.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolutions.Projectiles
{
    public class MFL : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("MegaForLocation");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            projectile.width = 66;
            projectile.height = 28;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.timeLeft = 7;
            projectile.tileCollide = true;
            projectile.alpha = 0;
            projectile.ignoreWater = true;
            projectile.aiStyle = -1;
            projectile.penetrate = 2000;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
        }
        public override bool ShouldUpdatePosition()
        {
            return false;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = 0;
        }
        public override void AI()
        {
            base.AI();
            NPC target = null;
            float distance = 0;
            projectile.Center = Main.player[projectile.owner].Center + new Vector2(-70f * projectile.ai[0], 0f);
            foreach (NPC npc in Main.npc)
            {
                if (Vector2.Distance(npc.Center, projectile.Center) > distance
                    && npc.type != NPCID.TargetDummy && npc.active
                    && Vector2.Distance(npc.Center, projectile.Center) < 1500f
                    && !npc.dontTakeDamage)
                {
                    distance = Vector2.Distance(npc.Center, projectile.Center);
                    target = npc;
                }
            }
            int shoot2 = 0, shoot3 = 0;
            if (Mega.GetMegaShootID.save1 == 0) Mega.GetMegaShootID.save1 = 14;
            if (Mega.GetMegaShootID.save2 == 0) shoot2 = Mega.GetMegaShootID.save1;
            else shoot2 = Mega.GetMegaShootID.save2;
            if (Mega.GetMegaShootID.save3 == 0) shoot3 = shoot2;
            else shoot3 = Mega.GetMegaShootID.save3;
            if (Mega.GetMegaShootID.save1 == ProjectileID.Bullet)
            {
                shoot2 = ProjectileID.Bullet;
                shoot3 = shoot2;
            }
            int shoot;
            if (projectile.ai[0] == 1) shoot = shoot2;
            else shoot = shoot3;
            if (target == null && projectile.timeLeft == 7)
            {
                Projectile.NewProjectile(projectile.Center, projectile.velocity, shoot, projectile.damage, projectile.knockBack, projectile.owner);
                projectile.rotation = projectile.velocity.ToRotation();
                if (projectile.rotation > 1.571f)
                {
                    projectile.rotation -= 3.14f;
                    projectile.spriteDirection = -1;
                }
                if (projectile.rotation < -1.571f)
                {
                    projectile.rotation -= 3.14f;
                    projectile.spriteDirection = -1;
                }
            }
            else if (projectile.timeLeft == 7)
            {
                Vector2 speed = Helper.ToUnitVector(target.Center - projectile.Center);
                projectile.rotation = (target.Center - projectile.Center).ToRotation();
                speed *= projectile.velocity.Length();
                Projectile.NewProjectile(projectile.Center, speed, shoot, projectile.damage, projectile.knockBack, projectile.owner);

                if (projectile.rotation > 1.571f)
                {
                    projectile.rotation -= 3.14f;
                    projectile.spriteDirection = -1;
                }
                if (projectile.rotation < -1.571f)
                {
                    projectile.rotation -= 3.14f;
                    projectile.spriteDirection = -1;
                }
            }

        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            /*Random rd = new Random();
            int shoot2 = 0, shoot3 = 0;
            if (Mega.GetMegaShootID.save1 == 0)
            {
                Mega.GetMegaShootID.save1 = 14;
            }
            if (Mega.GetMegaShootID.save2 == 0)
            {
                shoot2 = Mega.GetMegaShootID.save1;
            }
            else
            {
                shoot2 = Mega.GetMegaShootID.save2;
            }
            if (Mega.GetMegaShootID.save3 == 0)
            {
                shoot3 = shoot2;
            }
            else
            {
                shoot3 = Mega.GetMegaShootID.save3;
            }
            if (Mega.GetMegaShootID.save1 == ProjectileID.Bullet)
            {
                shoot2 = ProjectileID.Bullet;
                shoot3 = shoot2;
            }
            Projectile.NewProjectile(target.Center.X, target.Center.Y + 300, 0, -90, shoot2, projectile.damage * 8, 0, projectile.owner);
            Projectile.NewProjectile(target.Center.X, target.Center.Y - 300, 0, 90, shoot2, projectile.damage * 8, 0, projectile.owner);
            Projectile.NewProjectile(target.Center.X + 300, target.Center.Y, -90, 0, shoot3, projectile.damage * 8, 0, projectile.owner);
            Projectile.NewProjectile(target.Center.X - 300, target.Center.Y, 90, 0, shoot3, projectile.damage * 8, 0, projectile.owner);
            foreach (Projectile projectiles in Main.projectile)
            {
                if (projectiles.damage == projectile.damage * 8)
                {
                    projectiles.tileCollide = false;
                }
            }*/
        }
    }
}
