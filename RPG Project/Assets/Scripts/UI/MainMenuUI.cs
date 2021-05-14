using GameDevTV.Utils;
using RPG.SceneManagement;
using UnityEngine;

namespace RPG.UI
{
    public class MainMenuUI : MonoBehaviour {

        LazyValue<SavingWrapper> savingWrapper;

        private void Awake() {
            savingWrapper = new LazyValue<SavingWrapper>(() => FindObjectOfType<SavingWrapper>());
        }

        public void Continue()
        {
            savingWrapper.value.ContinueGame();
        }

        public void New()
        {

        }
    }
}