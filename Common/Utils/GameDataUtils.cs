using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Map;
using Terraria.Enums;
using Terraria.GameContent;
using RecipeExtract.Common.Models;

namespace RecipeExtract.Common.Utils
{
    public static class GameDataUtils
    {
        /// <summary>
        /// Converts a Terraria Item to our custom MyItem format
        /// </summary>
        public static MyItem GetMyItemByItem(Item item)
        {
            return new MyItem
            {
                itemId = item.type,
                itemName = item.Name, // Get localized name
                itemAmount = item.stack,
                ModName = item.ModItem?.Mod.Name ?? "Terraria"
            };
        }

        /// <summary>
        /// Converts a tile ID to our custom MyTile format
        /// </summary>
        public static MyTile GetMyTileByTileId(int tileId)
        {
            return new MyTile
            {
                tileId = tileId,
                tileName = Lang.GetMapObjectName(MapHelper.TileToLookup(tileId, 0)) // Try to get the tile name
            };
        }

        /// <summary>
        /// Converts a Terraria Condition to our custom MyCondition format
        /// </summary>
        public static MyCondition GetMyConditionByCondition(Condition condition)
        {
            return new MyCondition
            {
                conditionDescription = condition.Description.Value
            };
        }

        /// <summary>
        /// Gets the shimmer equivalent type for an item
        /// </summary>
        public static int GetShimmerEquivalentType(Item item)
        {
            if (ItemID.Sets.ShimmerCountsAsItem[item.type] != -1)
            {
                return ItemID.Sets.ShimmerCountsAsItem[item.type];
            }
            return item.type;
        }

        /// <summary>
        /// Gets the shimmer transformation result for an item
        /// </summary>
        public static List<Item> GetShimmerResult(Item input, out int stackRequired, out int decraftingRecipeIndex)
        {
            stackRequired = 1;
            int shimmerEquivalentType = GetShimmerEquivalentType(input);
            decraftingRecipeIndex = ShimmerTransforms.GetDecraftingRecipeIndex(shimmerEquivalentType);
            switch (shimmerEquivalentType)
            {
                case 1326:
                    return [new Item(5335)];
                case 779:
                    return [new Item(5134)];
                case 3031:
                    return [new Item(5364)];
                case 5364:
                    return [new Item(3031)];
                case 3461:
                    {
                        short type = Main.GetMoonPhase() switch
                        {
                            MoonPhase.QuarterAtRight => 5407,
                            MoonPhase.HalfAtRight => 5405,
                            MoonPhase.ThreeQuartersAtRight => 5404,
                            MoonPhase.Full => 5408,
                            MoonPhase.ThreeQuartersAtLeft => 5401,
                            MoonPhase.HalfAtLeft => 5403,
                            MoonPhase.QuarterAtLeft => 5402,
                            _ => 5406
                        };

                        return [new Item(type)];
                    }
                default:
                    {
                        if (input.createTile is TileID.MusicBoxes)
                        {
                            return [new Item(576)];
                        }

                        if (ItemID.Sets.ShimmerTransformToItem[shimmerEquivalentType] > 0)
                        {
                            return [new Item(ItemID.Sets.ShimmerTransformToItem[shimmerEquivalentType])];
                        }

                        if (decraftingRecipeIndex >= 0)
                        {
                            Recipe recipe = Main.recipe[decraftingRecipeIndex];
                            stackRequired = recipe.createItem.stack;
                            List<Item> enumerable = recipe.requiredItem;
                            if (recipe.customShimmerResults != null)
                                enumerable = recipe.customShimmerResults;
                            return enumerable;
                        }

                        break;
                    }
            }

            return null;
        }
    }
} 