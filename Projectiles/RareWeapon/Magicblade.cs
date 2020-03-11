using Microsoft.Xna.Framework;
using Revolutions.Utils;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;


namespace Revolutions.Projectiles.RareWeapon
{
    public class Magicblade : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magic blade");
            DisplayName.AddTranslation(GameCulture.Chinese, "魔法叶片");
            Main.projFrames[projectile.type] = 5;
        }
        public override void SetDefaults()
        {
            projectile.width = 28;
            projectile.height = 28;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.ignoreWater = false;
            projectile.timeLeft = 600;
            projectile.tileCollide = true;
            projectile.penetrate = 3;
            projectile.scale = 0.7f;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 10;
        }
        public override void AI()
        {
            projectile.alpha = 0;

            projectile.frameCounter++;
            if (projectile.frameCounter == 3)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame == 5)
                {
                    projectile.frame = 0;
                }

            }
            if (projectile.timeLeft < 530)
            {
                projectile.velocity.Y += 0.3f;
            }
            if (projectile.timeLeft < 597)
            {
                projectile.stepSpeed -= 0.79f;
                Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, MyDustId.PurpleHighFx, 0f, 0f, 100, default(Color), 1f);
                //Dust dust1 = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, MyDustId.ThinPinkMaterial, 0f, 0f, 200, default(Color), 0.8f);
                dust.noGravity = true;
            }
            if (projectile.timeLeft < 300)

                projectile.alpha += 10;
            Player player = Main.player[projectile.owner];
            NPC target = null;
            float distanceMax = 500f;
            foreach (NPC npc in Main.npc)
            {
                if (npc.active && !npc.friendly && npc.CanBeChasedBy())
                {
                    float currentDistance = Vector2.Distance(npc.Center, player.Center);
                    if (currentDistance < distanceMax)
                    {
                        distanceMax = currentDistance;
                        target = npc;
                    }
                }
            }
            if (target != null && projectile.timeLeft < 579)
            {
                Vector2 targetVec = target.Center - projectile.Center;
                //选出目标向量
                targetVec.Normalize();
                //转化为单位向量
                targetVec *= 30f;
                //长度变为30
                projectile.velocity = (projectile.velocity * 30f + targetVec) / 31f;
                //在原有速度基础上偏移
            }
            projectile.rotation = projectile.velocity.ToRotation();
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.damage /= 2;
        }
        public override void Kill(int timeLeft)
        {
            Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, MyDustId.PurpleHighFx, 0f, 0f, 100, Color.Purple, 3f);
            Dust dust1 = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, MyDustId.TrailingRed2, 0f, 0f, 200, Color.Green, 1f);
            dust.noGravity = true;
        }
    }
}
