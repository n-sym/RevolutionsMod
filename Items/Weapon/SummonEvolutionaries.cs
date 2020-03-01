using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
namespace Revolutions.Items.Weapon
{
    public class SummonEvolutionaries : ModItem
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            item.damage = 145;
            item.summon = true;
            item.width = 40;
            item.crit = 14;
            item.height = 20;
            item.mana = 15;
            item.useTime = 17;
            item.useAnimation = 17;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.noMelee = true;
            item.autoReuse = true;
            item.knockBack = 7;
            item.value = Item.sellPrice(0, 36, 0, 0); ;
            item.rare = 11;
            item.UseSound = SoundID.Item44;
            item.buffType = ModContent.BuffType<Buffs.BuffOfEvos>();
            item.shoot = ModContent.ProjectileType<Projectiles.Evolutionaries>();
            item.shootSpeed = 10f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            speedX = 0;
            speedY = 0;
            position = Main.MouseWorld;
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, 0, player.whoAmI, player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Evolutionaries>()]);
            player.AddBuff(item.buffType, 2);
            return false;
        }
    }
}
