using System;
using GameDevTV.Saving;
using UnityEngine;

namespace RPG.Inventories {
    public class Purse : MonoBehaviour, ISaveable
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
    }
}