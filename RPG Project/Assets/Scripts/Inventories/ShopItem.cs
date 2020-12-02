using GameDevTV.Inventories;

namespace RPG.Inventories
{
    public class ShopItem
    {
        InventoryItem item;
        int stock;
        float price;
        int quantityInTransaction;

        public ShopItem(InventoryItem item, int stock, float price, int quantityInTransaction)
        {
            this.item = item;
            this.stock = stock;
            this.price = price;
            this.quantityInTransaction = quantityInTransaction;
        }
    }

}
