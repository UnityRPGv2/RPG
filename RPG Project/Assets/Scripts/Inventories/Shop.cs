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

        public event Action onChange; 

        public IEnumerable<ShopItem> GetFilteredItems()
        {
            yield return new ShopItem(InventoryItem.GetFromID("77ad666c-513c-4c88-87b2-c8594bcd5a89"), 10, 10.50f, 0);
            yield return new ShopItem(InventoryItem.GetFromID("7f8ab35d-6fd6-42b0-9725-0121288138e4"), 10, 10.50f, 0);
            yield return new ShopItem(InventoryItem.GetFromID("2bb41498-0e04-482d-a7fb-d0ce934b06c0"), 10, 10.50f, 0);
            yield return new ShopItem(InventoryItem.GetFromID("e75a0c32-d41c-4651-8496-92cb958a8f1e"), 100, 2.0f, 0);
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
