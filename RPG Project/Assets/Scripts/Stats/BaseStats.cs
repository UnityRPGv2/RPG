using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [SerializeField] CharacterClass characterClass;
        [Range(1, 99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] Progression progression = null;
 
        public float GetHealth()
        {
            return progression.GetHealth(characterClass, startingLevel);
        }
    }
}