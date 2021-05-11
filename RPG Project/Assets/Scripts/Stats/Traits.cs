using System;
using System.Collections.Generic;
using GameDevTV.Saving;
using UnityEngine;

namespace RPG.Stats
{
    public class Traits : MonoBehaviour, ISaveable, IModifierProvider
    {
        [SerializeField] BonusPair[] bonusConfig;
        [System.Serializable]
        private class BonusPair
        {
            public Trait trait;
            public Stat stat;
            public float amount;
        }

        Dictionary<Trait, int> assignedPoints = new Dictionary<Trait, int>();
        Dictionary<Trait, int> stagedPoints = new Dictionary<Trait, int>();

        Dictionary<Stat, Dictionary<Trait, float>> bonusLookup;

        private void Awake() {
            bonusLookup = new Dictionary<Stat, Dictionary<Trait, float>>();
            foreach (BonusPair bonus in bonusConfig)
            {
                if (!bonusLookup.ContainsKey(bonus.stat))
                {
                    bonusLookup[bonus.stat] = new Dictionary<Trait, float>();
                }
                bonusLookup[bonus.stat][bonus.trait] = bonus.amount;
            }
        }

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

        public bool AssignPoints(Trait trait, int points)
        {
            if (points > GetUnassigned()) return false;
            if (GetStagedPoints(trait) + points < 0) return false;

            stagedPoints[trait] = GetStagedPoints(trait) + points;
            return true;
        }

        public int GetUnassigned()
        {
            return GetTotalPointsForLevel() - GetTotalProposedPoints();
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

        private int GetTotalProposedPoints()
        {
            int total = 0;
            foreach (var points in assignedPoints.Values)
            {
                total += points;
            }
            foreach (var points in stagedPoints.Values)
            {
                total += points;
            }
            return total;
        }

        private int GetTotalPointsForLevel()
        {
            return (int)GetComponent<BaseStats>().GetStat(Stat.TotalTraitPoints);
        }

        public object CaptureState()
        {
            return assignedPoints;
        }

        public void RestoreState(object state)
        {
            // assignedPoints = new Dictionary<Trait, int>((IDictionary<Trait, int>)state);
        }

        public IEnumerable<float> GetAdditiveModifiers(Stat stat)
        {
            yield break;
        }

        public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            if (bonusLookup.ContainsKey(stat))
            {
                var traitLookup = bonusLookup[stat];
                foreach (var trait in traitLookup.Keys)
                {
                    yield return traitLookup[trait] * GetPoints(trait);   
                }
            }
        }
    }
}