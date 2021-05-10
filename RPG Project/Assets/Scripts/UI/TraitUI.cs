using RPG.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class TraitUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI unassignedPointsText;
        [SerializeField] Button commitButton;
        
        Traits playerTraits = null;

        private void Awake()
        {
            playerTraits = GameObject.FindGameObjectWithTag("Player").GetComponent<Traits>();
            commitButton.onClick.AddListener(playerTraits.Commit);
        }


        private void Update() {
            unassignedPointsText.text = playerTraits.GetUnassigned().ToString();
        }
    }
}