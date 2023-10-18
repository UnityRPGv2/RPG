using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RPG.Crafting.UI
{
    // UI for one recipe
    public class RecipeUI : MonoBehaviour
    {
        // An event that will fire if a recipe is selected
        public static event Action<Recipe> RecipeSelected;

        // An image to hold the resulting item's icon
        [SerializeField] Image recipeIcon;
        // A text field to hold the resulting item's name
        [SerializeField] TextMeshProUGUI recipeName;

        // A reference to the recipe
        private Recipe recipe;

        // Set up the recipe
        public void Setup(Recipe recipe)
        {
            // Keep a reference to the recipe
            this.recipe = recipe;
            // Refresh the UI
            RefreshUI();
        }

        // Hooked to the UI button
        public void OnSelect()
        {
            // Visually select the game object
            EventSystem.current.SetSelectedGameObject(gameObject);
            // Fire the event when a recipe is selected
            RecipeSelected?.Invoke(recipe);
        }

        // Refresh the UI
        private void RefreshUI()
        {
            // Get the resulting item (the wrapper)
            var resultingItem = recipe.GetResult();
            // Set the recipe icon
            recipeIcon.sprite = resultingItem.Item.GetIcon();
            // Set the recipe name - will include the amount if it's more than 1
            recipeName.text = resultingItem.GetRecipeName();
        }
    }
}
