using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Revolutions.Utils;
using Revolutions.Items;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Revolutions
{
    public class RevolutionsWorld : ModWorld
    {
        int AlphaCD = 0;
        public override void PreUpdate()
        {
            AlphaCD--;
            if (Main.time < 5 && Main.rand.Next(3) == 0 && AlphaCD == 0)
            {
                AlphaCD = 3600;
                Point target = FindHome().Value;
                for(int i = -1; i < 3; i++)
                {
                    for (int j = -1; j < 3; j++)
                    {
                        WorldGen.KillTile(target.X + i, target.Y + j, false, false, true);
                        if((i == -1 || i == 2) || (j == -1 || j == 2))
                        {
                            WorldGen.PlaceTile(target.X + i, target.Y + j, TileID.DiamondGemspark);
                        }
                    }
                }
                SpawnChest(new Point(target.X, target.Y + 1), BasicChest(), "奖励箱");
                Helper.PrintColor("新的奖励箱已生成！", 255, 255, 0);
            }
        }

        public static void SpawnChest(Point pos, Point[] items, string name = "", int type = 21)
        {
            int x = pos.X;
            int y = pos.Y;
            WorldGen.PlaceChest(x, y, (ushort)type);
            int index = Chest.FindEmptyChest(x, y);
            Chest chest = Main.chest[index - 1];
            chest.name = name;
            for(int i = 0; i< items.Length; i++)
            {
                chest.item[i].SetDefaults(items[i].X, false);
                chest.item[i].stack = items[i].Y;
            }
        }
        public static Point[] BasicChest()
        {
            List<Point> items = new List<Point>();
            if (Main.hardMode)
            {
                items.Add(new Point(ItemID.GoldCoin, Main.rand.Next(20, 100)));
                items.Add(Main.rand.NextBool() ?
                    new Point(Main.rand.NextBool() ? ItemID.CobaltBar : ItemID.PalladiumBar, Main.rand.Next(4, 16)) :
                    new Point(Main.rand.NextBool() ? ItemID.AmmoReservationPotion : ItemID.BattlePotion, Main.rand.Next(4, 8)));
                items.Add(Main.rand.NextBool() ?
                    new Point(Main.rand.NextBool() ? ItemID.MythrilBar : ItemID.OrichalcumBar, Main.rand.Next(4, 16)) :
                    new Point(Main.rand.NextBool() ? ItemID.LifeforcePotion : ItemID.RagePotion, Main.rand.Next(4, 8)));
                items.Add(Main.rand.NextBool() ?
                    new Point(Main.rand.NextBool() ? ItemID.AdamantiteBar : ItemID.TitaniumBar, Main.rand.Next(4, 16)) :
                    new Point(Main.rand.NextBool() ? ItemID.InfernoPotion : ItemID.WrathPotion, Main.rand.Next(4, 8)));
                if ((NPC.downedMechBoss1 || NPC.downedMechBoss2 || NPC.downedMechBoss3) && Main.rand.Next(100) < 33)
                {
                    items.Add(Main.rand.Next(33) < 5 ?
                        new Point(ModContent.ItemType<Items.BluePrints.Treestickblueprint>(), 1) :
                        new Point(ItemID.CrystalShard, Main.rand.Next(5, 14)));
                }
                if (NPC.downedMechBossAny && Main.rand.Next(100) < 33)
                {
                    items.Add(new Point(ItemID.HallowedBar, Main.rand.Next(5, 14)));
                    items.Add(new Point(ItemID.ChlorophyteBar, Main.rand.Next(5, 14)));
                    items.Add(Main.rand.Next(33) < 5 ?
                        new Point(ModContent.ItemType<Items.BluePrints.MtwerBP>(), 1) :
                        new Point(ItemID.CrystalStorm, 1));
                }
                if (NPC.downedPlantBoss && Main.rand.Next(100) < 33)
                {
                    items.Add(new Point(ItemID.ShroomiteBar, Main.rand.Next(5, 14)));
                    items.Add(new Point(ItemID.SpectreBar, Main.rand.Next(5, 14)));
                }
                if(NPC.downedGolemBoss && Main.rand.Next(100) < 33)
                {
                    items.Add(new Point(ItemID.BeetleHusk, Main.rand.Next(2, 5)));
                }
                if(NPC.downedMoonlord && Main.rand.Next(100) < 33)
                {
                    items.Add(new Point(ItemID.LunarBar, Main.rand.Next(5, 14)));
                }
            }
            else
            {
                items.Add(Main.rand.NextBool() ? 
                    new Point(ItemID.GoldCoin, Main.rand.Next(1, 4)):
                    new Point(ItemID.SilverCoin, Main.rand.Next(30, 100)));
                items.Add(Main.rand.NextBool() ? 
                    new Point(Main.rand.NextBool() ? ItemID.IronBar : ItemID.LeadBar, Main.rand.Next(4, 16)):
                    new Point(Main.rand.NextBool() ? ItemID.RecallPotion : ItemID.LesserHealingPotion, Main.rand.Next(4, 8)));
                items.Add(Main.rand.NextBool() ? 
                    new Point(Main.rand.NextBool() ? ItemID.NightOwlPotion : ItemID.IronskinPotion, Main.rand.Next(3, 7)):
                    new Point(Main.rand.NextBool() ? ItemID.FishingPotion : ItemID.ShinePotion, Main.rand.Next(3, 7)));
                items.Add(Main.rand.NextBool() ?
                    new Point(Main.rand.NextBool() ? ItemID.Sunflower : ItemID.Daybloom, Main.rand.Next(1, 3)):
                    new Point(Main.rand.NextBool() ? ItemID.Moonglow : ItemID.Waterleaf, Main.rand.Next(1, 3)));
                if(NPC.downedBoss1)
                {
                    items.Add(new Point(ItemID.MeteoriteBar, Main.rand.Next(6, 18)));
                    items.Add(Main.rand.NextBool() ?
                        new Point(ItemID.DemoniteBar, Main.rand.Next(5, 14)):
                        new Point(ItemID.CrimtaneBar, Main.rand.Next(5, 14)));
                }
                if(NPC.downedBoss3)
                {
                    items.Add(Main.rand.Next(100) < 10 ?
                    new Point(ModContent.ItemType<Items.Weapon.Water.FireThrow>(), 1):
                    new Point(ItemID.FlareGun, 1));
                    items.Add(Main.rand.NextBool() ?
                    new Point(Main.rand.NextBool() ? ItemID.Damselfish : ItemID.Stinkfish, Main.rand.Next(1, 3)):
                    new Point(Main.rand.NextBool() ? ItemID.Bass : ItemID.SpecularFish, Main.rand.Next(1, 3)));
                }
            }
            return items.ToArray();
        }
        public static Point? FindCorrupt()
        {
            for (int x = 0; x < Main.maxTilesX; x += 5)
            {
                if (x > Main.maxTilesX) break;
                for (int y = Main.maxTilesY / 3; y < Main.maxTilesY; y += 5)
                {
                    if (y > Main.maxTilesY) break;
                    if (!Main.tile[x, y].active()) continue;
                    Tile tile = Main.tile[x, y];
                    if (tile.type == TileID.Ebonstone)
                    {
                        int score = 0;
                        for (int i = -3; i < 4; i++)
                        {
                            for (int j = -3; j < 4; j++)
                            {
                                if (Main.tile[i + x + Main.rand.Next(-6, 7), j + y + Main.rand.Next(-6, 7)].type == TileID.Ebonstone) score++;
                            }
                            if (score == 49) return new Point(x, y);
                        }
                    }
                }
            }
            return null;
        }
        public static Point? FindCrimson()
        {
            for (int x = 0; x < Main.maxTilesX; x += 5)
            {
                if (x > Main.maxTilesX) break;
                for (int y = Main.maxTilesY / 3; y < Main.maxTilesY; y += 5)
                {
                    if (y > Main.maxTilesY) break;
                    if (!Main.tile[x, y].active()) continue;
                    Tile tile = Main.tile[x, y];
                    if (tile.type == TileID.Crimstone)
                    {
                        int score = 0;
                        for (int i = -3; i < 4; i++)
                        {
                            for (int j = -3; j < 4; j++)
                            {
                                if (Main.tile[i + x + Main.rand.Next(-6, 7), j + y + Main.rand.Next(-6, 7)].type == TileID.Crimstone) score++;
                            }
                            if (score == 49) return new Point(x, y);
                        }
                    }
                }
            }
            return null;
        }
        public static Point? FindJungle()
        {
            for (int x = 0; x < Main.maxTilesX; x += 5)
            {
                if (x > Main.maxTilesX) break;
                for (int y = Main.maxTilesY / 3; y < Main.maxTilesY; y += 5)
                {
                    if (y > Main.maxTilesY) break;
                    if (!Main.tile[x, y].active()) continue;
                    Tile tile = Main.tile[x, y];
                    if (tile.type == TileID.JungleGrass)
                    {
                        int score = 0;
                        for (int i = -3; i < 4; i++)
                        {
                            for (int j = -3; j < 4; j++)
                            {
                                if (Main.tile[i + x + Main.rand.Next(-6, 7), j + y + Main.rand.Next(-6, 7)].type == TileID.Mud || Main.tile[i + x + Main.rand.Next(-6, 7), j + y + Main.rand.Next(-6, 7)].type == TileID.JungleGrass) score++;
                            }
                            if (score > 30) return new Point(x, y);
                        }
                    }
                }
            }
            return null;
        }
        public static Point? FindHallow()
        {
            for (int x = 0; x < Main.maxTilesX; x += 5)
            {
                if (x > Main.maxTilesX) break;
                for (int y = Main.maxTilesY / 3; y < Main.maxTilesY; y += 5)
                {
                    if (y > Main.maxTilesY) break;
                    if (!Main.tile[x, y].active()) continue;
                    Tile tile = Main.tile[x, y];
                    if (tile.type == TileID.Pearlstone)
                    {
                        int score = 0;
                        for (int i = -3; i < 4; i++)
                        {
                            for (int j = -3; j < 4; j++)
                            {
                                if (Main.tile[i + x + Main.rand.Next(-6, 7), j + y + Main.rand.Next(-6, 7)].type == TileID.Pearlstone) score++;
                            }
                            if (score == 49) return new Point(x, y);
                        }
                    }
                }
            }
            return null;
        }
        public static Point? FindFloatingIsland()
        {
            for (int x = 0; x < Main.maxTilesX; x += Main.rand.Next(50, 500))
            {
                if (x > Main.maxTilesX) break;
                for (int y = 0; y < Main.maxTilesY; y += 5)
                {
                    if (y > Main.maxTilesY) break;
                    if (!Main.tile[x, y].active()) continue;
                    Tile tile = Main.tile[x, y];
                    if (tile.type == TileID.Cloud)
                    {
                        int score = 0;
                        {
                        for (int i = -3; i < 4; i++)
                            for (int j = -3; j < 4; j++)
                            {
                                    Point rdpos = new Point(i + x + Main.rand.Next(-6, 7), j + y + Main.rand.Next(-6, 7));
                                if (Main.tile[i + x + Main.rand.Next(-6, 7), j + y + Main.rand.Next(-6, 7)].type == TileID.Cloud && Main.tile[rdpos.X, rdpos.Y].active() && Main.tile[rdpos.X, rdpos.Y].type == TileID.Dirt) score++;
                            }
                            if (score > 0) return new Point(x, y);
                        }
                    }
                }
            }
            return null;
        }
        public static Point? FindHome()
        {
            return new Point(Main.maxTilesX / 2 + Main.rand.Next(-200, 200), Main.spawnTileY + Main.rand.Next(50, 500));
        }
    }
}
