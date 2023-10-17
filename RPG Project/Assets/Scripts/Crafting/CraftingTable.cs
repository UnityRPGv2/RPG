using GameDevTV.Inventories;
using RPG.Control;
using System;
using UnityEngine;

namespace RPG.Crafting
{
    public class CraftingTable : MonoBehaviour, IRaycastable
    {
        public static event Action<Recipe[]> CraftingActivated;

        private Recipe[] recipes;

        private void Awake()
        {
            recipes = Resources.LoadAll<Recipe>("Recipes");
        }

        public CursorType GetCursorType()
        {
            return CursorType.Crafting;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (recipes == null || recipes.Length == 0)
            {
                return false;
            }

            if (Input.GetMouseButtonDown(0))
            {
                CraftingActivated?.Invoke(recipes);
            }

            return true;
        }

        public static bool CanCraftRecipe(Recipe recipe)
        {
            if (recipe == null)
            {
                return false;
            }

            var playerInventory = Inventory.GetPlayerInventory();
            if (playerInventory == null)
            {
                return false;
            }
            
            foreach (var craftingItem in recipe.GetIngredients())
            {
                var hasIngredient = playerInventory.HasItem(craftingItem.Item, out int amountInInventory);
                if (!hasIngredient)
                {
                    return false;
                }
                if (amountInInventory < craftingItem.Amount)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
