using UnityEngine;

namespace RPG.Inventories {
    public class Purse : MonoBehaviour {
        [SerializeField] float startingBalance = 400f;

        float balance = 0;

        private void Awake() {
            balance = startingBalance;
            print($"Balance: {balance}");
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