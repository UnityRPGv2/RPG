using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevTV.Inventories;
using System;
using RPG.Control;

namespace RPG.Inventories
{
public class Shop : MonoBehaviour, IRaycastable
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
    }

}
