using System;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

namespace RPG.Abilities.Targeting 
{
    [CreateAssetMenu(fileName = "TargetingDemo", menuName = "Abilities/Targeting/Demo", order = 0)]
    public class DemoTargeting : TargetingStrategy
    {
        public override IAction MakeAction(TargetingData data, Action<TargetingData> callback)
        {
            return new LambdaAction(() => {
                Debug.Log("Demo Targetting");
                callback(data);
            });
        }
    }
}