using Microsoft.Xna.Framework;
using Revolutions.Items.BluePrints;
using Revolutions.Items.Weapon.Rare;
using Revolutions.Utils;
using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolutions
{
    public class BluePrintManager
    {
        static ModRecipe meteowerRecipe;
        static ModRecipe leaveswandRecipe;
        public BluePrintManager(Mod mod)
        {
            RemoveAll();
            MakeMeteowerRecipe(mod, ref meteowerRecipe);
            MakeLeavesWandRecipe(mod, ref leaveswandRecipe);
        }
        public void Update()
        {
            bool[] blueprints = Main.LocalPlayer.GetModPlayer<RevolutionsPlayer>().bluePrint;
            if (blueprints[BluePrintID.Meteower] && meteowerRecipe.RecipeIndex == 0) meteowerRecipe.AddRecipe();
            if (blueprints[BluePrintID.LeavesWand] && leaveswandRecipe.RecipeIndex == 0) leaveswandRecipe.AddRecipe();
        }
        public static void RemoveAll()
        {
            RemoveRecipe(meteowerRecipe);
            RemoveRecipe(leaveswandRecipe);
        }
        public static void MakeMeteowerRecipe(Mod mod, ref ModRecipe recipe)
        {
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Book);
            recipe.AddIngredient(ItemID.CrystalShard, 15);
            recipe.AddIngredient(ItemID.SoulofLight, 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(ModContent.ItemType<Meteower>());
        }
        public static void MakeLeavesWandRecipe(Mod mod, ref ModRecipe recipe)
        {
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Wood, 15);
            recipe.AddIngredient(ItemID.SoulofLight, 10);
            recipe.AddIngredient(ItemID.SoulofNight, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(ModContent.ItemType<LeavesWand>());
        }
        
        public static void RemoveRecipe(ModRecipe recipe)
        {
            if (recipe != null && recipe.RecipeIndex != 0) Main.recipe[recipe.RecipeIndex] = new Recipe();
        }
    }
    public class BluePrintID
    {
        public const int Meteower = 0;
        public const int LeavesWand = 1;
        public const int Oath = 2;
    }
}

