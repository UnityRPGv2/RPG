using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Inventories;
using System;
using TMPro;
using UnityEngine.UI;

namespace RPG.UI.Shops 
{
    public class ShopUI : MonoBehaviour
    {
        Shopper shopper = null;
        Shop currentShop = null;

        [SerializeField] TextMeshProUGUI shopName;
        [SerializeField] TextMeshProUGUI basketTotalField;
        [SerializeField] Button switchModeButton;
        [SerializeField] Button confirmButton;
        [SerializeField] Transform listRoot;
        [SerializeField] RowUI rowPrefab;

        Color basketTotalFieldOriginalColor;

        private void Start() {
            gameObject.SetActive(false);
            basketTotalFieldOriginalColor = basketTotalField.color;

            shopper = GameObject.FindGameObjectWithTag("Player").GetComponent<Shopper>();
            if (shopper == null) return;

            shopper.activeShopChanged += ShopChanged;
            switchModeButton.onClick.AddListener(SwitchMode);
            confirmButton.onClick.AddListener(ConfirmTransaction);
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
            basketTotalField.color = currentShop.HasSufficientFunds() ? basketTotalFieldOriginalColor : Color.red;
            confirmButton.interactable = currentShop.CanTransact();
            var switchModeText = switchModeButton.GetComponentInChildren<TextMeshProUGUI>();
            switchModeText.text = currentShop.IsBuyingMode() ? "Switch to Selling" : "Switch to Buying";
            var confirmText = confirmButton.GetComponentInChildren<TextMeshProUGUI>();
            confirmText.text = currentShop.IsBuyingMode() ? "Buy" : "Sell";

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

        private void SwitchMode()
        {
            currentShop.SelectMode(!currentShop.IsBuyingMode());
        }

        public void ConfirmTransaction()
        {
            currentShop.ConfirmTransaction();
        }
    }
}