using System;
using UnityEngine;

namespace RPG.Abilities.Targeting
{
    [CreateAssetMenu(fileName = "SelfTargeting", menuName = "Abilities/Targeting/Self", order = 0)]
    public class SelfTargeting : TargetingStrategy
    {
        public override void StartTargeting(TargetingData data, Action<TargetingData> callback)
        {
            data.SetTarget(data.GetSource().transform.position);
            data.SetTargets(new GameObject[]{data.GetSource()});
            callback(data);
        }
    }
}
