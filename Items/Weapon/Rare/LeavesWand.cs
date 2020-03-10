using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Revolutions.Items.BluePrints;

namespace Revolutions.Items.Weapon.Rare
{
    public class LeavesWand : ModItem 
    {

        public override void SetStaticDefaults() 
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Leaves wand");
            Tooltip.SetDefault("Summon Tracking Magic Blade to attack enemies...");
        }
        public override void SetDefaults() 
        {
            item.damage = 53;
            item.knockBack = 0.25f;
            item.crit = 10;
            item.rare = 8;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 5;
            item.autoReuse = true;
            item.magic  = true;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.UseSound = SoundID.Item9;
            item.width = 40;
            item.height = 40;
            item.maxStack = 1;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("Magicblade");
            item.shootSpeed = 8f;
            item.mana = 10;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 4 + Main.rand.Next(2);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30)); 
                float scale = 1f - (Main.rand.NextFloat() * .3f);
                perturbedSpeed = perturbedSpeed * scale; 
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 0);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Wood, 15);
            recipe.AddIngredient(ItemID.SoulofLight, 3);
            recipe.AddIngredient(ItemID.SoulofNight, 3);
            recipe.AddIngredient(ModContent.ItemType<Treestickblueprint>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
