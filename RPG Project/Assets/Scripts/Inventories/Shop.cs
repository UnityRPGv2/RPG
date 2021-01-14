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
        Dictionary<InventoryItem, int> stock = new Dictionary<InventoryItem, int>();
        
        Shopper shopper = null;

        public event Action onChange; 

        private void Awake() {
            foreach (StockItemConfig item in stockConfig)
            {
                stock[item.inventoryItem] = item.initialStock;
            }
        }

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
                int itemStock = stock[item.inventoryItem];
                yield return new ShopItem(item.inventoryItem, itemStock, price, transactionQuantity);
            }
        }

        public void SelectFilter(ItemCategory category){}
        public ItemCategory GetFilter(){ return default;}
        public void SelectMode(bool buying){}
        public bool IsBuyingMode(){ return default; }
        public bool CanTransact()
        { 
            // Empty transaction
            if (transaction.Count == 0) return false;
            // Not sufficient funds
            if (!HasSufficientFunds()) return false;
            // Not sufficient inventory space
            if (!HasInventorySpace(shopper)) return false;

            return true;
        }

        public bool HasSufficientFunds()
        {
            var purse = shopper.GetComponent<Purse>();
            if (!purse) return false;

            return purse.GetBalance() >= BasketTotal();
        }

        public bool HasInventorySpace(Shopper shopper)
        {
            var inventory = shopper.GetComponent<Inventory>();
            if (!inventory) return false;

            inventory.TakeSnapshot();

            foreach (var item in GetAllItems())
            {
                for (int i = 0; i < item.GetQuantity(); i++)
                {
                    bool success = inventory.AddToFirstEmptySlot(item.GetInventoryItem(), 1);
                    if (!success) 
                    {
                        inventory.RevertSnapshot();
                        return false;
                    }
                }
            }

            inventory.RevertSnapshot();
            return true;
        }

        public string GetShopName()
        {
            return shopName;
        }

        public void ConfirmTransaction()
        {
            // Transfer Items
            var inventory = shopper.GetComponent<Inventory>();
            var purse = shopper.GetComponent<Purse>();
            if (!inventory || !purse) return;
            foreach (var item in GetAllItems())
            {
                for (int i = 0; i < item.GetQuantity(); i++)
                {
                    if (purse.GetBalance() < item.GetPrice()) break;                
                    bool success = inventory.AddToFirstEmptySlot(item.GetInventoryItem(), 1);
                    if (success)
                    {
                        transaction[item.GetInventoryItem()]--;
                        stock[item.GetInventoryItem()]--;
                        purse.UpdateBalance(-item.GetPrice());
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

            if (stock[item] >= transaction[item] + quantity)
            {
                transaction[item] += quantity;

                if (transaction[item] <= 0)
                {
                    transaction.Remove(item);
                }
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
