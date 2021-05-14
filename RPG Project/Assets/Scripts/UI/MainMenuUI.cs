using GameDevTV.Utils;
using RPG.SceneManagement;
using UnityEngine;
using TMPro;

namespace RPG.UI
{
    public class MainMenuUI : MonoBehaviour {

        LazyValue<SavingWrapper> savingWrapper;

        [SerializeField] TMP_InputField newGameName;

        private void Awake() {
            savingWrapper = new LazyValue<SavingWrapper>(() => FindObjectOfType<SavingWrapper>());
        }

        public void Continue()
        {
            savingWrapper.value.ContinueGame();
        }

        public void New()
        {
            savingWrapper.value.NewGame(newGameName.text);
        }
    }
}