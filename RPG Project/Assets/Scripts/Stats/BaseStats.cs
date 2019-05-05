using RPG.Resources;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [SerializeField] CharacterClass characterClass;
        [Range(1, 99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] Progression progression = null;
 
        private void Update() {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log(gameObject.name + ": " + GetLevel());
            }
        }

        public float GetStat(Stat stat)
        {
            return progression.GetStat(characterClass, stat, GetLevel());
        }

        private int GetLevel()
        {
            Experience exp = GetComponent<Experience>();
            if (exp == null)
            {
                return startingLevel;
            }
            
            int level = 1;
            while (progression.GetStat(characterClass, Stat.ExperienceToLevelUp, level) <= exp.GetExperience() && level < 100)
            {
                level++;
            }

            return level;
        }
    }
}