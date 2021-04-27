using System;
using System.Collections;
using RPG.Attributes;
using RPG.Combat;
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

        public override void StartEffect(TargetingData data, Action complete)
        {
            CooldownStore store = data.GetSource().GetComponent<CooldownStore>();
            store.StartCoroutine(Effect(data, complete));
        }

        private IEnumerator Effect(TargetingData data, Action complete)
        {
            yield return new WaitForSeconds(spawnDelay);
            var fighter = data.GetSource().GetComponent<Fighter>();
            float calculatedDamage = damage * data.GetEffectScaling();
            foreach (var target in data.GetTargets())
            {
                var health = target.GetComponent<Health>();
                Projectile.Launch(projectilePrefab, fighter.GetHandTransform(useRightHand).position, health, data.GetSource(), calculatedDamage);
            }
        }
    }
}
