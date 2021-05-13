using System.Collections;
using GameDevTV.Saving;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string latestSaveFileKey = "latestSaveFile";
        const string autoSaveFile = "autosave";
        const string manualSaveFile = "manualsave";

        [SerializeField] float fadeInTime = 0.2f;
        
        private void Awake() 
        {
            StartCoroutine(LoadLastScene());
        }

        private IEnumerator LoadLastScene() {
            yield return GetComponent<SavingSystem>().LoadLastScene(GetLatestSave());
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
                foreach (var item in GetComponent<SavingSystem>().ListSaves())
                {
                    print(item);
                }
            }
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                Delete();
            }
        }

        public string GetLatestSave()
        {
            return PlayerPrefs.GetString(latestSaveFileKey, null);
        }

        public void LoadManualSave()
        {
            GetComponent<SavingSystem>().Load(manualSaveFile);
        }

        public void ManualSave()
        {
            var saveFile = $"{manualSaveFile} {System.DateTime.Now.ToString()}";
            saveFile = saveFile.Replace("/","-").Replace(":", "");
            GetComponent<SavingSystem>().Save(saveFile);
            PlayerPrefs.SetString(latestSaveFileKey, saveFile);
            PlayerPrefs.Save();
        }

        public void LoadAutoSave()
        {
            GetComponent<SavingSystem>().Load(autoSaveFile);
        }

        public void AutoSave()
        {
            GetComponent<SavingSystem>().Save(autoSaveFile);
            PlayerPrefs.SetString(latestSaveFileKey, autoSaveFile);
            PlayerPrefs.Save();
        }

        public void Delete()
        {
            GetComponent<SavingSystem>().Delete(autoSaveFile);
        }
    }
}