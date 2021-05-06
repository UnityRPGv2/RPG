using System.Collections;
using System.Collections.Generic;
using GameDevTV.Core.UI.Dragging;
using GameDevTV.Inventories;
using RPG.Abilities;
using UnityEngine;
using UnityEngine.UI;

namespace GameDevTV.UI.Inventories
{
    /// <summary>
    /// The UI slot for the player action bar.
    /// </summary>
    public class ActionSlotUI : MonoBehaviour, IItemHolder, IDragContainer<InventoryItem>
    {
        // CONFIG DATA
        [SerializeField] InventoryItemIcon icon = null;
        [SerializeField] int index = 0;
        [SerializeField] Image cooldownOverlay = null;

        // CACHE
        ActionStore store;
        CooldownStore cooldownStore;

        // LIFECYCLE METHODS
        private void Awake()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            store = player.GetComponent<ActionStore>();
            cooldownStore = player.GetComponent<CooldownStore>();
            store.storeUpdated += UpdateIcon;
        }

        private void Update() {
            cooldownOverlay.fillAmount = cooldownStore.GetFractionRemaining(GetItem());
        }

        // PUBLIC

        public void AddItems(InventoryItem item, int number)
        {
            store.AddAction(item, index, number);
        }

        public InventoryItem GetItem()
        {
            return store.GetAction(index);
        }

        public int GetNumber()
        {
            return store.GetNumber(index);
        }

        public int MaxAcceptable(InventoryItem item)
        {
            return store.MaxAcceptable(item, index);
        }

        public void RemoveItems(int number)
        {
            store.RemoveItems(index, number);
        }

        // PRIVATE

        void UpdateIcon()
        {
            icon.SetItem(GetItem(), GetNumber());
        }
    }
}
