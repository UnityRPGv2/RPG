using GameDevTV.Inventories;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Crafting.UI
{
    // The UI for one ingredient
    public class IngredientUI : MonoBehaviour
    {
        // An image to hold the icon for this ingredient
        [SerializeField] Image ingredientIcon;
        // A text field to hold the name of this ingredient
        [SerializeField] TextMeshProUGUI ingredientName;
        // A text field to hold the required amount for this ingredient
        [SerializeField] TextMeshProUGUI ingredientAmount;

        // A reference to the ingredient
        private CraftingItem ingredient;

        // Set up the ingredient
        public void Setup(CraftingItem ingredient)
        {
            // Keep a reference to this ingredient
            this.ingredient = ingredient;

            // Set the ingredient icon
            ingredientIcon.sprite = ingredient.Item.GetIcon();
            // Set the ingredient name text
            ingredientName.text = ingredient.Item.GetDisplayName();
            // Set the ingredient amount text
            ingredientAmount.text = ingredient.Amount.ToString();

            // Stop any coroutines
            StopAllCoroutines();
            // If the player has the ingredient, we are done
            if (PlayerHasIngredient(ingredient))
            {
                return;
            }
            // If the player does not have the ingredient, start
            // a coroutine to flash the ingredient amount
            StartCoroutine(FlashAmountRoutine());
        }

        // Check if the player has the required amount of this specific ingredient
        private bool PlayerHasIngredient(CraftingItem ingredient)
        {
            // Get the player inventory
            var playerInventory = Inventory.GetPlayerInventory();
            // If we found no inventory, return false
            if (playerInventory == null)
            {
                return false;
            }

            var hasIngredient = playerInventory.HasItem(ingredient.Item, out int amountInInventory);
            // If the player does not have the ingredient, return false
            if (!hasIngredient)
            {
                return false;
            }
            // If the player does not have enough of an ingredient, return false
            if (amountInInventory < ingredient.Amount)
            {
                return false;
            }
            // If we got to here, the player has enough of the ingredient. return true
            return true;
        }

        // Routine to flash the amount text red if the player does not have enough of this ingredient
        private IEnumerator FlashAmountRoutine()
        {
            // Flag to toggle between red and the default color
            var isOn = false;
            // Get the current text color
            var defaultColor = ingredientAmount.color;
            // A yield instruction to wait for .25 of a second
            var waitSome = new WaitForSeconds(0.25f);
            // Forever
            while (true)
            {
                // Set the color based on the toggle
                ingredientAmount.color = isOn ? Color.red : defaultColor;
                // flip the toggle
                isOn = !isOn;
                // Wait a bit
                yield return waitSome;
            }
        }
    }
}