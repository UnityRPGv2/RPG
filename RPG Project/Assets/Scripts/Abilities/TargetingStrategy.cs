using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities
{
    public abstract class TargetingStrategy : ScriptableObject
    {
        public abstract void StartTargeting(GameObject user, Action<IEnumerable<GameObject>> finished);
    }
}