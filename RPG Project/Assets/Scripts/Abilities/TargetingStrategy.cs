using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities
{
    public abstract class TargetingStrategy : ScriptableObject {
        public abstract void StartTargeting(TargetingData data, Action<TargetingData> callback);
    }
}