using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Revolutions.Projectiles
{
    public class JustDamage : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Saviour Bonus");
        }
        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;
            projectile.extraUpdates = 1;
            projectile.friendly = true;
            projectile.timeLeft = 3;
            projectile.tileCollide = false;
            projectile.alpha = 255;
            projectile.ignoreWater = true;
            projectile.aiStyle = -1;
            projectile.scale = 1f;
        }
        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            if (player.GetModPlayer<RevolutionsPlayer>().justDmgcounter > 20)
            {
                projectile.Kill();
            }

            return true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            int a = 0;
            foreach (CombatText combatText in Main.combatText)
            {
                int.TryParse(combatText.text, out a);
                if (a == damage && !combatText.crit) combatText.color = Color.White;
            }
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player player = Main.player[projectile.owner];
            if (player.GetModPlayer<RevolutionsPlayer>().justDmgcounter > 20)
            {
                damage = 0;
                projectile.Kill();
            }
            else
            {
                player.GetModPlayer<RevolutionsPlayer>().justDmgcounter++;
            }
            if (damage - (int)((target.lifeMax - target.life) * 0.0025f) >= 500) damage = (int)((target.lifeMax - target.life) * 0.0025f);
        }
    }
}
