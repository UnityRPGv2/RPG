using System;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Abilities
{
    public abstract class EffectStrategy : ScriptableObject {
        public abstract void StartEffect(GameObject source, IEnumerable<GameObject> targets, Action complete);
    }
}