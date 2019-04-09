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
            FileStream stream = File.Open(path, FileMode.Create);
            // Challenge: write hello world;
            // stream.WriteByte(102);
            byte[] messageBytes = Encoding.UTF8.GetBytes("Hello world");
            stream.Write(messageBytes, 0, messageBytes.Length);
            stream.Close();
        }

        public void Load(string saveFile)
        {
            print("Load: " + GetPathFromSaveFile(saveFile));
        }

        private string GetPathFromSaveFile(string saveFile)
        {
            // CHALLENGE: show docs and ask to work out.
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }
    }
}