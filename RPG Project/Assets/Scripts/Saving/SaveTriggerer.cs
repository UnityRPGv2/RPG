using UnityEngine;

namespace RPG.Saving
{
    public class SaveTriggerer : MonoBehaviour
    {
        const string defaultSaveFile = "save";

        private void Start() {
            Load();
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
        }

        public void Load()
        {
            GetComponent<SaveSystem>().Load(defaultSaveFile);
        }

        public void Save()
        {
            GetComponent<SaveSystem>().Save(defaultSaveFile);
        }
    }
}