using UnityEngine;

namespace RPG.Saving
{
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

    }
}