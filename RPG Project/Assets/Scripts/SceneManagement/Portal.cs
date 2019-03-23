using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] int destinationScene = -1;

        private void OnTriggerEnter(Collider other) {

            //Challenge
            if (other.tag == "Player")
            {
                SceneManager.LoadScene(destinationScene);
            }
        }
    }
}