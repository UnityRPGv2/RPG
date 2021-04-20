using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities.Filters
{
    [CreateAssetMenu(fileName = "TagFilter", menuName = "Abilities/Filter/Tag Based", order = 0)]
    public class TagFilter : FilterStrategy
    {
        [SerializeField] string tagToInclude;

        public override IEnumerable<GameObject> Filter(IEnumerable<GameObject> objects)
        {
            foreach (GameObject gameObject in objects)
            {
                if (gameObject.CompareTag(tagToInclude))
                {
                    yield return gameObject;
                }
            }
        }
    }
}