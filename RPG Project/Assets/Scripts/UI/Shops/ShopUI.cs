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
        [SerializeField] TextMeshProUGUI basketTotalField;
        [SerializeField] Transform listRoot;
        [SerializeField] RowUI rowPrefab;

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

            if (currentShop != null) 
            {
                currentShop.onChange -= RefreshUI;
            }

            if (currentShop == null) return;
            currentShop.onChange += RefreshUI;

            RefreshUI();
        }        

        private void RefreshUI()
        {
            shopName.text = currentShop.GetShopName();
            basketTotalField.text = $"Total: ${currentShop.BasketTotal():N2}";

            foreach (Transform child in listRoot)
            {
                Destroy(child.gameObject);
            }

            foreach (var item in currentShop.GetFilteredItems())
            {
                RowUI row = Instantiate<RowUI>(rowPrefab, listRoot);
                row.Setup(currentShop, item);
            }
        }

        public void Close()
        {
            shopper.SetActiveShop(null);
        }

        public void ConfirmTransaction()
        {
            currentShop.ConfirmTransaction();
        }
    }
}