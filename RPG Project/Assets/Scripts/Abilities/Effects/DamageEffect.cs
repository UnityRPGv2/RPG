using System;
using System.Collections.Generic;
using RPG.Attributes;
using UnityEngine;

namespace RPG.Abilities.Effects
{
    [CreateAssetMenu(fileName = "DamageEffect", menuName = "Abilities/Effects/Damage", order = 0)]
    public class DamageEffect : EffectStrategy
    {
        [SerializeField] float amount;

        public override void StartEffect(GameObject source, IEnumerable<GameObject> targets, Action complete)
        {
            foreach (var target in targets)
            {
                Health healthComp = target.GetComponent<Health>();
                if (healthComp != null)
                {
                    healthComp.TakeDamage(source, amount);
                }
            }
            if (complete != null) complete();
        }
    }
}