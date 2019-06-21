using System;
using RPG.Core;
using RPG.Saving;
using UnityEngine;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float regenerationPercentage = 70;


        float maxHealthPoints;
        float healthPoints = -1f;

        bool isDead = false;

        public event Action onDie;

        public void Init(float maxHealthPoints)
        {
            this.maxHealthPoints = maxHealthPoints;
            if (healthPoints < 0)
            {
                healthPoints = maxHealthPoints;
            }
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            print(gameObject.name + " took damage: " + damage);

            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if(healthPoints == 0)
            {
                Die();
            }
        }

        public float GetHealthPoints()
        {
            return healthPoints;
        }

        public float GetMaxHealthPoints()
        {
            return maxHealthPoints;
        }

        public float GetPercentage()
        {
            return 100 * (healthPoints / maxHealthPoints);
        }

        private void Die()
        {
            if (isDead) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public void UpdateMaxHealth(float newMaxHealth)
        {
            maxHealthPoints = newMaxHealth;
            float regenHealthPoints = maxHealthPoints * (regenerationPercentage / 100);
            healthPoints = Mathf.Max(healthPoints, regenHealthPoints);
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float) state;
            
            if (healthPoints <= 0)
            {
                Die();
            }
        }
    }
}