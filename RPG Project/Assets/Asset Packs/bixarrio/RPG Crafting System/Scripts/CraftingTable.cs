using GameDevTV.Inventories;
using RPG.Control;
using System;
using UnityEngine;

namespace RPG.Crafting
{
    public class CraftingTable : MonoBehaviour, IRaycastable
    {
        // Event that will fire when the crafting table is interacted with
        public static event Action<Recipe[]> CraftingActivated;

        // All the discovered recipes in the system
        private Recipe[] recipes;

        private void Awake()
        {
            // Finds all the recipes in the system. Assumes recipes are under /Resources/Recipes/
            recipes = Resources.LoadAll<Recipe>("Recipes");
        }

        public CursorType GetCursorType()
        {
            // Return the 'crafting' cursor type
            return CursorType.Crafting;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            // If there are no discovered recipes, return false
            if (recipes == null || recipes.Length == 0)
            {
                return false;
            }

            // If the player presseed the left mouse button, fire the event
            if (Input.GetMouseButtonDown(0))
            {
                CraftingActivated?.Invoke(recipes);
            }

            return true;
        }

        // A static helper that checks if the player has all the
        // required ingredients of a recipe in their inventory
        public static bool CanCraftRecipe(Recipe recipe)
        {
            // If we receive no recipe, return false
            if (recipe == null)
            {
                return false;
            }

            // Get the player inventory
            var playerInventory = Inventory.GetPlayerInventory();
            // If we found no inventory, return false
            if (playerInventory == null)
            {
                return false;
            }
            
            // Go through each ingredient and check the player's inventory for that ingredient
            foreach (var craftingItem in recipe.GetIngredients())
            {
                var hasIngredient = playerInventory.HasItem(craftingItem.Item, out int amountInInventory);
                // If the player does not have the ingredient, return false
                if (!hasIngredient)
                {
                    return false;
                }
                // If the player does not have enough of an ingredient, return false
                if (amountInInventory < craftingItem.Amount)
                {
                    return false;
                }
            }

            // If we got to here, the player has all the ingredients required. return true
            return true;
        }
    }
}
