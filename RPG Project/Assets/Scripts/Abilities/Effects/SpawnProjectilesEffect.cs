using System;
using System.Collections;
using RPG.Abilities.Helpers;
using RPG.Attributes;
using RPG.Combat;
using RPG.Core;
using UnityEngine;

namespace RPG.Abilities.Effects
{
    [CreateAssetMenu(fileName = "Spawn Projectiles", menuName = "Abilities/Effects/Spawn Projectiles", order = 0)]
    public class SpawnProjectilesEffect : EffectStrategy
    {
        [SerializeField] float spawnDelay = 0;
        [SerializeField] float damage = 1;
        [SerializeField] Projectile projectilePrefab = null;
        [SerializeField] bool useRightHand = true;

        public override IAction MakeAction(TargetingData data, Action complete)
        {
            return new CoroutineAction(data.GetCoroutineOwner(), Effect(data, complete));
        }

        private IEnumerator Effect(TargetingData data, Action complete)
        {
            yield return new WaitForSeconds(spawnDelay);
            var fighter = data.GetSource().GetComponent<Fighter>();
            float calculatedDamage = damage * data.GetEffectScaling();
            var targets = data.GetTargets();
            if (targets == null)
            {
                Projectile.Launch(projectilePrefab, fighter.GetHandTransform(useRightHand).position, data.GetTargetPoint(), data.GetSource(), calculatedDamage);
                complete();
                yield break;
            }
            foreach (var target in targets)
            {
                var health = target.GetComponent<Health>();
                Projectile.Launch(projectilePrefab, fighter.GetHandTransform(useRightHand).position, health, data.GetSource(), calculatedDamage);
            }
            complete();
        }
    }
}
