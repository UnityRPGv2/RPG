using System;
using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using RPG.Control;
using UnityEngine;

namespace RPG.Shops
{
    public class Shop : MonoBehaviour, IRaycastable
    {
        [SerializeField] string shopName;

        // Stock Config
            // Item: 
                // InventoryItem
                // Initial Stock
                // buyingDiscount
        [SerializeField]
        StockItemConfig[] stockConfig;

        [System.Serializable]
        class StockItemConfig
        {
            public InventoryItem item;
            public int initialStock;
            [Range(0, 100)]
            public float buyingDiscountPercentage;
        }

        Dictionary<InventoryItem, int> transaction = new Dictionary<InventoryItem, int>();
        Shopper currentShopper = null;

        public event Action onChange;

        public void SetShopper(Shopper shopper)
        {
            currentShopper = shopper;
        }

        public IEnumerable<ShopItem> GetFilteredItems() 
        {
            foreach (StockItemConfig config in stockConfig)
            {
                float price = config.item.GetPrice() * (1 - config.buyingDiscountPercentage/100);
                int quantityInTransaction = 0;
                transaction.TryGetValue(config.item, out quantityInTransaction);
                yield return new ShopItem(config.item, config.initialStock, price, quantityInTransaction);
            }
        }

        public void SelectFilter(ItemCategory category) {}
        public ItemCategory GetFilter() { return ItemCategory.None; }
        public void SelectMode(bool isBuying) {}
        public bool IsBuyingMode() { return true; }
        public bool CanTransact() { return true; }

        public void ConfirmTransaction()
        {
            Inventory shopperInventory = currentShopper.GetComponent<Inventory>();
            if (shopperInventory == null) return;

            // Transfer to or from the inventory
            var transactionSnapshot = new Dictionary<InventoryItem, int>(transaction);
            foreach (InventoryItem item in transactionSnapshot.Keys)
            {
                int quantity = transactionSnapshot[item];
                for (int i = 0; i < quantity; i++)
                {
                    bool success = shopperInventory.AddToFirstEmptySlot(item, 1);
                    if (success)
                    {
                        AddToTransaction(item, -1);
                    }
                }
            }
            // Removal from transaction
            // Debting or Crediting of funds
        }

        public float TransactionTotal() { return 0; }

        public string GetShopName()
        {
            return shopName;
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

            if (onChange != null)
            {
                onChange();
            }
        }

        public CursorType GetCursorType()
        {
            return CursorType.Shop;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (Input.GetMouseButtonDown(0))
            {
                callingController.GetComponent<Shopper>().SetActiveShop(this);
            }
            return true;
        }
    }
}