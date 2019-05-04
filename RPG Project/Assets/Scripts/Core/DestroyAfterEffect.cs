using UnityEngine;

namespace RPG.Core
{
    public class DestroyAfterEffect : MonoBehaviour
    {
        [SerializeField] GameObject target = null;

        private void Update()
        {
            if (!GetComponent<ParticleSystem>().IsAlive())
            {
                Destroy(target);
            }
        }
    }
}