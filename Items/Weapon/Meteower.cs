using Microsoft.Xna.Framework;
using Revolutions.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolutions.Items.Weapon
{
    public class Meteower : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Casting 15 meteors every attack\nMeteors can penetrate enemys 16 defenses");
        }
        public override void SetDefaults()
        {
            item.damage = 16;
            item.magic = true;
            item.width = 40;
            item.crit = 6;
            item.height = 20;
            item.useTime = 10;
            item.mana = 12;
            item.useAnimation = 10;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = Item.sellPrice(0, 36, 0, 0); ;
            item.rare = 11;
            item.UseSound = SoundID.Item9;
            item.autoReuse = true;
            item.shootSpeed = 12f;
            item.shoot = mod.ProjectileType("MeteowerHelper");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            /*for (int i = 15; i > 0; i--)
            {
                Vector2 Speed2 = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(90));
                Projectile.NewProjectile(position.X, position.Y, Speed2.X, Speed2.Y, type, damage, knockBack, player.whoAmI,speedX,speedY);
            }*/

            //Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("MeteowerHelper"), damage, knockBack, player.whoAmI, speedX, speedY);
            //Lighting.AddLight(player.Center, 126, 171, 243);
            return true;
        }
        public override void UpdateInventory(Player player)
        {
            if (BlueprintState.Meteower == 1)
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.CrystalShard, 15);
                recipe.AddIngredient(ItemID.SoulofLight, 15);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(this);
                recipe.AddRecipe();
                BlueprintState.Meteower = -1;
            }
        }
        public override void OnCraft(Recipe recipe)
        {
            base.OnCraft(recipe);
        }
    }
}
