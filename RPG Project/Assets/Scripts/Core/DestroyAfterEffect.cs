using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class DestroyAfterEffect : MonoBehaviour
    {
        [SerializeField] GameObject optionalTarget = null;

        void Update()
        {
            if (!GetComponent<ParticleSystem>().IsAlive())
            {
                if (optionalTarget == null)
                {
                    Destroy(gameObject);
                }
                else
                {
                    Destroy(optionalTarget);
                }
            }
        }
    }
}