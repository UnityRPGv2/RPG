using UnityEditor;
using UnityEngine;

namespace RPG.Saving
{
    [ExecuteInEditMode]
    public class SaveableEntity : MonoBehaviour
    {
        [SerializeField] string uniqueIdentifier;

        public string GetUniqueIdentifier()
        {
            return uniqueIdentifier;
        }

        public object CaptureState()
        {
            print("Captured state for " + uniqueIdentifier);
            return null;
        }

        public void RestoreState(object state)
        {
            print("Restored state for " + uniqueIdentifier);

        }

        private void Update() {
            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty serializedProperty = serializedObject.FindProperty("uniqueIdentifier");

            // 2 Challenge:
            if (string.IsNullOrEmpty(serializedProperty.stringValue))
            {
                // 1 Do this
                serializedProperty.stringValue = System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}