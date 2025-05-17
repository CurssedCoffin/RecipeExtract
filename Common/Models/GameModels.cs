using System.Collections.Generic;

namespace RecipeExtract.Common.Models
{
    /// <summary>
    /// Represents an item with its basic properties
    /// </summary>
    public class MyItem
    {
        public int itemId;           // Unique identifier for the item
        public string itemName;      // Localized name of the item
        public int itemAmount;       // Stack size or quantity
        public string ModName;       // Name of the mod this item belongs to, "Terraria" for vanilla items
    }

    /// <summary>
    /// Represents a tile (crafting station) with its properties
    /// </summary>
    public class MyTile
    {
        public int tileId;           // Unique identifier for the tile
        public string tileName;      // Localized name of the tile
    }

    /// <summary>
    /// Represents a condition required for crafting or other game mechanics
    /// </summary>
    public class MyCondition
    {
        public string conditionDescription;  // Human-readable description of the condition
    }

    /// <summary>
    /// Represents a complete crafting recipe
    /// </summary>
    public class MyRecipe
    {
        public MyItem item;                  // The resulting item
        public List<MyItem> ingredients;     // Required materials
        public List<MyTile> tiles;           // Required crafting stations
        public List<MyCondition> conditions; // Required conditions
    }

    /// <summary>
    /// Represents a shimmer transformation
    /// </summary>
    public class MyShimmer
    {
        public MyItem item;                  // Item to be transformed
        public List<MyItem> shimmerResult;   // Resulting items after transformation
    }

    /// <summary>
    /// Represents an NPC with basic properties
    /// </summary>
    public class MyNPC
    {
        public int npcId;                    // Unique identifier for the NPC
        public string npcName;               // Localized name of the NPC
    }

    /// <summary>
    /// Represents an item sold in a shop
    /// </summary>
    public class MyShopItem
    {
        public MyItem item;                  // The item being sold
        public int price;                    // Price of the item
        public List<MyCondition> conditions; // Conditions required to purchase
    }

    /// <summary>
    /// Represents a complete shop with its inventory
    /// </summary>
    public class MyShop
    {
        public MyNPC npc;                    // The NPC running the shop
        public string shopName;              // Name of the shop
        public string ModName;               // Name of the mod this shop belongs to
        public List<MyShopItem> items;       // Items available in the shop
    }

    /// <summary>
    /// Represents an item that can drop from an NPC
    /// </summary>
    public class MyDropItem
    {
        public string itemName;              // Name of the dropped item
        public int itemId;                   // Unique identifier for the item
        public int itemMinStack;             // Minimum stack size
        public int itemMaxStack;             // Maximum stack size
        public float dropRate;               // Drop rate as a decimal (0.0 to 1.0)
        public List<MyCondition> conditions; // Conditions required for the drop
    }

    /// <summary>
    /// Represents all possible drops from an NPC
    /// </summary>
    public class MyDrop
    {
        public MyNPC npc;                    // The NPC that can drop these items
        public List<MyDropItem> items;       // List of possible drops
    }
} 