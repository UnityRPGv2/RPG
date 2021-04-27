using System;
using System.Collections.Generic;
using RPG.Control;
using UnityEngine;

namespace RPG.Abilities.Targeting 
{
    [CreateAssetMenu(fileName = "UnderCursorTargeting", menuName = "Abilities/Targeting/Under Cursor", order = 0)]
    public class UnderCursorTargeting : TargetingStrategy
    {
        [SerializeField] LayerMask castingLayer;
        [SerializeField] float groundOffset = 1;

        public override void StartTargeting(TargetingData data, Action<TargetingData> callback)
        {
            RaycastHit mouseHit;
            Ray ray = PlayerController.GetMouseRay();
            if (Physics.Raycast(ray, out mouseHit, 100, castingLayer))
            {
                data.SetTarget(mouseHit.point + groundOffset / ray.direction.y * ray.direction);
            }
            callback(data);
        }
    }
}