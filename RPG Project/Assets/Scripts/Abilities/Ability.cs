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

        public override void Use(GameObject user)
        {
            if (targeting != null)
            {
                var targetingData = new TargetingData(effectScale, user);
                targeting.StartTargeting(targetingData, TargetAquired);
            }
        }

        private void TargetAquired(TargetingData data)
        {
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
