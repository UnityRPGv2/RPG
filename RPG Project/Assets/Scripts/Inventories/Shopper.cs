using System;
using UnityEngine;

namespace RPG.Inventories
{
    public class Shopper : MonoBehaviour {
        
        public event Action activeShopChanged;
        Shop activeShop = null;

        public void SetActiveShop(Shop shop)
        {
            if (activeShop) activeShop.SetCurrentShopper(null);
            activeShop = shop;
            if (activeShop) activeShop.SetCurrentShopper(this);
            activeShopChanged();
        }

        public Shop GetActiveShop()
        {
            return activeShop;
        }
    }
}