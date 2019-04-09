using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

namespace RPG.Saving
{
    public class SaveSystem : MonoBehaviour
    {
        public void Save(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            print("Save: " + path);
            var state = LoadState(saveFile);
            using (FileStream stream = File.Open(path, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                CaptureState(state);
                formatter.Serialize(stream, state);
            }
        }

        public void Load(string saveFile)
        {
            RestoreState(LoadState(saveFile));
        }

        private Dictionary<string, object> LoadState(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            print("Load: " + path);
            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (Dictionary<string, object>)formatter.Deserialize(stream);
            }
        }

        private void CaptureState(Dictionary<string, object> state)
        {
            var saveables = FindObjectsOfType<SaveableEntity>();
            print(saveables);
            foreach (var saveable in saveables)
            {
                state[saveable.GetUniqueIdentifier()] = saveable.CaptureState();
            }
        }

        private void RestoreState(Dictionary<string, object> state)
        {
            var saveables = FindObjectsOfType<SaveableEntity>();
            foreach (var saveable in saveables)
            {
                saveable.RestoreState(state[saveable.GetUniqueIdentifier()]);
            }
        }

        private Transform GetPlayerTransform()
        {
            return GameObject.FindWithTag("Player").transform;
        }

        private string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }
    }
}