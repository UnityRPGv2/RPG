using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Shops;
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
        TextMeshProUGUI availabilityField;
        [SerializeField]
        TextMeshProUGUI priceField;
        
        public void Setup(ShopItem item)
        {
            iconField.sprite = item.GetIcon();
            nameField.text = item.GetName();
            availabilityField.text = $"{item.GetAvailability()}";
            priceField.text = $"${item.GetPrice():N2}";
        }
    }
}