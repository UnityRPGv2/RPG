using GameDevTV.Inventories;
using System;
using UnityEngine;

namespace RPG.Crafting
{
    [CreateAssetMenu(menuName = "Crafting / Recipe", order = 0)]
    public class Recipe : ScriptableObject
    {
        [SerializeField] CraftingItem[] ingredients;
        [SerializeField] CraftingItem resultingItem;
        [SerializeField] float craftDuration = 1f;

        public CraftingItem[] GetIngredients()
        {
            return ingredients;
        }
        public CraftingItem GetResult()
        {
            return resultingItem;
        }
        public float GetCraftDuration()
        {
            return craftDuration;
        }
    }

    [Serializable]
    public class CraftingItem
    {
        public InventoryItem Item;
        public int Amount = 1;

        /// <summary>
        /// Will return the resulting item's display name, and append the number
        /// of resulting items that will be crafted if the amount is greater than 1
        /// </summary>
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
