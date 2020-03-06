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
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CrystalShard, 15);
            recipe.AddIngredient(ItemID.SoulofLight, 15);
            recipe.AddIngredient(ModContent.ItemType<BluePrints.MtwerBP>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override void UpdateInventory(Player player)
        {
            if (player.HeldItem == item && Revolutions.Settings.rangeIndex == 2)
            {
                RevolutionsPlayer.drawcircler = 370f;
                RevolutionsPlayer.drawcircletype = 1;
            }
        }
    }
}
