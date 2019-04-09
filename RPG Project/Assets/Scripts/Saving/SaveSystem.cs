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
                byte[] messageBytes = Encoding.UTF8.GetBytes("Hello world");
                stream.Write(messageBytes, 0, messageBytes.Length);
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
                //CHALLENGE convert to text:
                print(Encoding.UTF8.GetString(buffer));
            }
        }

        private string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }
    }
}