using RPG.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class TraitUI : MonoBehaviour
    {
        [SerializeField] Trait trait;
        [SerializeField] TextMeshProUGUI valueText;
        [SerializeField] Button minusButton;
        [SerializeField] Button plusButton;

        int value = 0;

        private void Start() {
            minusButton.onClick.AddListener(() => Assign(-1));
            plusButton.onClick.AddListener(() => Assign(1));
        }
        
        private void Update() {
            minusButton.interactable = value > 0;

            valueText.text = value.ToString();

        }

        public void Assign(int amount)
        {
            value += amount;
            if (value <= 0)
            {
                value = 0;
            }
        }
    }
}