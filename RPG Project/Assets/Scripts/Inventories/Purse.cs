using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Inventories {
    public class Purse : MonoBehaviour
    {
        [SerializeField] float startingBalance = 100;
        float balance;

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
            print($"Balance: {balance}");
        }
    }
}