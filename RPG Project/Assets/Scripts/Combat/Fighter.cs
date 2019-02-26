using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        Transform combatTarget;

        Mover mover;

        private void Start() {
            mover = GetComponent<Mover>();
        }

        private void Update() {
            if (combatTarget)
            {
                mover.MoveTo(combatTarget.position);                
            }
        }

        public void Attack(CombatTarget target)
        {
            combatTarget = target.transform;
        }
    }
}