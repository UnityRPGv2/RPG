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

        public override void Use(GameObject user)
        {

            if (targeting != null)
            {
                targeting.StartTargeting(user, TargetAquired);
            }
        }

        private void TargetAquired(IEnumerable<GameObject> targets)
        {
            foreach (var filter in filters)
            {
                targets = filter.Filter(targets);
            }
            foreach (var target in targets)
            {
                Debug.Log(target);
            }
        }
    }
}
