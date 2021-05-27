using GameDevTV.Inventories;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Inventories
{
    public class CurrencyPickup : Pickup
    {
        Purse purse;

        private void Awake()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            purse = player.GetComponent<Purse>();
        }

        public override void PickupItem()
        {
            if (GetItem() is CurrencyItem item)
            {
                float amount = item.GetPrice() * GetNumber();
                purse.UpdateBalance(amount);
                Destroy(gameObject);
            }
            else
            {
                base.PickupItem();
            }
        }

        public override bool CanBePickedUp()
        {
            if (GetItem() is CurrencyItem) return true;
            return base.CanBePickedUp();
        }

    }
}