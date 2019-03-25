using System.Collections;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        static Fader instance;

        private void Awake() {
            if (instance == null)
            {
                instance = this;
                gameObject.transform.parent = null;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(this);
            }


        }

        public static IEnumerator FadeOut(float duration)
        {
            return null;
        }

        public static IEnumerator FadeIn(float duration)
        {
            return null;
        }
    }
}