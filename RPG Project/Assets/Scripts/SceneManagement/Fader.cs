using System.Collections;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        public void FadeOutImmediate()
        {
            GetComponent<CanvasGroup>().alpha = 1;
        }

        public IEnumerator FadeOut(float time)
        {
            var canvasGroup = GetComponent<CanvasGroup>();
            while (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime / time;
                yield return null;
            }
        }

        public IEnumerator FadeIn(float time)
        {
            var canvasGroup = GetComponent<CanvasGroup>();
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime / time;
                yield return null;
            }
        }
    }
}