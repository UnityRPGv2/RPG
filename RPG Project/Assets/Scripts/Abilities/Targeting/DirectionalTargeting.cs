using System;
using RPG.Control;
using RPG.Core;
using UnityEngine;

namespace RPG.Abilities.Targeting
{
    [CreateAssetMenu(fileName = "Directional Targeting", menuName = "Abilities/Targeting/Directional", order = 0)]
    public class DirectionalTargeting : TargetingStrategy
    {
        [SerializeField] LayerMask castingLayer;
        [SerializeField] float groundOffset = 1;

        public override IAction MakeAction(TargetingData data, Action<TargetingData> callback)
        {
            return new LambdaAction(() =>
            {
                RaycastHit mouseHit;
                Ray ray = PlayerController.GetMouseRay();
                if (Physics.Raycast(ray, out mouseHit, 100, castingLayer))
                {
                    // Projected back towards camera until groundOffset off ground.
                    data.SetTarget(mouseHit.point + groundOffset / ray.direction.y * ray.direction);
                }
                callback(data);
            });
        }
    }
}
