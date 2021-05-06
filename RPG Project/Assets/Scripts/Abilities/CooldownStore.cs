using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities
{
    public class CooldownStore : MonoBehaviour
    {
        Dictionary<Ability, float> cooldownTimers = new Dictionary<Ability, float>();

        void Update() {
            var keys = new List<Ability>(cooldownTimers.Keys);
            foreach (Ability ability in keys)
            {
                cooldownTimers[ability] -= Time.deltaTime;
                if (cooldownTimers[ability] < 0)
                {
                    cooldownTimers.Remove(ability);
                }
            }
        }

        public void StartCooldown(Ability ability, float cooldownTime)
        {
            cooldownTimers[ability] = cooldownTime;
        }

        public float GetTimeRemaining(Ability ability)
        {
            if (!cooldownTimers.ContainsKey(ability))
            {
                return 0;
            }

            return cooldownTimers[ability];
        }
    }
}
