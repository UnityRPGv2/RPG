using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities.Targeting 
{
    [CreateAssetMenu(fileName = "TargetingDemo", menuName = "Abilities/Targeting/Demo", order = 0)]
    public class DemoTargeting : TargetingStrategy
    {
        public override void StartTargeting(TargetingData data, Action<TargetingData> callback)
        {
            Debug.Log("Demo!");
        }
    }
}