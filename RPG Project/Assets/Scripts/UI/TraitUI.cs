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

        Traits playerTraits = null;

        private void Awake() {
            playerTraits = GameObject.FindGameObjectWithTag("Player").GetComponent<Traits>();
        }

        private void Start() {
            minusButton.onClick.AddListener(() => Assign(-1));
            plusButton.onClick.AddListener(() => Assign(1));
        }
        
        private void Update()
        {
            minusButton.interactable = playerTraits.GetPoints(trait) > 0;
            plusButton.interactable = playerTraits.GetUnassigned() > 0;

            valueText.text = playerTraits.GetPoints(trait).ToString();
        }

        public void Assign(int amount)
        {
            playerTraits.AssignPoints(trait, amount);
        }
    }
}