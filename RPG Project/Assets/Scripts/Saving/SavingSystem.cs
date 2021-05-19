using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Linq;
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
            JObject state = LoadFile(saveFile);
            IDictionary<string, JToken> stateDict = state;
            int buildIndex = SceneManager.GetActiveScene().buildIndex;
            if (stateDict.ContainsKey("lastSceneBuildIndex"))
            {
                buildIndex = (int)stateDict["lastSceneBuildIndex"];
            }
            yield return SceneManager.LoadSceneAsync(buildIndex);
            RestoreState(state);
        }

        /// <summary>
        /// Save the current scene to the provided save file.
        /// </summary>
        public void Save(string saveFile)
        {
            JObject state = LoadFile(saveFile);
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

        public IEnumerable<string> ListSaves()
        {
            foreach (string path in Directory.EnumerateFiles(Application.persistentDataPath))
            {
                if (Path.GetExtension(path) == ".json")
                {
                    yield return Path.GetFileNameWithoutExtension(path);
                }
            }
        }

        // PRIVATE

        private JObject LoadFile(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            if (!File.Exists(path))
            {
                return new JObject();
            }

            // using (var file = File.OpenRead(path))
            // {
            //     using (var reader = new BsonReader(file))
            //     {
            using (var textReader = File.OpenText(path))
            {
                using (var reader = new JsonTextReader(textReader))
                {
                    reader.FloatParseHandling = FloatParseHandling.Double;

                    return JObject.Load(reader);
                }
            }


            //using (FileStream stream = File.Open(path, FileMode.Open))
            //{
            //    BinaryFormatter formatter = new BinaryFormatter();
            //    return (Dictionary<string, object>)formatter.Deserialize(stream);
            //}
        }

        private void SaveFile(string saveFile, JObject state)
        {
            string path = GetPathFromSaveFile(saveFile);
            print("Saving to " + path);

            // using (var file = File.Open(path, FileMode.Create))
            // {
            //     using (var writer = new BsonWriter(file))
            //     {
            using (var textWriter = File.CreateText(path))
            {
                using (var writer = new JsonTextWriter(textWriter))
                {
                    writer.Formatting = Formatting.Indented;
                    state.WriteTo(writer);
                }
            }
            //using (FileStream stream = File.Open(path, FileMode.Create))
            //{
            //    BinaryFormatter formatter = new BinaryFormatter();
            //    formatter.Serialize(stream, state);
            //}
        }

        private void CaptureState(JObject state)
        {
            IDictionary<string, JToken> stateDict = state;
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                stateDict[saveable.GetUniqueIdentifier()] = saveable.CaptureState();
            }

            stateDict["lastSceneBuildIndex"] = SceneManager.GetActiveScene().buildIndex;
        }

        private void RestoreState(JObject state)
        {
            IDictionary<string, JToken> stateDict = state;
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                string id = saveable.GetUniqueIdentifier();
                if (stateDict.ContainsKey(id))
                {
                    saveable.RestoreState(stateDict[id]);
                }
            }
        }

        private string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + ".json");
        }
    }
}