# RecipeExtract

一个用于提取和导出游戏数据到JSON文件的Terraria模组。该模组提供了一种简单的方式来导出配方、微光转换、商店物品、NPC掉落物和物品纹理，用于数据分析和工具开发。

## 功能特性

- **物品导出**：导出所有物品数据，包括：
  - 物品ID
  - 物品名称
  - 堆叠数量
  - 模组名称
  - 物品纹理（PNG格式）

- **配方导出**：导出所有制作配方，包括：
  - 成品物品
  - 所需材料
  - 所需制作站
  - 制作条件

- **微光转换导出**：导出所有微光转换数据，包括：
  - 源物品
  - 转换结果
  - 特殊情况（如音乐盒）

- **商店导出**：导出所有NPC商店数据，包括：
  - 商店NPC信息
  - 可用物品
  - 物品价格
  - 购买条件

- **掉落物导出**：导出所有NPC掉落数据，包括：
  - NPC信息
  - 可掉落物品
  - 掉落率
  - 堆叠数量
  - 掉落条件

## 技术细节

### 数据模型

该模组使用多个数据模型来表示游戏数据：

- `MyItem`：基本物品信息
  - 物品ID
  - 物品名称
  - 堆叠数量
  - 模组名称

- `MyNPC`：NPC信息
  - NPC ID
  - NPC名称
  - 模组名称

- `MyTile`：制作站信息
  - 方块ID
  - 方块名称

- `MyCondition`：游戏条件信息
  - 条件描述

- `MyRecipe`：完整配方信息
  - 成品物品
  - 所需材料
  - 所需制作站
  - 所需条件

- `MyShimmer`：微光转换信息
  - 源物品
  - 结果物品

- `MyShop`：商店信息
  - 商店NPC
  - 商店名称
  - 可用物品
  - 物品价格
  - 购买条件

- `MyDrop`：NPC掉落信息
  - NPC详情
  - 可掉落物品
  - 掉落率
  - 堆叠数量
  - 掉落条件

### 项目结构

```
RecipeExtract/
├── Common/
│   ├── Models/         # 数据模型类
│   ├── Commands/       # 聊天命令实现
│   ├── Services/       # 数据导出服务
│   └── Utils/          # 工具函数
├── ItemSaver.cs        # 主模组类
└── README.md           # 说明文件
```

### 依赖项

- tModLoader v1.4.4 或更高版本
- Newtonsoft.Json（用于JSON序列化）

## 使用方法

1. 通过tModLoader安装模组
2. 在游戏中，在聊天框中输入 `/RecipeExtract`
3. 模组将在你的存档文件夹中导出以下文件：
   - `ItemTextures/`：包含所有物品纹理的PNG文件目录
   - `Items.json`：所有物品数据
   - `Recipes.json`：所有制作配方
   - `Shimmers.json`：所有微光转换
   - `Shops.json`：所有NPC商店库存
   - `Drops.json`：所有NPC掉落表

## 输出格式

所有数据都以UTF-8编码的JSON格式导出，并进行了适当的缩进以提高可读性。JSON结构遵循上述数据模型。

物品JSON示例：
```json
{
    "itemId": 1,
    "itemName": "铁镐",
    "itemAmount": 1,
    "ModName": "Terraria"
}
```

## 贡献

欢迎提交问题和拉取请求。对于重大更改，请先开一个issue来讨论你想要更改的内容。

## 致谢

- tModLoader团队提供的模组框架
- Terraria开发者们制作的精彩游戏
- Terraria模组社区的支持和资源 