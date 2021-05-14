using System.Collections;
using System.Collections.Generic;
using GameDevTV.Saving;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string latestSaveFileKey = "latestSaveFile";
        const string autoSaveFile = "autosave";
        const string manualSaveFile = "manualsave";

        [SerializeField] float fadeInTime = 0.2f;
        
        public void ContinueGame() 
        {
            LoadSave(GetLatestSave());
        }

        public void NewGame()
        {
            StartCoroutine(LoadFirstScene());
        }

        public void LoadSave(string saveFile)
        {
            StartCoroutine(LoadScene(saveFile));
        }

        public void OpenMenu()
        {
            StartCoroutine(LoadMenu());
        }

        public string GetLatestSave()
        {
            return PlayerPrefs.GetString(latestSaveFileKey, null);
        }

        public IEnumerable<string> GetAllSaves()
        {
            return GetComponent<SavingSystem>().ListSaves();
        }

        public void ManualSave()
        {
            var saveFile = $"{manualSaveFile} {System.DateTime.Now.ToString()}";
            saveFile = saveFile.Replace("/","-").Replace(":", "");
            GetComponent<SavingSystem>().Save(saveFile);
            PlayerPrefs.SetString(latestSaveFileKey, saveFile);
            PlayerPrefs.Save();
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

        public void LoadAutoSave()
        {
            GetComponent<SavingSystem>().Load(autoSaveFile);
        }

        private IEnumerator LoadScene(string saveFile)
        {
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeInTime);
            yield return GetComponent<SavingSystem>().LoadLastScene(saveFile);
            yield return fader.FadeIn(fadeInTime);
        }

        private IEnumerator LoadFirstScene()
        {
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeInTime);
            yield return SceneManager.LoadSceneAsync(1);
            yield return fader.FadeIn(fadeInTime);
        }

        private IEnumerator LoadMenu()
        {
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeInTime);
            yield return SceneManager.LoadSceneAsync(0);
            yield return fader.FadeIn(fadeInTime);
        }
    }
}