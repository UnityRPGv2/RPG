using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevTV.Inventories;
using System;

namespace RPG.Inventories
{
public class Shop : MonoBehaviour
    {
        public class ShopItem
        {
            InventoryItem item;
            int stock;
            float price;
            int quantityInTransaction;
        }

        public event Action onChange; 

        public IEnumerable<ShopItem> GetFilteredItems(){ return null; }
        public void SelectFilter(ItemCategory category){}
        public ItemCategory GetFilter(){ return default;}
        public void SelectMode(bool buying){}
        public bool IsBuyingMode(){ return default; }
        public bool CanTransact(){ return default; }
        public void ConfirmTransaction(){}
        public float BasketTotal() { return default; }
        public void AddToTransaction(InventoryItem item, int quantity) {}
}

}
