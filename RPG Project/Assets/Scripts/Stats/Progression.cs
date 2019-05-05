using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "RPG Project/Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionClass[] progressionClasses = null;
        
        public float GetStat(CharacterClass characterClass, Stat stat, int level)
        {
            foreach (ProgressionClass progressionClass in progressionClasses)
            {
                if (progressionClass.characterClass != characterClass) continue;

                foreach (ProgressionStat progressionStat in progressionClass.stats)
                {
                    if (progressionStat.stat != stat) continue;

                    if (progressionStat.levels.Length < level)
                    {
                        return progressionStat.levels[progressionStat.levels.Length - 1];
                    }
                    return progressionStat.levels[level - 1];
                }
            }

            return 0;
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