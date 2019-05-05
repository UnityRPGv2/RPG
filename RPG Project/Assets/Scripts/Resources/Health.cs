using RPG.Saving;
using UnityEngine;
using RPG.Core;
using RPG.Stats;
using System;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float healthPoints = 100f;

        bool isDead = false;

        private void Start() {
            healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if(healthPoints == 0)
            {
                Die();
                AwardXP(instigator);
            }
        }

        private void AwardXP(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience != null)
            {
                experience.GainPoints(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
            }
        }
		
        public float GetPercentage()
        {
            return Mathf.Clamp01(healthPoints / GetComponent<BaseStats>().GetStat(Stat.Health));
        }
		
        private void Die()
        {
            if (isDead) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
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