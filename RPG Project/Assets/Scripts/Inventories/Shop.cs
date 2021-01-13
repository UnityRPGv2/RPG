using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevTV.Inventories;
using System;
using RPG.Control;

namespace RPG.Inventories
{
    public partial class Shop : MonoBehaviour, IRaycastable
    {
        [SerializeField] string shopName;
        [SerializeField] StockItemConfig[] stockConfig;

        Dictionary<InventoryItem, int> transaction = new Dictionary<InventoryItem, int>();

        public event Action onChange; 

        public IEnumerable<ShopItem> GetFilteredItems()
        {
            foreach (StockItemConfig item in stockConfig)
            {
                float price = item.inventoryItem.GetPrice() * (1 - item.buyDiscountPercentage);
                int transactionQuantity = 0;
                if (transaction.ContainsKey(item.inventoryItem))
                {
                    transactionQuantity = transaction[item.inventoryItem];
                }
                yield return new ShopItem(item.inventoryItem, item.initialStock, price, transactionQuantity);
            }
        }

        public void SelectFilter(ItemCategory category){}
        public ItemCategory GetFilter(){ return default;}
        public void SelectMode(bool buying){}
        public bool IsBuyingMode(){ return default; }
        public bool CanTransact(){ return default; }

        public string GetShopName()
        {
            return shopName;
        }

        public void ConfirmTransaction(){}
        public float BasketTotal() { return default; }
        public void AddToTransaction(InventoryItem item, int quantity)
        {
            print($"Updating transaction {quantity} {item.GetDisplayName()}");

            if (!transaction.ContainsKey(item))
            {
                transaction[item] = 0;
            }

            transaction[item] += quantity;

            if (transaction[item] <= 0)
            {
                transaction.Remove(item);
            }
        }

        public CursorType GetCursorType()
        {
            return CursorType.Shop;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            var shopper = callingController.GetComponent<Shopper>();
            if (shopper == null)
            {
                return false;
            }

            if (Input.GetMouseButtonDown(0))
            {
                shopper.SetActiveShop(this);
            }

            return true;
        }

        [System.Serializable]
        private class StockItemConfig
        {
            public InventoryItem inventoryItem;
            public int initialStock;
            public float buyDiscountPercentage;
        }
    }

}
