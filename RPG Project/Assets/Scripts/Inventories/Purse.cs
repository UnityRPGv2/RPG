using System;
using System.Collections.Generic;
using GameDevTV.Inventories;
using GameDevTV.Saving;
using UnityEngine;

namespace RPG.Inventories {
    public class Purse : MonoBehaviour, ISaveable, IItemStore
    {
        [SerializeField] float startingBalance = 400f;

        float balance = 0;

        public event Action onChange;

        private void Awake() {
            balance = startingBalance;
        }

        public float GetBalance()
        {
            return balance;
        }

        public void UpdateBalance(float amount)
        {
            balance += amount;
            if (onChange != null)
            {
                onChange();
            }
        }

        public object CaptureState()
        {
            return balance;
        }

        public void RestoreState(object state)
        {
            balance = (float)state;
        }

        public void AddItems(InventoryItem item, int number)
        {
            UpdateBalance(number * item.GetPrice());
        }

        public IEnumerable<(InventoryItem, int)> CanAcceptItems(IEnumerable<(InventoryItem, int)> items)
        {
            foreach (var (item, number) in items)
            {
                if (item is CurrencyItem currencyItem)
                {
                    yield return (currencyItem, number);
                }
            }
        }

        public int GetPriority()
        {
            return 20;
        }
    }
}