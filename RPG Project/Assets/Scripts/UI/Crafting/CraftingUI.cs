using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Crafting.UI
{
    public class CraftingUI : MonoBehaviour
    {
        [SerializeField] Transform recipesListContainer;
        [SerializeField] RecipeUI recipePrefab;

        private List<RecipeUI> recipesInList = new List<RecipeUI>();

        private void Awake()
        {
            gameObject.SetActive(false);

            CleanupRecipesList();
            CraftingTable.CraftingActivated += OnCraftingActivated;
        }

        private void OnDestroy()
        {
            CraftingTable.CraftingActivated -= OnCraftingActivated;
        }

        private void CleanupRecipesList()
        {
            foreach (var recipe in recipesInList)
            {
                recipe.transform.SetParent(null);
                Destroy(recipe.gameObject);
            }
            recipesInList.Clear();

            if (recipesListContainer.childCount == 0)
            {
                return;
            }

            foreach(Transform child in recipesListContainer)
            {
                child.SetParent(null);
                Destroy(child.gameObject);
            }
        }

        private void PopulateRecipesList(Recipe[] recipes)
        {
            CleanupRecipesList();
            foreach (var recipe in recipes)
            {
                var recipeUI = Instantiate(recipePrefab, recipesListContainer);
                recipeUI.Setup(recipe);
                recipesInList.Add(recipeUI);
            }
        }

        private void OnCraftingActivated(Recipe[] recipes)
        {
            PopulateRecipesList(recipes);
            gameObject.SetActive(true);
        }
    }
}
