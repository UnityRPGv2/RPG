## RPG Crafting System
This is a simple crafting system for the GameDev.tv RPG course. This crafting system was highly inspired by Valheim.

### Setup
#### Cursor Type
* Add a new entry to the `CursorType` enum and call it `Crafting`
* Add a new mapping to the `PlayerController` for this `CursorType` and select an icon that will represent crafting

#### Crafting Table
* Place a crafting table model somewhere in the scene and add the `CraftingTable` component to it. You can have multiple crafting tables.

#### Crafting UI
* Add the `Crafting UI` to the `UI Canvas` game object in the `Core` prefab

#### Recipes
* Make some recipes (be sure to remove the demo recipe or it will appear in your game)

### Notes
* I made an extension method for the inventory system to check if an item is in the inventory and get the amount if any. It's ever so slightly less performant, but saves me from having to change the `Inventory` class and explain here how to do it, since I can't add it to this package.
* This package is dependent on the RPG course - especially the inventory system. The UI uses sprites that are included in the course and are therefore not in this package. Sprites that are not in the course were either created by myself, or modified from course images (I made a horizontal scrollbar from the vertical ones) and are included here.
