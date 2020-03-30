using Microsoft.Xna.Framework;
using Revolutions.Items.BluePrints;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolutions.Items.Weapon.Rare
{
    public class Oath : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {
            item.damage = 50;
            item.magic = true;
            item.width = 40;
            item.crit = 8;
            item.height = 20;
            item.useTime = 10;
            item.mana = 5;
            item.useAnimation = 10;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 4;
            item.value = Item.sellPrice(0, 42, 0, 0); ;
            item.rare = 9;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shootSpeed = 28f;
            item.shoot = ModContent.ProjectileType<Projectiles.RareWeapon.OathProj>();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 speed = new Vector2(speedX, speedY).RotatedByRandom(0.05);
            Projectile.NewProjectile(position, speed, type, damage, knockBack, player.whoAmI, position.X, position.Y);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CrystalShard, 15);
            recipe.AddIngredient(ItemID.SoulofLight, 15);
            recipe.AddIngredient(ModContent.ItemType<MtwerBP>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
