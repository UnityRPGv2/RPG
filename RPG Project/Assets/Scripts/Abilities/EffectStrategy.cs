using System;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Abilities
{
    public abstract class EffectStrategy : ScriptableObject {
        public abstract void StartEffect(TargetingData data, Action complete);
    }
}