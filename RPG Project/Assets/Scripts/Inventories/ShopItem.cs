using System;
using GameDevTV.Inventories;
using UnityEngine;

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

        public string GetItemName()
        {
            return item.GetDisplayName();
        }

        public Sprite GetIcon()
        {
            return item.GetIcon();
        }

        public float GetPrice()
        {
            return price;
        }

        public int GetStock()
        {
            return stock;
        }

        public InventoryItem GetInventoryItem()
        {
            return item;
        }

        public object GetQuantity()
        {
            return quantityInTransaction;
        }
    }

}
