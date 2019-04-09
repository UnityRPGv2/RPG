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
            using (FileStream stream = File.Open(path, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                Transform player = GetPlayerTransform();
                formatter.Serialize(stream, new SerializableVector3(player.position));
            }
        }

        public void Load(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            print("Load: " + path);
            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                // CHALLENGE: given Deserialize:
                BinaryFormatter formatter = new BinaryFormatter();
                Transform player = GetPlayerTransform();
                SerializableVector3 position = (SerializableVector3)formatter.Deserialize(stream);
                player.position = position.ToVector();
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