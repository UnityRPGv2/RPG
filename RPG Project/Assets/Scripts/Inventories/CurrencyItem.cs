using GameDevTV.Inventories;
using UnityEngine;

namespace RPG.Inventories 
{
    [CreateAssetMenu(fileName = "Currency Item", menuName = "RPG Project/Currency Item", order = 0)]
    public class CurrencyItem : InventoryItem
    {
        private void Awake() {
            stackable = true;
        }
    }
}