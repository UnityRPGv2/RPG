using System;
using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;

namespace RPG.Abilities
{
    [CreateAssetMenu(fileName = "MyAbility", menuName = "Abilities/Ability", order = 0)]
    public class Ability : ActionItem
    {
        [Header("Ability")]
        [SerializeField] TargetingStrategy targeting;
        [SerializeField] FilterStrategy[] filters;
        [SerializeField] EffectStrategy[] effects;
        [SerializeField] float effectScale = 1;
        [SerializeField] float cooldown = 0;

        public override void Use(GameObject user)
        {
            var cooldownStore = user.GetComponent<CooldownStore>();
            if (cooldownStore && cooldownStore.GetTimeRemaining(this) > 0)
            {
                return;
            }

            if (targeting != null)
            {
                var targetingData = new TargetingData(effectScale, user);
                targeting.StartTargeting(targetingData, TargetAquired);
            }
        }

        private void TargetAquired(TargetingData data)
        {
            var cooldownStore = data.GetSource().GetComponent<CooldownStore>();
            if (cooldownStore)
            {
                cooldownStore.StartCooldown(this, cooldown);
            }

            foreach (var filter in filters)
            {
                data.SetTargets(filter.Filter(data.GetTargets()));
            }
            foreach (var effect in effects)
            {
                effect.StartEffect(data, null);
            }
        }
    }
}
