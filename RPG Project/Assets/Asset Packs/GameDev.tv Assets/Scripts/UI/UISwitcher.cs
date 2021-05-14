using UnityEngine;
    
namespace GameDevTV.UI
{
    public class UISwitcher : MonoBehaviour {
        [SerializeField] GameObject entryPoint;

        private void Start() {
            Display(entryPoint);
        }

        public void Display(GameObject childToDisplay)
        {
            if (childToDisplay.transform.parent != transform) return;

            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(child.gameObject == childToDisplay);
            }
        }
    }
}