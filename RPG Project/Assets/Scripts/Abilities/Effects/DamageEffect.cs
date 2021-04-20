using System;
using System.Collections.Generic;
using RPG.Attributes;
using UnityEngine;

namespace RPG.Abilities.Effects
{
    [CreateAssetMenu(fileName = "DamageEffect", menuName = "Abilities/Effects/Damage", order = 0)]
    public class DamageEffect : EffectStrategy
    {
        [SerializeField] float amount = 1;

        public override void StartEffect(TargetingData data, Action complete)
        {
            foreach (var target in data.GetTargets())
            {
                Health healthComp = target.GetComponent<Health>();
                if (healthComp != null)
                {
                    healthComp.TakeDamage(data.GetSource(), amount * data.GetEffectScaling());
                }
            }
            if (complete != null) complete();
        }
    }
}