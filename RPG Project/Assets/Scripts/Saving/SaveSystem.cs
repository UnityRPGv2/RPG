using System;
using System.IO;
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
                Transform player = GetPlayerTransform();
                byte[] buffer = ToBytes(player.position);
                stream.Write(buffer, 0, buffer.Length);
            }
        }

        public void Load(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            print("Load: " + path);
            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                // CHALLENGE:
                Transform player = GetPlayerTransform();
                player.position = FromBytes(buffer);
            }
        }

        private byte[] ToBytes(Vector3 vector)
        {
            byte[] vectorBytes = new byte[4*3];
            BitConverter.GetBytes(vector.x).CopyTo(vectorBytes, 0);
            BitConverter.GetBytes(vector.y).CopyTo(vectorBytes, 4);
            BitConverter.GetBytes(vector.z).CopyTo(vectorBytes, 8);
            return vectorBytes;
        }

        private Vector3 FromBytes(byte[] bytes)
        {
            Vector3 result = new Vector3();
            result.x = BitConverter.ToSingle(bytes, 0);
            //CHALLENGE:
            result.y = BitConverter.ToSingle(bytes, 4);
            result.z = BitConverter.ToSingle(bytes, 8);
            return result;
        }

        private Transform GetPlayerTransform()
        {
            //Mini challenge
            return GameObject.FindWithTag("Player").transform;
        }

        private string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }
    }
}