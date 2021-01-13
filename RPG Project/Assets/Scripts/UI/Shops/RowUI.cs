using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Inventories;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI.Shops
{
    public class RowUI : MonoBehaviour
    {
        [SerializeField]
        Image iconField;
        [SerializeField]
        TextMeshProUGUI nameField;
        [SerializeField]
        TextMeshProUGUI stockField;
        [SerializeField]
        TextMeshProUGUI priceField;
        [SerializeField]
        TextMeshProUGUI quantityField;

        Shop currentShop;
        ShopItem currentItem;

        public void Setup(Shop shop, ShopItem item)
        {
            currentShop = shop;
            currentItem = item;
            iconField.sprite = item.GetIcon();
            nameField.text = item.GetItemName();
            stockField.text = $"{item.GetStock()}";
            priceField.text = $"${item.GetPrice():N2}";
            quantityField.text = $"{item.GetQuantity()}";
        }

        public void AddItem()
        {
            currentShop.AddToTransaction(currentItem.GetInventoryItem(), 1);
        }

        public void RemoveItem()
        {
            currentShop.AddToTransaction(currentItem.GetInventoryItem(), -1);
        }
    }
}