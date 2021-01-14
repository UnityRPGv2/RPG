using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Inventories;
using TMPro;

namespace RPG.UI.Shops {
    public class PurseUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI balanceField;

        Purse purse;

        // Start is called before the first frame update
        void Start()
        {
            purse = GameObject.FindGameObjectWithTag("Player").GetComponent<Purse>();
            purse.onChange += RefreshUI;
            RefreshUI();
        }

        void RefreshUI()
        {
            balanceField.text = $"${purse.GetBalance():N2}";
        }
    }
}