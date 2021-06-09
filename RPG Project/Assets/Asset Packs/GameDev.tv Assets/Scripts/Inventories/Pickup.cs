using UnityEngine;

namespace GameDevTV.Inventories
{
    /// <summary>
    /// To be placed at the root of a Pickup prefab. Contains the data about the
    /// pickup such as the type of item and the number.
    /// </summary>
    public class Pickup : MonoBehaviour
    {
        // STATE
        InventoryItem item;
        int number = 1;

        // CACHED REFERENCE
        GameObject player;

        // LIFECYCLE METHODS

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        // PUBLIC

        /// <summary>
        /// Set the vital data after creating the prefab.
        /// </summary>
        /// <param name="item">The type of item this prefab represents.</param>
        /// <param name="number">The number of items represented.</param>
        public void Setup(InventoryItem item, int number)
        {
            this.item = item;
            if (!item.IsStackable())
            {
                number = 1;
            }
            this.number = number;
        }

        public InventoryItem GetItem()
        {
            return item;
        }

        public int GetNumber()
        {
            return number;
        }

        public virtual void PickupItem()
        {
            if (CanBePickedUp())
            {
                ItemStore.GiveItem(player, item, number);
                Destroy(gameObject);
            }
            
        }

        public virtual bool CanBePickedUp()
        {
            return ItemStore.CanAccept(player, item, number);
        }
    }
}