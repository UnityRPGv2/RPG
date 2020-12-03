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


        public void Setup(ShopItem item)
        {
            iconField.sprite = item.GetIcon();
            nameField.text = item.GetItemName();
            stockField.text = $"{item.GetStock()}";
            priceField.text = $"${item.GetPrice():N2}";
        }
    }
}