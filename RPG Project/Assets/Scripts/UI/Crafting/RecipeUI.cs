using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RPG.Crafting.UI
{
    public class RecipeUI : MonoBehaviour
    {
        public static event Action<Recipe> RecipeSelected;

        [SerializeField] Image recipeIcon;
        [SerializeField] TextMeshProUGUI recipeName;

        private Recipe recipe;

        public void Setup(Recipe recipe)
        {
            this.recipe = recipe;
            RefreshUI();
        }

        public void OnSelect()
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
            RecipeSelected?.Invoke(recipe);
        }

        private void RefreshUI()
        {
            var resultingItem = recipe.GetResult();
            recipeIcon.sprite = resultingItem.Item.GetIcon();
            recipeName.text = resultingItem.GetRecipeName();
        }
    }
}
