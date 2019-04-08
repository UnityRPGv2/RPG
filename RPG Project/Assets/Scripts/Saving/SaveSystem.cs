using System.IO;
using UnityEngine;

namespace RPG.Saving
{
    public class SaveSystem : MonoBehaviour
    {
        public void Save(string saveFile)
        {
            print("Save: " + GetPathFromSaveFile(saveFile));
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