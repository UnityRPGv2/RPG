using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using MiniJSON;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameDevTV.Saving
{
    /// <summary>
    /// This component provides the interface to the saving system. It provides
    /// methods to save and restore a scene.
    ///
    /// This component should be created once and shared between all subsequent scenes.
    /// </summary>
    public class SavingSystem : MonoBehaviour
    {
        /// <summary>
        /// Will load the last scene that was saved and restore the state. This
        /// must be run as a coroutine.
        /// </summary>
        /// <param name="saveFile">The save file to consult for loading.</param>
        public IEnumerator LoadLastScene(string saveFile)
        {
            Dictionary<string, object> state = LoadFile(saveFile);
            int buildIndex = SceneManager.GetActiveScene().buildIndex;
            if (state.ContainsKey("lastSceneBuildIndex"))
            {
                print(state["lastSceneBuildIndex"].GetType());
                buildIndex = (int)(Int64)state["lastSceneBuildIndex"];
            }
            yield return SceneManager.LoadSceneAsync(buildIndex);
            RestoreState(state);
        }

        /// <summary>
        /// Save the current scene to the provided save file.
        /// </summary>
        public void Save(string saveFile)
        {
            Dictionary<string, object> state = LoadFile(saveFile);
            CaptureState(state);
            SaveFile(saveFile, state);
        }

        /// <summary>
        /// Delete the state in the given save file.
        /// </summary>
        public void Delete(string saveFile)
        {
            File.Delete(GetPathFromSaveFile(saveFile));
        }

        public void Load(string saveFile)
        {
            RestoreState(LoadFile(saveFile));
        }

        // PRIVATE

        private Dictionary<string, object> LoadFile(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            if (!File.Exists(path))
            {
                return new Dictionary<string, object>();
            }
            var json = System.IO.File.ReadAllText(path);
            return (Dictionary<string, object>)Json.Deserialize(json);
        }

        private void SaveFile(string saveFile, Dictionary<string, object> state)
        {
            string path = GetPathFromSaveFile(saveFile);
            print("Saving to " + path);
            var json = Json.Serialize(state);
            System.IO.File.WriteAllText(path, json);
        }

        private void CaptureState(Dictionary<string, object> state)
        {
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                state[saveable.GetUniqueIdentifier()] = saveable.CaptureState();
            }

            state["lastSceneBuildIndex"] = SceneManager.GetActiveScene().buildIndex;
        }

        private void RestoreState(Dictionary<string, object> state)
        {
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                string id = saveable.GetUniqueIdentifier();
                if (state.ContainsKey(id))
                {
                    saveable.RestoreState(state[id]);
                }
            }
        }

        private string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }
    }
}