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

        public override void Use(GameObject user)
        {

            if (targeting != null)
            {
                Debug.Log("Targeting");
                targeting.StartTargeting(user, null);
            }
        }
    }
}
