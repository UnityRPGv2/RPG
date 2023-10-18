using GameDevTV.Inventories;

namespace RPG.Crafting
{
    public static class InventoryExtensions
    {
        // A helper to check if an inventory has a specific item and, if so, how many of it
        public static bool HasItem(this Inventory inventory, InventoryItem item, out int amount)
        {
            amount = 0;
            var hasItem = false;

            var inventorySize = inventory.GetSize();
            for (var i = 0; i < inventorySize; i++)
            {
                var testItem = inventory.GetItemInSlot(i);
                if (!object.ReferenceEquals(item, testItem))
                {
                    continue;
                }
                hasItem = true;
                amount += inventory.GetNumberInSlot(i);
            }

            return hasItem;
        }
    }
}
