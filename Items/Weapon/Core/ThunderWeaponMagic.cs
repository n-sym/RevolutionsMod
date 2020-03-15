using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Revolutions.Items.Weapon.Core
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
            item.useTime = 15;
            item.mana = 12;
            item.useAnimation = 15;
            item.useStyle = 4;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = Item.sellPrice(0, 36, 0, 0); ;
            item.rare = 11;
            item.autoReuse = true;
            item.shootSpeed = 12f;
            item.shoot = ModContent.ProjectileType<Projectiles.CoreWeapon.ThunderMagic>();
        }
        public override bool CanUseItem(Player player)
        {
            foreach (NPC t in Main.npc)
            {
                if (!t.dontTakeDamage && (!t.friendly || t.type == Terraria.ID.NPCID.TargetDummy) && t.active && Vector2.Distance(t.position, player.position) < 700f) return true;
            }
            return false;
        }
        public override void UpdateInventory(Player player)
        {
            if (player.HeldItem == item) RevolutionsPlayer.drawcircler = 700f;
            if (soundcd > 0) soundcd--;
        }
        int soundcd = 0;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, position.X, position.Y);
            if (soundcd == 0)
            {
                Main.PlaySound(SoundLoader.customSoundType, player.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/electric" + Main.rand.Next(1, 3).ToString()));
                soundcd += 20;
            }
            return false;
        }
    }
}
