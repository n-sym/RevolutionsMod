using Microsoft.Xna.Framework;
using Revolutions.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolutions.Items.Weapon
{
    public class ThunderWeaponMagic : ModItem
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            item.damage = 145;
            item.magic = true;
            item.width = 40;
            item.crit = 6;
            item.height = 20;
            item.useTime = 20;
            item.mana = 16;
            item.useAnimation = 20;
            item.useStyle = 4;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = Item.sellPrice(0, 36, 0, 0); ;
            item.rare = 11;
            item.UseSound = SoundID.Item9;
            item.autoReuse = true;
            item.shootSpeed = 12f;
            item.shoot = ModContent.ProjectileType<Projectiles.ThunderMagic>();
        }
        public override bool CanUseItem(Player player)
        {
            foreach(NPC t in Main.npc)
            {
                if (t.active && Vector2.Distance(t.position, player.position) < 800f) return true;
            }
            return false;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, position.X, position.Y);
            return false;
        }
    }
}
