using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class TraitStore : MonoBehaviour
    {
        Dictionary<Trait, int> assignedPoints = new Dictionary<Trait, int>();

        int unassignedPoints = 10;

        public int GetPoints(Trait trait)
        {
            return assignedPoints.ContainsKey(trait) ? assignedPoints[trait] : 0;
        }

        public void AssignPoints(Trait trait, int points)
        {
            if (!CanAssignPoints(trait, points)) return;

            assignedPoints[trait] = GetPoints(trait) + points;
            unassignedPoints -= points;
        }

        public bool CanAssignPoints(Trait trait, int points)
        {
            if (GetPoints(trait) + points < 0) return false;
            if (unassignedPoints < points) return false;
            return true;
        }

        public int GetUnassignedPoints()
        {
            return unassignedPoints;
        }
    }
}