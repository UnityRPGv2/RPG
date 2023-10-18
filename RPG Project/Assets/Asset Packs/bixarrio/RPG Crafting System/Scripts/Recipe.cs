using GameDevTV.Inventories;
using System;
using UnityEngine;

namespace RPG.Crafting
{
    [CreateAssetMenu(menuName = "Crafting / Recipe", order = 0)]
    public class Recipe : ScriptableObject
    {
        // A list of all the required ingredients
        [SerializeField] CraftingItem[] ingredients;
        // The resulting item
        [SerializeField] CraftingItem resultingItem;
        // How long it will take (in seconds) to craft this item
        [SerializeField] float craftDuration = 1f;

        // A getter to return the ingredients
        public CraftingItem[] GetIngredients()
        {
            return ingredients;
        }
        // A getter to return the resulting item
        public CraftingItem GetResult()
        {
            return resultingItem;
        }
        // A getter to return the crafting duration
        public float GetCraftDuration()
        {
            return craftDuration;
        }
    }

    [Serializable]
    // A wrapper to hold an inventory item and amount
    public class CraftingItem
    {
        // The inventory item
        public InventoryItem Item;
        // The amount
        public int Amount = 1;

        // Will return the resulting item's display name, and append the number
        // of resulting items that will be crafted if the amount is greater than 1
        // eg.
        //     Hunting Bow
        //     Flaming Arrow x20
        public string GetRecipeName()
        {
            var recipeName = Item.GetDisplayName();
            if (Amount > 1)
            {
                recipeName = $"{recipeName} x{Amount}";
            }
            return recipeName;
        }
    }
}
