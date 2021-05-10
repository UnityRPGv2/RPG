using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class Traits : MonoBehaviour
    {
        [SerializeField] int availablePoints = 10;

        Dictionary<Trait, int> assignedPoints = new Dictionary<Trait, int>();
        Dictionary<Trait, int> stagedPoints = new Dictionary<Trait, int>();

        public int GetProposedPoints(Trait trait)
        {
            return GetPoints(trait) + GetStagedPoints(trait);
        }

        public int GetPoints(Trait trait)
        {
            return assignedPoints.ContainsKey(trait) ? assignedPoints[trait] : 0;
        }

        public int GetStagedPoints(Trait trait)
        {
            return stagedPoints.ContainsKey(trait) ? stagedPoints[trait] : 0;
        }

        public int GetAvailablePoints()
        {
            return availablePoints;
        }

        public bool AssignPoints(Trait trait, int points)
        {
            if (points > availablePoints) return false;
            if (GetStagedPoints(trait) + points < 0) return false;

            stagedPoints[trait] = GetStagedPoints(trait) + points;
            availablePoints -= points;
            return true;
        }

        public int GetUnassigned()
        {
            return availablePoints;
        }

        public void Commit()
        {
            List<Trait> stagedTraits = new List<Trait>(stagedPoints.Keys);
            foreach (Trait trait in stagedTraits)
            {
                assignedPoints[trait] = GetProposedPoints(trait);
                stagedPoints.Remove(trait);
            }
        }
    }
}