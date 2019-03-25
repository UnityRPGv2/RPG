using System;
using System.Collections;
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
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            //Mini-challenge: what if do destroy?
            DontDestroyOnLoad(gameObject);
            yield return SceneManager.LoadSceneAsync(destinationScene);
            print("Scene Loaded");
            Destroy(gameObject);
        }
    }
}