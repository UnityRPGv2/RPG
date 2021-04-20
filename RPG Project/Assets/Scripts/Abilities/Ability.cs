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

        public override void Use(GameObject user)
        {
            if (targeting != null)
            {
                targeting.StartTargeting(user, (t) => TargetAquired(user, t));
            }
        }

        private void TargetAquired(GameObject user, IEnumerable<GameObject> targets)
        {
            foreach (var filter in filters)
            {
                targets = filter.Filter(targets);
            }
            foreach (var effect in effects)
            {
                effect.StartEffect(user, targets, null);
            }
        }
    }
}
