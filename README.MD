# RecipeExtract

A Terraria mod that extracts and exports game data to JSON files. This mod provides a simple way to export recipes, shimmer transformations, shop items, NPC drops, and item textures for data analysis and tool development.

## Features

- **Item Export**: Exports all item data including:
  - Item ID
  - Item name
  - Stack size
  - Mod name
  - Item textures (as PNG files)

- **Recipe Export**: Exports all crafting recipes including:
  - Resulting items
  - Required ingredients
  - Required crafting stations
  - Crafting conditions

- **Shimmer Export**: Exports all shimmer transformation data including:
  - Source items
  - Transformation results
  - Special cases (like music boxes)

- **Shop Export**: Exports all NPC shop data including:
  - Shop NPC information
  - Available items
  - Item prices
  - Purchase conditions

- **Drop Export**: Exports all NPC drop data including:
  - NPC information
  - Droppable items
  - Drop rates
  - Stack sizes
  - Drop conditions

## Technical Details

### Data Models

The mod uses several data models to represent game data:

- `MyItem`: Basic item information
  - Item ID
  - Item name
  - Stack size
  - Mod name

- `MyNPC`: NPC information
  - NPC ID
  - NPC name
  - Mod name

- `MyTile`: Crafting station information
  - Tile ID
  - Tile name

- `MyCondition`: Game condition information
  - Condition description

- `MyRecipe`: Complete recipe information
  - Result item
  - Required ingredients
  - Required stations
  - Required conditions

- `MyShimmer`: Shimmer transformation information
  - Source item
  - Result items

- `MyShop`: Shop information
  - Shop NPC
  - Shop name
  - Available items
  - Item prices
  - Purchase conditions

- `MyDrop`: NPC drop information
  - NPC details
  - Droppable items
  - Drop rates
  - Stack sizes
  - Drop conditions

### Project Structure

```
RecipeExtract/
├── Common/
│   ├── Models/         # Data model classes
│   ├── Commands/       # Chat command implementation
│   ├── Services/       # Data export service
│   └── Utils/          # Utility functions
├── ItemSaver.cs        # Main mod class
└── README.md           # This file
```

### Dependencies

- tModLoader v1.4.4 or later
- Newtonsoft.Json (for JSON serialization)

## Usage

1. Install the mod through tModLoader
2. In-game, type `/RecipeExtract` in the chat
3. The mod will export the following files to your save folder:
  - `ItemTextures/`: Directory containing all item textures as PNG files
  - `Items.json`: All item data
  - `Recipes.json`: All crafting recipes
  - `Shimmers.json`: All shimmer transformations
  - `Shops.json`: All NPC shop inventories
  - `Drops.json`: All NPC drop tables

## Output Format

All data is exported in UTF-8 encoded JSON format with proper indentation for readability. The JSON structure follows the data models described above.

Example item JSON:
```json
{
  "itemId": 1,
  "itemName": "Iron Pickaxe",
  "itemAmount": 1,
  "ModName": "Terraria"
}
```

## Contributing

Feel free to submit issues and pull requests. For major changes, please open an issue first to discuss what you would like to change.

## Acknowledgments

- tModLoader team for the modding framework
- Terraria developers for the amazing game
- The Terraria modding community for their support and resources
