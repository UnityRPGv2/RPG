using System;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour {
        [SerializeField] float healthPoints = 100;

        bool isDead = false;

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(0, healthPoints - damage);
            if (healthPoints == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
        }
    }
}