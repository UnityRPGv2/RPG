using System;
using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using RPG.Attributes;
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
        [SerializeField] float manaCost = 0;
        [SerializeField] string animatorTrigger = null;
        [SerializeField] bool turnToTarget = false;

        public override void Use(GameObject user)
        {
            if (manaCost > 0)
            {
                var mana = user.GetComponent<Mana>();
                if (mana.GetMana() < manaCost) return;
            }
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
            if (manaCost > 0)
            {
                var mana = data.GetSource().GetComponent<Mana>();
                if (!mana.UseMana(manaCost)) return;
            }

            var cooldownStore = data.GetSource().GetComponent<CooldownStore>();
            if (cooldownStore)
            {
                cooldownStore.StartCooldown(this, cooldown);
            }

            foreach (var filter in filters)
            {
                data.SetTargets(filter.Filter(data.GetTargets()));
            }

            var animator = data.GetSource().GetComponent<Animator>();
            animator.SetTrigger(animatorTrigger);
            if (turnToTarget)
            {
                data.GetSource().transform.LookAt(data.GetTargetPoint());
            }

            foreach (var effect in effects)
            {
                effect.StartEffect(data, null);
            }
        }
    }
}
