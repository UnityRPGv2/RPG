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
        Shopper shopper = null;

        public event Action onChange; 

        public void SetCurrentShopper(Shopper newShopper)
        {
            shopper = newShopper;
        }

        public IEnumerable<ShopItem> GetFilteredItems()
        {
            return GetAllItems();
        }

        public IEnumerable<ShopItem> GetAllItems()
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

        public void ConfirmTransaction()
        {
            // Transfer Items
            var inventory = shopper.GetComponent<Inventory>();
            if (!inventory) return;
            var transactionCopy = new Dictionary<InventoryItem, int>(transaction);
            foreach (var item in transactionCopy.Keys)
            {
                for (int i = 0; i < transactionCopy[item]; i++)
                {
                    bool success = inventory.AddToFirstEmptySlot(item, 1);
                    if (success)
                    {
                        AddToTransaction(item, -1);
                    }
                }
            }
            // Deduct funds
            // Clear transaction

            if (onChange != null) onChange();
        }
        public float BasketTotal() 
        {
            float total = 0;
            foreach (var item in GetAllItems())
            {
                total += item.GetPrice() * item.GetQuantity();
            }
            return total;
        }
        public void AddToTransaction(InventoryItem item, int quantity)
        {
            if (!transaction.ContainsKey(item))
            {
                transaction[item] = 0;
            }

            transaction[item] += quantity;

            if (transaction[item] <= 0)
            {
                transaction.Remove(item);
            }

            if (onChange != null) onChange();
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
