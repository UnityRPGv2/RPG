using UnityEngine;

namespace RPG.UI
{
    public class PauseMenuUI : MonoBehaviour {
        private void OnEnable() {
            Time.timeScale = 0;
        }

        private void OnDisable() {
            Time.timeScale = 1;
        }
    }
}