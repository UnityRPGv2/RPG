using UnityEngine;

namespace RPG.Saving
{
    public class SaveTriggerer : MonoBehaviour
    {
        const string defaultSaveFile = "save";

        private void Update() {
            if (Input.GetKeyDown(KeyCode.S))
            {
                GetComponent<SaveSystem>().Save(defaultSaveFile);
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                GetComponent<SaveSystem>().Load(defaultSaveFile);
            }
        }
    }
}