using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities
{
    public class TargetingData
    {
        float effectScale;
        GameObject source;
        Vector3 targetPoint;
        IEnumerable<GameObject> targetGameObjects;

        public TargetingData(float effectScale, GameObject source)
        {
            this.effectScale = effectScale;
            this.source = source;
        }

        public void SetTarget(Vector3 target)
        {
            targetPoint = target;
        }

        public Vector3 GetTargetPoint()
        {
            return targetPoint;
        }

        public void SetTargets(IEnumerable<GameObject> targets)
        {
            targetGameObjects = targets;
        }

        public float GetEffectScaling()
        {
            return effectScale;
        }

        public GameObject GetSource()
        {
            return source;
        }

        public IEnumerable<GameObject> GetTargets()
        {
            return targetGameObjects;
        }
    }
}