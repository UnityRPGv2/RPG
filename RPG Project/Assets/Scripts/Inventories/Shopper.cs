using System;
using UnityEngine;

namespace RPG.Inventories
{
    public class Shopper : MonoBehaviour {
        
        public event Action activeShopChanged;
        Shop activeShop = null;

        public void SetActiveShop(Shop shop)
        {
            activeShop = shop;
            activeShopChanged();
        }

        public Shop GetActiveShop()
        {
            return activeShop;
        }
    }
}