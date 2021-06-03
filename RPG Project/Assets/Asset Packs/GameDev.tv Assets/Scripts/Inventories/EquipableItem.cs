using UnityEngine;
using RPG.Stats;

namespace GameDevTV.Inventories
{
    /// <summary>
    /// An inventory item that can be equipped to the player. Weapons could be a
    /// subclass of this.
    /// </summary>
    [CreateAssetMenu(menuName = ("GameDevTV/GameDevTV.UI.InventorySystem/Equipable Item"))]
    public class EquipableItem : InventoryItem
    {
        // CONFIG DATA
        [Tooltip("Where are we allowed to put this item.")]
        [SerializeField] EquipLocation allowedEquipLocation = EquipLocation.Weapon;
        [SerializeField] Requirement requirement;

        // PUBLIC


        public bool CanEquip(GameObject player, EquipLocation equipLocation)
        {
            return equipLocation == allowedEquipLocation && requirement.CheckRequirement(player);
        }
    }
}