using System.Collections;
using GameDevTV.Saving;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string autoSaveFile = "autosave";
        const string manualSaveFile = "manualsave";

        [SerializeField] float fadeInTime = 0.2f;
        
        private void Awake() 
        {
            StartCoroutine(LoadLastScene());
        }

        private IEnumerator LoadLastScene() {
            yield return GetComponent<SavingSystem>().LoadLastScene(manualSaveFile);
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();
            yield return fader.FadeIn(fadeInTime);
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.S))
            {
                ManualSave();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                LoadManualSave();
            }
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                Delete();
            }
        }

        public void LoadManualSave()
        {
            GetComponent<SavingSystem>().Load(manualSaveFile);
        }

        public void ManualSave()
        {
            GetComponent<SavingSystem>().Save(manualSaveFile);
        }

        public void LoadAutoSave()
        {
            GetComponent<SavingSystem>().Load(autoSaveFile);
        }

        public void AutoSave()
        {
            GetComponent<SavingSystem>().Save(autoSaveFile);
        }

        public void Delete()
        {
            GetComponent<SavingSystem>().Delete(autoSaveFile);
        }
    }
}