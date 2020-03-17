using Microsoft.Xna.Framework;
using Revolutions.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolutions.Items.Weapon.Core
{
    public class ThunderBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            item.damage = 145;
            item.melee = true;
            item.width = 40;
            item.crit = 14;
            item.height = 20;
            item.useTime = 9;
            item.useAnimation = 8;
            item.useStyle = 1;
            item.noMelee = false;
            item.knockBack = 10;
            item.value = Item.sellPrice(0, 36, 0, 0); ;
            item.rare = 11;
            item.UseSound = SoundID.Item60;
            item.autoReuse = true;
        }
        bool l = true;
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            l = !l;
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.CoreWeapon.Thunder>()] < 51 && l)
            {
                NPC t = null;
                float distance = new Vector2(Main.screenWidth, Main.screenHeight).Length() * Main.UIScale;
                if (target.life < damage || target.type == NPCID.TargetDummy || target.SpawnedFromStatue)
                {
                    foreach (NPC npc in Main.npc)
                    {
                        if (npc.active && npc.CanBeChasedBy() && Vector2.Distance(npc.position, player.position) < distance)
                        {
                            t = npc;
                            distance = Vector2.Distance(npc.position, player.position);
                            if (npc.boss) break;
                        }
                    }
                }
                if (t == null)
                {
                    Projectile.NewProjectile(target.Center + 15 * target.velocity + new Vector2((600 + Helper.EntroptPool[damage]), -800 + Helper.EntroptPool[damage + 100]), Vector2.Zero, ModContent.ProjectileType<Projectiles.CoreWeapon.Thunder>(), damage, 0, player.whoAmI, target.Center.X + 15 * target.velocity.X, target.Center.Y + 15 * target.velocity.Y);
                }
                else
                {
                    Projectile.NewProjectile(t.Center + 15 * t.velocity + new Vector2((600 + Helper.EntroptPool[damage]), -800 + Helper.EntroptPool[damage + 100]), Vector2.Zero, ModContent.ProjectileType<Projectiles.CoreWeapon.Thunder>(), damage, 0, player.whoAmI, t.Center.X + 15 * t.velocity.X, t.Center.Y + 15 * t.velocity.Y);
                }
            }
        }

    }
}
