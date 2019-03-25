using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] int destinationScene = -1;
        [SerializeField] Transform spawnPoint;

        private void OnTriggerEnter(Collider other) 
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            DontDestroyOnLoad(gameObject);
            yield return SceneManager.LoadSceneAsync(destinationScene);
            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal.spawnPoint);
            Destroy(gameObject);
        }

        private Portal GetOtherPortal()
        {
            //Challenge
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal != this)
                {
                    return portal;
                }
            }

            return null;
        }

        private static void UpdatePlayer(Transform spawnTransform)
        {
            //Challenge
            GameObject player = GameObject.FindWithTag("Player");
            player.transform.position = spawnTransform.position;
            player.transform.rotation = spawnTransform.rotation;
        }
    }
}