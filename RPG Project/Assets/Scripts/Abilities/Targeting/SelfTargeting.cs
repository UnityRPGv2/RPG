using System;
using RPG.Core;
using UnityEngine;

namespace RPG.Abilities.Targeting
{
    [CreateAssetMenu(fileName = "SelfTargeting", menuName = "Abilities/Targeting/Self", order = 0)]
    public class SelfTargeting : TargetingStrategy
    {
        public override IAction MakeAction(TargetingData data, Action<TargetingData> callback)
        {
            return new LambdaAction(() => {
                data.SetTarget(data.GetSource().transform.position);
                data.SetTargets(new GameObject[] { data.GetSource() });
                callback(data);
            });
        }
    }
}
