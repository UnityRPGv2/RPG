using System;
using System.Collections.Generic;
using UnityEngine;
using GameDevTV.Saving;
using RPG.Core;

namespace GameDevTV.Inventories
{
    /// <summary>
    /// Provides a store for the items equipped to a player. Items are stored by
    /// their equip locations.
    /// 
    /// This component should be placed on the GameObject tagged "Player".
    /// </summary>
    public class Equipment : MonoBehaviour, ISaveable, IPredicateEvaluator
    {
        // STATE
        Dictionary<EquipLocation, EquipableItem> equippedItems = new Dictionary<EquipLocation, EquipableItem>();

        // PUBLIC

        private void Start() {
            AddItem(EquipLocation.Weapon, (EquipableItem)EquipableItem.GetFromID("77ad666c-513c-4c88-87b2-c8594bcd5a89"));
            // AddItem(EquipLocation.Shield, (EquipableItem)EquipableItem.GetFromID("803520ee-c7fc-4ab3-8aa1-0c14a9647cf7"));
        }

        /// <summary>
        /// Broadcasts when the items in the slots are added/removed.
        /// </summary>
        public event Action equipmentUpdated;

        /// <summary>
        /// Return the item in the given equip location.
        /// </summary>
        public EquipableItem GetItemInSlot(EquipLocation equipLocation)
        {
            if (!equippedItems.ContainsKey(equipLocation))
            {
                return null;
            }

            return equippedItems[equipLocation];
        }

        /// <summary>
        /// Add an item to the given equip location. Do not attempt to equip to
        /// an incompatible slot.
        /// </summary>
        public void AddItem(EquipLocation slot, EquipableItem item)
        {
            Debug.Assert(item.CanEquip(gameObject, slot));

            equippedItems[slot] = item;

            if (equipmentUpdated != null)
            {
                equipmentUpdated();
            }
        }

        /// <summary>
        /// Remove the item for the given slot.
        /// </summary>
        public void RemoveItem(EquipLocation slot)
        {
            equippedItems.Remove(slot);
            if (equipmentUpdated != null)
            {
                equipmentUpdated();
            }
        }

        /// <summary>
        /// Enumerate through all the slots that currently contain items.
        /// </summary>
        public IEnumerable<EquipLocation> GetAllPopulatedSlots()
        {
            return equippedItems.Keys;
        }

        // PRIVATE

        object ISaveable.CaptureState()
        {
            var equippedItemsForSerialization = new Dictionary<EquipLocation, string>();
            foreach (var pair in equippedItems)
            {
                equippedItemsForSerialization[pair.Key] = pair.Value.GetItemID();
            }
            return equippedItemsForSerialization;
        }

        void ISaveable.RestoreState(object state)
        {
            equippedItems = new Dictionary<EquipLocation, EquipableItem>();

            var equippedItemsForSerialization = (Dictionary<EquipLocation, string>)state;

            foreach (var pair in equippedItemsForSerialization)
            {
                var item = (EquipableItem)InventoryItem.GetFromID(pair.Value);
                if (item != null)
                {
                    equippedItems[pair.Key] = item;
                }
            }
            equipmentUpdated?.Invoke();
        }

        public bool? Evaluate(string predicate, string[] parameters)
        {
            if (predicate == "HasEquipmentItem")
            {
                foreach (var item in equippedItems.Values)
                {
                    if (item.GetItemID() == parameters[0]) return true;
                }
                return false;
            }

            return null;
        }
    }
}