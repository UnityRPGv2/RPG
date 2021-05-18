using UnityEngine;

namespace RPG.UI
{
    public class SaveLoadUI : MonoBehaviour
    {
        [SerializeField] Transform contentRoot;
        [SerializeField] GameObject buttonPrefab;

        private void OnEnable()
        {
            print("Loading Saves...");
            foreach (Transform child in contentRoot)
            {
                Destroy(child.gameObject);
            }
            Instantiate(buttonPrefab, contentRoot);
            Instantiate(buttonPrefab, contentRoot);
            Instantiate(buttonPrefab, contentRoot);
            Instantiate(buttonPrefab, contentRoot);
        }    
    }
}