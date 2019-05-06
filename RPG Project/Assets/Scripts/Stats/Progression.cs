using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "RPG Project/Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionClass[] progressionClasses = null;

        Dictionary<CharacterClass, Dictionary<Stat, float[]>> lookup = null;
        
        public float GetStat(CharacterClass characterClass, Stat stat, int level)
        {
            BuildLookup();

            var classStats = lookup[characterClass];
            if (!classStats.ContainsKey(stat))
            {
                return 0;
            }
            float[] levels = classStats[stat];

            if (levels.Length < level)
            {
                return levels[levels.Length - 1];
            }

            return levels[level - 1];
        }

        private void BuildLookup()
        {
            if (lookup != null) return;

            lookup = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();
            foreach (ProgressionClass progressionClass in progressionClasses)
            {
                lookup[progressionClass.characterClass] = new Dictionary<Stat, float[]>();
                var classLookup = lookup[progressionClass.characterClass];
                foreach (ProgressionStat progressionStat in progressionClass.stats)
                {
                    classLookup[progressionStat.stat] = progressionStat.levels;
                }
            }
        }

        [System.Serializable]
        class ProgressionClass
        {
            public CharacterClass characterClass;
            public ProgressionStat[] stats = null;
        }

        [System.Serializable]
        class ProgressionStat
        {
            public Stat stat;
            public float[] levels = null;
        }
    }
}