using GameDevTV.Inventories;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Crafting.UI
{
    // Details of the recipe. Also does the crafting
    public class RecipeDetailsUI : MonoBehaviour
    {
        // An event that will fire when a recipe has been crafted
        public static event Action RecipeCrafted;

        // An image to hold the resulting item's icon
        [SerializeField] Image recipeIcon;
        // A texct field to hold the resulting item's name (and amount)
        [SerializeField] TextMeshProUGUI recipeName;
        // A text field to hold the resulting item's description
        [SerializeField] TextMeshProUGUI recipeDescription;
        // The container that will hold all the ingredients
        [SerializeField] Transform ingredientsListContainer;
        // The prefab that represents each ingredient
        [SerializeField] IngredientUI ingredientPrefab;
        // The crafting button
        [SerializeField] Button craftButton;
        // A 'filled' image that will serve as the crafting progress bar
        [SerializeField] Image craftProgressImage;
        // The container holding the 'progress' image
        [SerializeField] GameObject craftProgressContainer;

        // A reference to the recipe
        private Recipe recipe;

        private void Awake()
        {
            // Hide the image
            recipeIcon.gameObject.SetActive(false);
            // Hide the name
            recipeName.gameObject.SetActive(false);
            // Hide the description
            recipeDescription.gameObject.SetActive(false);
            // Stop crafting and reset
            StopCoroutinesAndCleanUp();
        }

        private void OnEnable()
        {
            // Hook to the recipe selection event
            RecipeUI.RecipeSelected += OnRecipeSelected;
        }

        private void OnDisable()
        {
            // Unhook from the recipe selection event
            RecipeUI.RecipeSelected -= OnRecipeSelected;
        }

        // Hooked to the UI button
        public void CraftRecipe()
        {
            // Stop crafting and reset
            StopCoroutinesAndCleanUp();
            // Start the crafting
            StartCoroutine(CraftItemRoutine(recipe));
        }

        // Hooked to the progress image (clicking the image will cancel crafting)
        public void CancelCrafting()
        {
            // Stop crafting and reset
            StopCoroutinesAndCleanUp();
        }

        private void RefreshUI()
        {
            // Remove all children from the ingredient list container
            CleanupIngredientsList();

            // Reset the progress image
            craftProgressImage.fillAmount = 0f;
            // Hide the progress image
            craftProgressContainer.SetActive(false);

            // Initially disable the craft button
            craftButton.interactable = false;

            // If we have no recipe, we're done
            if (recipe == null)
            {
                return;
            }

            // Get the resulting item for this recipe
            var resultingItem = recipe.GetResult();
            // Set the resulting item icon
            recipeIcon.sprite = resultingItem.Item.GetIcon();
            // Set the resulting item name (and amount)
            recipeName.text = resultingItem.GetRecipeName();
            // Set the resulting item description
            recipeDescription.text = resultingItem.Item.GetDescription();

            // Populate the ingredients list
            PopulateIngredientsList(recipe.GetIngredients());

            // Show the recipe icon
            recipeIcon.gameObject.SetActive(true);
            // Show the recipe name (and amount)
            recipeName.gameObject.SetActive(true);
            // Show the recipe description
            recipeDescription.gameObject.SetActive(true);

            // Show the craft button
            craftButton.gameObject.SetActive(true);
            // Make the craft button interactable _if_ the player can craft this recipe
            craftButton.interactable = CraftingTable.CanCraftRecipe(recipe);
        }

        private void StopCoroutinesAndCleanUp()
        {
            // Stop any crafting that may currently be happening
            StopAllCoroutines();

            // Refresh the UI
            RefreshUI();
        }

        private void CleanupIngredientsList()
        {
            // Destroy all the child items in the ingredients list
            foreach (Transform child in ingredientsListContainer)
            {
                child.SetParent(null);
                Destroy(child.gameObject);
            }
        }

        private void PopulateIngredientsList(CraftingItem[] ingredients)
        {
            // Remove all children from the ingredient list container
            CleanupIngredientsList();
            // Go through each ingredient, create it's representation and add it to the list
            foreach (var ingredient in ingredients)
            {
                var ingredientUI = Instantiate(ingredientPrefab, ingredientsListContainer);
                ingredientUI.Setup(ingredient);
            }
        }

        // The event handler that is executed when a recipe is selected
        private void OnRecipeSelected(Recipe recipe)
        {
            // Keep a reference to the selected recipe
            this.recipe = recipe;
            // Stop crafting and reset
            StopCoroutinesAndCleanUp();
            // Refresh the UI
            RefreshUI();
            // Make this game object active
            gameObject.SetActive(true);
        }

        // Routine to craft the recipe
        private IEnumerator CraftItemRoutine(Recipe recipe)
        {
            // Disable the craft button
            craftButton.interactable = false;
            // Hide the craft button
            craftButton.gameObject.SetActive(false);

            // Reset the crafting progress image
            craftProgressImage.fillAmount = 0f;
            // Make the progress image visible
            craftProgressContainer.SetActive(true);

            // Get the crafting duration
            var duration = recipe.GetCraftDuration();
            // Loop while we are crafting
            for (var timer = 0f; timer / duration <= 1f; timer += Time.deltaTime)
            {
                // Increase the fill amount of the progress image
                craftProgressImage.fillAmount = timer / duration;
                yield return null;
            }

            // Reset the progress image
            craftProgressImage.fillAmount = 0f;
            // Hide the progress image
            craftProgressContainer.SetActive(false);

            // Show the craft button
            craftButton.gameObject.SetActive(true);
            // Make the craft button interactable _if_ the player can craft this recipe
            craftButton.interactable = CraftingTable.CanCraftRecipe(recipe);

            // Get the player inventory
            var playerInventory = Inventory.GetPlayerInventory();
            // Remove each ingredient from the player's inventory
            foreach (var ingredient in recipe.GetIngredients())
            {
                playerInventory.RemoveItem(ingredient.Item, ingredient.Amount);
            }
            // Get the resulting item
            var resultingItem = recipe.GetResult();
            // Add the resulting item to the player's inventory
            playerInventory.AddToFirstEmptySlot(resultingItem.Item, resultingItem.Amount);

            // Refresh the UI
            RefreshUI();
            // Fire the event
            RecipeCrafted?.Invoke();
        }
    }
}
