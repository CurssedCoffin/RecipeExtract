using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using RecipeExtract.Common.Models;
using RecipeExtract.Common.Utils;

namespace RecipeExtract.Common.Services
{
    public class DataExportService
    {
        private readonly Mod _mod;

        public DataExportService(Mod mod)
        {
            _mod = mod;
        }

        /// <summary>
        /// Exports all game data to JSON files
        /// </summary>
        public void ExportData()
        {
            ExportRecipeData();
            ExportShimmerData();
            ExportShopData();
            ExportDropData();
        }

        /// <summary>
        /// Saves a list of objects to a JSON file
        /// </summary>
        private void SaveToFile<T>(string fileName, List<T> content)
        {
            try
            {
                string json = JsonConvert.SerializeObject(content, Formatting.Indented);
                string savePathDir = Path.Combine(Main.SavePath, "Mods", _mod.Name);
                Directory.CreateDirectory(savePathDir);
                string filePath = Path.Combine(savePathDir, fileName);
                File.WriteAllText(filePath, json, Encoding.UTF8);
                _mod.Logger.Info($"Data file saved to: {filePath}");
                Main.NewText($"Data file saved to: {filePath}", Color.Green);
            }
            catch (Exception ex)
            {
                _mod.Logger.Error("Error exporting data: " + ex.ToString());
                Main.NewText("Error exporting data: " + ex.ToString(), Color.Red);
            }
        }

        /// <summary>
        /// Exports all recipe data to a JSON file
        /// </summary>
        private void ExportRecipeData()
        {
            List<MyRecipe> allRecipes = [];

            foreach (Recipe recipe in Main.recipe)
            {
                if (recipe == null || recipe.createItem == null || recipe.createItem.type == ItemID.None)
                {
                    continue;
                }

                MyRecipe currentRecipe = new()
                {
                    item = GameDataUtils.GetMyItemByItem(recipe.createItem),
                    ingredients = [],
                    tiles = [],
                    conditions = []
                };

                foreach (Item ingredient in recipe.requiredItem)
                {
                    if (ingredient == null || ingredient.type == ItemID.None)
                    {
                        continue;
                    }
                    currentRecipe.ingredients.Add(GameDataUtils.GetMyItemByItem(ingredient));
                }

                foreach (int tileId in recipe.requiredTile)
                {
                    if (tileId == -1) continue;
                    currentRecipe.tiles.Add(GameDataUtils.GetMyTileByTileId(tileId));
                }

                foreach (Condition condition in recipe.Conditions)
                {
                    if (condition != null && !string.IsNullOrEmpty(condition.Description?.Value))
                    {
                        currentRecipe.conditions.Add(GameDataUtils.GetMyConditionByCondition(condition));
                    }
                }

                allRecipes.Add(currentRecipe);
            }

            SaveToFile("Recipes.json", allRecipes);
        }

        /// <summary>
        /// Exports all shimmer transformation data to a JSON file
        /// </summary>
        private void ExportShimmerData()
        {
            List<MyShimmer> allShimmers = [];

            for (int i = 1; i < ItemLoader.ItemCount; i++)
            {
                Item item = new(i);
                if (item == null || item.type == ItemID.None)
                {
                    continue;
                }

                MyShimmer currentShimmer = new()
                {
                    item = GameDataUtils.GetMyItemByItem(item),
                    shimmerResult = []
                };

                List<Item> shimmerResult = GameDataUtils.GetShimmerResult(item, out int _, out int _);
                if (shimmerResult == null)
                {
                    continue;
                }

                foreach (Item shimmerItem in shimmerResult)
                {
                    currentShimmer.shimmerResult.Add(GameDataUtils.GetMyItemByItem(shimmerItem));
                }

                allShimmers.Add(currentShimmer);
            }

            SaveToFile("Shimmers.json", allShimmers);
        }

        /// <summary>
        /// Exports all shop data to a JSON file
        /// </summary>
        private void ExportShopData()
        {
            List<MyShop> allShops = [];

            foreach (AbstractNPCShop shop in NPCShopDatabase.AllShops)
            {
                NPC npc = new();
                npc.SetDefaults(shop.NpcType);

                MyShop currentShop = new()
                {
                    npc = new MyNPC
                    {
                        npcName = Lang.GetNPCNameValue(shop.NpcType),
                        npcId = shop.NpcType,
                    },
                    shopName = shop.Name,
                    ModName = npc.ModNPC?.Mod.Name ?? "Terraria",
                    items = []
                };

                foreach (AbstractNPCShop.Entry entry in shop.ActiveEntries)
                {
                    Item item = new(entry.Item.type);
                    MyShopItem currentItem = new()
                    {
                        item = GameDataUtils.GetMyItemByItem(item),
                        price = entry.Item.value,
                        conditions = []
                    };

                    foreach (Condition condition in entry.Conditions)
                    {
                        currentItem.conditions.Add(GameDataUtils.GetMyConditionByCondition(condition));
                    }

                    currentShop.items.Add(currentItem);
                }

                allShops.Add(currentShop);
            }

            SaveToFile("Shops.json", allShops);
        }

        /// <summary>
        /// Exports all NPC drop data to a JSON file
        /// </summary>
        private void ExportDropData()
        {
            List<MyDrop> allDrops = [];

            for (int i = -65; i < NPCLoader.NPCCount; i++)
            {
                NPC npc = new();
                npc.SetDefaults(i);
                if (npc.type == NPCID.None)
                {
                    continue;
                }

                string npcName = npc.FullName;
                if (string.IsNullOrEmpty(npcName) || npcName == "Retrieving data...")
                {
                    npcName = Lang.GetNPCNameValue(npc.type);
                }
                if (string.IsNullOrEmpty(npcName))
                {
                    npcName = "Unknown";
                }

                List<IItemDropRule> dropRules = Main.ItemDropsDB.GetRulesForNPCID(npc.type, includeGlobalDrops: false);
                if (dropRules == null || dropRules.Count == 0)
                {
                    continue;
                }

                MyDrop currentDrop = new()
                {
                    npc = new MyNPC
                    {
                        npcName = npcName,
                        npcId = npc.type,
                    },
                    items = []
                };

                foreach (var rule in dropRules)
                {
                    if (rule == null) continue;

                    List<DropRateInfo> dropRates = [];
                    rule.ReportDroprates(dropRates, new DropRateInfoChainFeed(1f));

                    if (dropRates.Count > 0)
                    {
                        foreach (var dropInfo in dropRates)
                        {
                            Item dropItem = new(dropInfo.itemId);
                            if (dropItem == null || dropItem.type == ItemID.None) continue;

                            MyDropItem currentDropItem = new()
                            {
                                itemName = dropItem.Name,
                                itemId = dropItem.type,
                                itemMinStack = dropInfo.stackMin,
                                itemMaxStack = dropInfo.stackMax,
                                dropRate = dropInfo.dropRate,
                                conditions = []
                            };

                            if (dropInfo.conditions?.Count > 0)
                            {
                                foreach (IItemDropRuleCondition condition in dropInfo.conditions)
                                {
                                    if (condition == null) continue;

                                    string condition_description = condition.GetConditionDescription();
                                    if (string.IsNullOrEmpty(condition_description)) continue;

                                    currentDropItem.conditions.Add(
                                        new MyCondition
                                        {
                                            conditionDescription = condition_description
                                        }
                                    );
                                }
                            }

                            currentDrop.items.Add(currentDropItem);
                        }
                    }
                }

                allDrops.Add(currentDrop);
            }

            SaveToFile("Drops.json", allDrops);
        }
    }
} 