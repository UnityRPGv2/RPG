using RPG.Saving;
using UnityEngine;
using RPG.Core;
using RPG.Stats;
using System;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float damagePoints = 0;

        bool isDead = false;

        private void Update() {
            if (ShouldDie())
            {
                Die();
            }
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            Debug.Log("Damage: " + damage + " taken by: " + gameObject.name);
            damagePoints += damage;
            if (ShouldDie())
            {
                AwardXP(instigator);
            }
        }

        private bool ShouldDie()
        {
            return damagePoints >= GetComponent<BaseStats>().GetStat(Stat.Health);
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
            return 1 - Mathf.Clamp01(damagePoints / GetComponent<BaseStats>().GetStat(Stat.Health));
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
            return damagePoints;
        }

        public void RestoreState(object state)
        {
            damagePoints = (float) state;
        }
    }
}