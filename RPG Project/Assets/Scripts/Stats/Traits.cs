using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class Traits : MonoBehaviour
    {
        [SerializeField] int availablePoints = 10;

        Dictionary<Trait, int> assignedPoints = new Dictionary<Trait, int>();

        public int GetPoints(Trait trait)
        {
            return assignedPoints.ContainsKey(trait) ? assignedPoints[trait] : 0;
        }

        public int GetAvailablePoints()
        {
            return availablePoints;
        }

        public bool AssignPoints(Trait trait, int points)
        {
            if (points > availablePoints) return false;
            if (GetPoints(trait) + points < 0) return false;

            assignedPoints[trait] = GetPoints(trait) + points;
            availablePoints -= points;
            return true;
        }

        public int GetUnassigned()
        {
            return availablePoints;
        }
    }
}