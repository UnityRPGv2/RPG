using GameDevTV.Inventories;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Crafting.UI
{
    public class RecipeDetailsUI : MonoBehaviour
    {
        public static event Action RecipeCrafted;

        [SerializeField] Image recipeIcon;
        [SerializeField] TextMeshProUGUI recipeName;
        [SerializeField] TextMeshProUGUI recipeDescription;
        [SerializeField] Transform ingredientsListContainer;
        [SerializeField] IngredientUI ingredientPrefab;
        [SerializeField] Button craftButton;
        [SerializeField] Image craftProgressImage;
        [SerializeField] GameObject craftProgressContainer;

        private Recipe recipe;

        private void Awake()
        {
            recipeIcon.gameObject.SetActive(false);
            recipeName.gameObject.SetActive(false);
            recipeDescription.gameObject.SetActive(false);
            StopCoroutinesAndCleanUp();
        }

        private void OnEnable()
        {
            RecipeUI.RecipeSelected += OnRecipeSelected;
        }

        private void OnDisable()
        {
            RecipeUI.RecipeSelected -= OnRecipeSelected;
        }

        public void CraftRecipe()
        {
            StopCoroutinesAndCleanUp();
            StartCoroutine(CraftItemRoutine(recipe));
        }

        public void CancelCrafting()
        {
            StopCoroutinesAndCleanUp();
        }

        private void RefreshUI()
        {
            CleanupIngredientsList();

            if (recipe == null)
            {
                return;
            }

            var resultingItem = recipe.GetResult();
            recipeIcon.sprite = resultingItem.Item.GetIcon();
            recipeName.text = resultingItem.GetRecipeName();
            recipeDescription.text = resultingItem.Item.GetDescription();

            craftProgressImage.fillAmount = 0f;
            craftProgressContainer.SetActive(false);

            PopulateIngredientsList(recipe.GetIngredients());

            recipeIcon.gameObject.SetActive(true);
            recipeName.gameObject.SetActive(true);
            recipeDescription.gameObject.SetActive(true);

            craftButton.gameObject.SetActive(true);
            craftButton.interactable = CraftingTable.CanCraftRecipe(recipe);
        }

        private void StopCoroutinesAndCleanUp()
        {
            StopAllCoroutines();

            craftProgressContainer.SetActive(false);
            craftProgressImage.fillAmount = 0f;

            craftButton.gameObject.SetActive(true);
            craftButton.interactable = CraftingTable.CanCraftRecipe(recipe);

            RefreshUI();
        }

        private void CleanupIngredientsList()
        {
            foreach (Transform child in ingredientsListContainer)
            {
                child.SetParent(null);
                Destroy(child.gameObject);
            }
        }

        private void PopulateIngredientsList(CraftingItem[] ingredients)
        {
            CleanupIngredientsList();
            foreach (var ingredient in ingredients)
            {
                var ingredientUI = Instantiate(ingredientPrefab, ingredientsListContainer);
                ingredientUI.Setup(ingredient);
            }
        }

        private void OnRecipeSelected(Recipe recipe)
        {
            this.recipe = recipe;
            StopAllCoroutines();
            RefreshUI();
            gameObject.SetActive(true);
        }

        private IEnumerator CraftItemRoutine(Recipe recipe)
        {
            craftButton.interactable = false;
            craftButton.gameObject.SetActive(false);

            craftProgressImage.fillAmount = 0f;
            craftProgressContainer.SetActive(true);

            var duration = recipe.GetCraftDuration();
            for (var timer = 0f; timer / duration <= 1f; timer += Time.deltaTime)
            {
                craftProgressImage.fillAmount = timer / duration;
                yield return null;
            }

            craftProgressContainer.SetActive(false);
            craftProgressImage.fillAmount = 0f;

            craftButton.gameObject.SetActive(true);
            craftButton.interactable = CraftingTable.CanCraftRecipe(recipe);

            var playerInventory = Inventory.GetPlayerInventory();
            foreach (var ingredient in recipe.GetIngredients())
            {
                playerInventory.RemoveItem(ingredient.Item, ingredient.Amount);
            }
            var resultingItem = recipe.GetResult();
            playerInventory.AddToFirstEmptySlot(resultingItem.Item, resultingItem.Amount);

            RefreshUI();
            RecipeCrafted?.Invoke();
        }
    }
}
