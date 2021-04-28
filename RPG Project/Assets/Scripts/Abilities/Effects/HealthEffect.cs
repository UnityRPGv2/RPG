using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Abilities.Helpers;
using RPG.Attributes;
using RPG.Control;
using RPG.Core;
using UnityEngine;

namespace RPG.Abilities.Effects
{
    [CreateAssetMenu(fileName = "HealthEffect", menuName = "Abilities/Effects/Health", order = 0)]
    public class HealthEffect : EffectStrategy
    {
        [SerializeField] float amount = 1;
        [SerializeField] int steps = 1;
        [SerializeField] float totalTime = 1;
        [SerializeField] float initialDelay = 0;
        [SerializeField] Transform effectPrefab;

        public override IAction MakeAction(TargetingData data, Action complete)
        {
            return new CoroutineAction(data.GetCoroutineOwner(), Effect(data, complete));
        }

        private IEnumerator Effect(TargetingData data, Action complete)
        {
            yield return new WaitForSeconds(initialDelay);
            SpawnEffect(data.GetTargetPoint());
            for (int i = 0; i < steps; i++)
            {
                DealDamage(data);
                if (i == 0)
                {
                    // Can only cancel up to this point, then damage continues regardless.
                    complete();
                }
                yield return new WaitForSeconds(totalTime / steps);
            }
        }

        private void DealDamage(TargetingData data)
        {
            float damage = amount * data.GetEffectScaling() / steps;
            foreach (var target in data.GetTargets())
            {
                Health healthComp = target.GetComponent<Health>();
                if (healthComp != null)
                {
                    if (damage >= 0)
                    {
                        healthComp.TakeDamage(data.GetSource(), damage);
                    }
                    else
                    {
                        healthComp.Heal(-damage);
                    }
                }
            }
        }

        private void SpawnEffect(Vector3 position)
        {
            var effect = Instantiate(effectPrefab);
            effect.position = position;
        }
    }
}