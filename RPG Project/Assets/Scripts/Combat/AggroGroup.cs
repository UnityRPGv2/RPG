using UnityEngine;

namespace RPG.Combat
{
    public class AggroGroup : MonoBehaviour
    {
        [SerializeField] Fighter[] groupMembers;

        private void Start() {
            Activate(false);
        }

        public void Activate(bool enabled = true)
        {
            foreach (var fighter in groupMembers)
            {
                var target = fighter.GetComponent<CombatTarget>();
                if (target)
                {
                    target.enabled = enabled;
                }
                fighter.enabled = enabled;
            }
        }
    }
}