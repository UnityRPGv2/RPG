using RPG.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class LoadSaveUI : MonoBehaviour {
        [SerializeField] Transform loadListRoot;
        [SerializeField] GameObject saveRowPrefab;

        private void OnEnable() {
            foreach (Transform child in loadListRoot)
            {
                Destroy(child.gameObject);
            }
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
            foreach (string saveFile in savingWrapper.GetAllSaves())
            {
                GameObject saveRow = Instantiate<GameObject>(saveRowPrefab, loadListRoot);
                saveRow.GetComponentInChildren<TextMeshProUGUI>().text = saveFile;
                saveRow.GetComponentInChildren<Button>().onClick.AddListener(() => {
                    savingWrapper.LoadSave(saveFile);
                });
            }
        }
    }
}