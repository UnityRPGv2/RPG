using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Inventories;
using System;
using TMPro;

namespace RPG.UI.Shops 
{
    public class ShopUI : MonoBehaviour
    {
        Shopper shopper = null;
        Shop currentShop = null;

        [SerializeField] TextMeshProUGUI shopName;

        private void Start() {
            gameObject.SetActive(false);

            shopper = GameObject.FindGameObjectWithTag("Player").GetComponent<Shopper>();
            if (shopper == null) return;

            shopper.activeShopChanged += ShopChanged;
        }

        private void ShopChanged()
        {
            currentShop = shopper.GetActiveShop();
            gameObject.SetActive(currentShop != null);

            if (currentShop == null) return;

            shopName.text = currentShop.GetShopName();
        }        

        public void Close()
        {
            shopper.SetActiveShop(null);
        }
    }
}