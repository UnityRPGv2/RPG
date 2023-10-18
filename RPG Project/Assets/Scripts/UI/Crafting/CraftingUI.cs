using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Crafting.UI
{
    // This is the complete Crafting UI
    public class CraftingUI : MonoBehaviour
    {
        // The container that will hold all the recipes
        [SerializeField] Transform recipesListContainer;
        // The prefab that represents each recipe
        [SerializeField] RecipeUI recipePrefab;

        // All the recipes in the list
        private List<RecipeUI> recipesInList = new List<RecipeUI>();

        private void Awake()
        {
            // Disable the carfting ui
            gameObject.SetActive(false);

            // Remove all children from the recipe list container
            CleanupRecipesList();
            // Hook to the event that will let us know when a crafting table has been interacted with
            CraftingTable.CraftingActivated += OnCraftingActivated;
        }

        private void OnDestroy()
        {
            // Unhook the event
            CraftingTable.CraftingActivated -= OnCraftingActivated;
        }

        private void CleanupRecipesList()
        {
            // Destroy all the recipe items we know of
            foreach (var recipe in recipesInList)
            {
                recipe.transform.SetParent(null);
                Destroy(recipe.gameObject);
            }
            // clear the list
            recipesInList.Clear();

            // If there are no more items in the container, we are done
            if (recipesListContainer.childCount == 0)
            {
                return;
            }

            // If we still have children, destroy those too
            foreach(Transform child in recipesListContainer)
            {
                child.SetParent(null);
                Destroy(child.gameObject);
            }
        }

        private void PopulateRecipesList(Recipe[] recipes)
        {
            // Remove all children from the recipe list container
            CleanupRecipesList();
            // Go through each recipe, create it's representation and add it to the list
            foreach (var recipe in recipes)
            {
                var recipeUI = Instantiate(recipePrefab, recipesListContainer);
                recipeUI.Setup(recipe);
                recipesInList.Add(recipeUI);
            }
        }

        // The event handler that is executed when a crafting table is interacted with
        private void OnCraftingActivated(Recipe[] recipes)
        {
            // Populate the recipes list
            PopulateRecipesList(recipes);
            // Show the UI
            gameObject.SetActive(true);
        }
    }
}
