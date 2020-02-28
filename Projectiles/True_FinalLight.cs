using Microsoft.Xna.Framework;
using Revolutions.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Revolutions.Projectiles
{
    public class True_FinalLight : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Final Light");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            //projectile.damage = 256;
            projectile.width = 10;
            projectile.height = 10;
            projectile.extraUpdates = 1;
            projectile.hostile = true;
            projectile.timeLeft = 350;
            projectile.tileCollide = false;
            projectile.alpha = 0;
            projectile.ignoreWater = true;
            projectile.aiStyle = 1;
            projectile.light = 0.5f;
            aiType = ProjectileID.Bullet;
        }
        public override void AI()
        {
            Player player = Main.player[0];
            // 最大寻敌距离为1000像素
            float distanceMax = 4000f;
            // 计算与玩家的距离
            float currentDistance = Vector2.Distance(projectile.Center, player.Center);
            // 如果npc距离比当前最大距离小
            if (currentDistance < distanceMax)
            {
                Vector2 targetVec = player.Center - projectile.Center;
                targetVec.Normalize();
                // 目标向量是朝向目标的大小为20的向量
                targetVec *= 20f;
                // 朝向npc的单位向量*20 + 3.33%偏移量
                projectile.velocity = (projectile.velocity * 26.5f + targetVec) / 31.5f;
            }
            else
            {
                projectile.timeLeft = 10;
            }
        }

        public override void Kill(int timeLeft)
        {
            base.Kill(timeLeft);
            for (int i = 0; i < 30; i++)
            {
                // 生成dust
                Dust d = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height,
                MyDustId.CyanCrystal, 0, 0, 100, Color.White, 0.8f);
                // 粒子效果无重力
                d.noGravity = true;
                // 粒子效果初速度乘以二
                d.velocity *= 2;
            }
        }
    }
}
