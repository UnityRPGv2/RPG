using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevTV.Inventories;
using System;
using RPG.Control;
using RPG.Stats;

namespace RPG.Inventories
{
    public partial class Shop : MonoBehaviour, IRaycastable
    {
        [SerializeField] string shopName;
        [SerializeField] StockItemConfig[] stockConfig;
        [SerializeField] float sellingDiscount = 0.6f;

        Dictionary<InventoryItem, int> transaction = new Dictionary<InventoryItem, int>();
        Dictionary<InventoryItem, int> stockSold = new Dictionary<InventoryItem, int>();
        bool isBuying = true;
        ItemCategory filter = ItemCategory.None;

        Shopper shopper = null;

        public event Action onChange; 

        public void SetCurrentShopper(Shopper newShopper)
        {
            shopper = newShopper;
        }

        public IEnumerable<ShopItem> GetFilteredItems()
        {
            foreach (var item in GetAllItems())
            {
                if (filter != ItemCategory.None && item.GetInventoryItem().GetCategory() != filter) continue;
                yield return item;
            }
        }

        public IEnumerable<ShopItem> GetAllItems()
        {
            var prices = GetPrices();
            var itemAvailabilities = GetItemAvailabilities();
            foreach (InventoryItem item in GetAvailableItems())
            {
                float price = 0;
                prices.TryGetValue(item, out price);
                int transactionQuantity = 0;
                transaction.TryGetValue(item, out transactionQuantity);
                int itemAvailability = 0;
                itemAvailabilities.TryGetValue(item, out itemAvailability);
                yield return new ShopItem(item, itemAvailability, price, transactionQuantity);
            }
        }

        public IEnumerable<InventoryItem> GetAvailableItems()
        {
            var available = new Dictionary<InventoryItem, int>();
            int level = GetLevel();
            foreach (StockItemConfig item in stockConfig)
            {
                if (item.unlockLevel > GetLevel()) continue;
                available[item.inventoryItem] = 1;
            }
            return available.Keys;
        }

        public Dictionary<InventoryItem, float> GetPrices()
        {
            var prices = new Dictionary<InventoryItem, float>();
            foreach (StockItemConfig item in stockConfig)
            {
                var invItem = item.inventoryItem;
                if (item.unlockLevel > GetLevel()) continue;

                if (!prices.ContainsKey(invItem))
                {
                    prices[invItem] = item.inventoryItem.GetPrice();
                    if (!isBuying)
                    {
                        prices[invItem] *= sellingDiscount;
                    }
                }
                if (isBuying)
                {  
                    prices[invItem] *= (1 - item.buyDiscountPercentage);
                }    
            }
            return prices;
        }

        public Dictionary<InventoryItem, int> GetItemAvailabilities()
        {
            var availabilities = new Dictionary<InventoryItem, int>();
            foreach (StockItemConfig item in stockConfig)
            {
                var invItem = item.inventoryItem;
                if (item.unlockLevel > GetLevel()) continue;

                if (isBuying)
                {
                    if (!availabilities.ContainsKey(invItem))
                    {
                        int sold = 0;
                        stockSold.TryGetValue(invItem, out sold);   
                        availabilities[invItem] = -sold;
                    }
                    availabilities[invItem] += item.initialStock;
                }
                else
                {
                    availabilities[invItem] = CountItemsInInventory(invItem);
                }
            }
            return availabilities;
        }

        public void SelectFilter(ItemCategory category)
        {
            filter = category;
            if (onChange != null) onChange();
        }

        public ItemCategory GetFilter(){ return filter; }

        public void SelectMode(bool buying)
        {
            isBuying = buying;
            if (onChange != null) onChange();
        }
        public bool IsBuyingMode(){ return isBuying; }
        public bool CanTransact()
        { 
            // Empty transaction
            if (transaction.Count == 0) return false;
            // Not sufficient funds
            if (!HasSufficientFunds()) return false;
            // Not sufficient inventory space
            if (!HasInventorySpace()) return false;

            return true;
        }

        public bool HasSufficientFunds()
        {
            if (!isBuying) return true;

            var purse = shopper.GetComponent<Purse>();
            if (!purse) return false;

            return purse.GetBalance() >= BasketTotal();
        }

        public bool HasInventorySpace()
        {
            if (!isBuying) return true;

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
                    bool success = false;
                    var invItem = item.GetInventoryItem();
                    if (isBuying)
                    {
                        if (purse.GetBalance() < item.GetPrice()) break;
                        success = inventory.AddToFirstEmptySlot(invItem, 1);
                    }             
                    else
                    {
                        int slot = FindFirstItemSlot(invItem);
                        if (slot == -1) break;
                        inventory.RemoveFromSlot(slot, 1);
                        success = true;
                    }
                    if (success)
                    {
                        transaction[invItem]--;
                        int sign = isBuying ? -1 : 1;
                        if (!stockSold.ContainsKey(invItem))
                        {
                            stockSold[invItem] = 0;
                        }
                        stockSold[invItem] -= sign;
                        purse.UpdateBalance(sign * item.GetPrice());
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

            var availabilities = GetItemAvailabilities();
            int availability = 0;
            availabilities.TryGetValue(item, out availability);
            if (availability >= transaction[item] + quantity)
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

        private int CountItemsInInventory(InventoryItem item)
        {
            var inventory = shopper.GetComponent<Inventory>();
            if (!inventory) return 0;

            int count = 0;
            for (int i = 0; i < inventory.GetSize(); i++)
            {
                if (inventory.GetItemInSlot(i) == item)
                {
                    count += inventory.GetNumberInSlot(i);
                }
            }
            return count;
        }

        private int FindFirstItemSlot(InventoryItem item)
        {
            var inventory = shopper.GetComponent<Inventory>();
            if (!inventory) return -1;

            for (int i = 0; i < inventory.GetSize(); i++)
            {
                if (inventory.GetItemInSlot(i) == item)
                {
                    return i;
                }
            }

            return -1;
        }

        private int GetLevel()
        {
            var baseStats = shopper.GetComponent<BaseStats>();
            if (!baseStats) return 0;
            return baseStats.GetLevel();
        }

        [System.Serializable]
        private class StockItemConfig
        {
            public InventoryItem inventoryItem;
            public int initialStock;
            public float buyDiscountPercentage;
            public int unlockLevel;
        }
    }

}
