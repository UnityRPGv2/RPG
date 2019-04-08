using UnityEngine;

namespace RPG.Saving
{
    public class SaveSystem : MonoBehaviour
    {
        public void Save(string saveFile)
        {
            print("Save");
        }

        public void Load(string saveFile)
        {
            print("Load");
        }
    }
}