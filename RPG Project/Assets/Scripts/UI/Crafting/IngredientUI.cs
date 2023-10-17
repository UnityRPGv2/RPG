using GameDevTV.Inventories;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Crafting.UI
{
    public class IngredientUI : MonoBehaviour
    {
        [SerializeField] Image ingredientIcon;
        [SerializeField] TextMeshProUGUI ingredientName;
        [SerializeField] TextMeshProUGUI ingredientAmount;

        private CraftingItem ingredient;

        public void Setup(CraftingItem ingredient)
        {
            this.ingredient = ingredient;

            ingredientIcon.sprite = ingredient.Item.GetIcon();
            ingredientName.text = ingredient.Item.GetDisplayName();
            ingredientAmount.text = ingredient.Amount.ToString();

            StopAllCoroutines();
            if (PlayerHasIngredient(ingredient))
            {
                return;
            }
            StartCoroutine(FlashAmountRoutine());
        }

        private bool PlayerHasIngredient(CraftingItem ingredient)
        {
            var playerInventory = Inventory.GetPlayerInventory();
            var hasIngredient = playerInventory.HasItem(ingredient.Item, out int amountInInventory);
            if (!hasIngredient)
            {
                return false;
            }
            if (amountInInventory < ingredient.Amount)
            {
                return false;
            }
            return true;
        }

        private IEnumerator FlashAmountRoutine()
        {
            var isOn = false;
            var defaultColor = ingredientAmount.color;
            var waitSome = new WaitForSeconds(0.25f);
            while (true)
            {
                ingredientAmount.color = isOn ? Color.red : defaultColor;
                isOn = !isOn;
                yield return waitSome;
            }
        }
    }
}