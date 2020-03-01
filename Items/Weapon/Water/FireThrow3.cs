using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
namespace Revolutions.Items.Weapon.Water
{
    public class FireThrow3 : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault(Language.GetTextValue("ItemTooltip.Flamethrower"));
        }
        public override void SetDefaults()
        {
            item.useStyle = 5;
            item.autoReuse = true;
            item.useAnimation = 30;
            item.useTime = 5;
            item.width = 50;
            item.height = 18;
            item.shoot = ModContent.ProjectileType<Projectiles.ForWater.Fire1>();
            item.useAmmo = AmmoID.Gel;
            item.UseSound = SoundID.Item34;
            item.damage = 33;
            item.knockBack = 0.6f;
            item.shootSpeed = 10f;
            item.noMelee = true;
            item.value = 650000;
            item.rare = 6;
            item.ranged = true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.wet) return false;
            else return true;
        }
    }
}
