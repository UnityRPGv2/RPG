using UnityEngine;

namespace RPG.Stats
{
    [System.Serializable]
    public class Requirement {
        [SerializeField] Trait trait;
        [SerializeField] int minimumTrait = 0;
        [SerializeField] int minimumLevel = 0;

        public bool CheckRequirement(GameObject player)
        {
            if (minimumTrait > 0)
            {
                TraitStore traitStore = player.GetComponent<TraitStore>();
                if (traitStore.GetPoints(trait) < minimumTrait) return false;
            }
            if (minimumLevel > 0)
            {
                BaseStats baseStats = player.GetComponent<BaseStats>();
                if (baseStats.GetLevel() < minimumLevel) return false;
            }

            return true;
        }
    }
}