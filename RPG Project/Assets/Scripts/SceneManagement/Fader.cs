using System.Collections;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        static Fader instance;

        CanvasGroup canvasGroup;

        private void Awake() {
            if (instance == null)
            {
                instance = this;
                gameObject.transform.parent = null;
                DontDestroyOnLoad(gameObject);
                canvasGroup = GetComponent<CanvasGroup>();
            }
            else
            {
                Destroy(this);
            }


        }

        public static IEnumerator FadeOut(float duration)
        {
            // Challenge
            return instance.Fade(1, duration);
        }

        public static IEnumerator FadeIn(float duration)
        {
            // Challenge
            return instance.Fade(0, duration);
        }

        private IEnumerator Fade(float targetAlpha, float duration)
        {
            float speed = 1 / duration;
            while (!Mathf.Approximately(canvasGroup.alpha, targetAlpha))
            {
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
                yield return null;
            }
        }
    }
}