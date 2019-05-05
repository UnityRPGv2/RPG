using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "RPG Project/Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionClass[] progressionClasses = null;
        
        public float GetHealth(CharacterClass characterClass, int level)
        {
            foreach (ProgressionClass progressionClass in progressionClasses)
            {
                if (progressionClass.characterClass != characterClass) continue;

                return progressionClass.health[level];
            }

            return 0;
        }

        [System.Serializable]
        class ProgressionClass
        {
            public CharacterClass characterClass;
            public float[] health = null;
        }
    }
}