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

        [SerializeField] float fadeInTime = 0.2f;
        
        public void ContinueGame() 
        {
            StartCoroutine(LoadScene(GetCurrentSave()));
        }

        public void NewGame(string saveFile)
        {
            SetCurrentSave(saveFile);
            StartCoroutine(LoadFirstScene());
        }

        public void LoadGame(string saveFile)
        {
            SetCurrentSave(saveFile);
            StartCoroutine(LoadScene(saveFile));
        }

        public void OpenMenu()
        {
            StartCoroutine(LoadMenu());
        }

        public string GetCurrentSave()
        {
            return PlayerPrefs.GetString(latestSaveFileKey, null);
        }

        public void SetCurrentSave(string saveFile)
        {
            PlayerPrefs.SetString(latestSaveFileKey, saveFile);
            PlayerPrefs.Save();
        }

        public IEnumerable<string> GetAllSaves()
        {
            return GetComponent<SavingSystem>().ListSaves();
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(GetCurrentSave());
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(GetCurrentSave());
        }

        private IEnumerator LoadScene(string saveFile)
        {
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeInTime);
            yield return GetComponent<SavingSystem>().LoadLastScene(saveFile, 1);
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