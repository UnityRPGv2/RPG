using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] float attackRange = 2;

        Transform combatTarget;

        Mover mover;

        private void Start() {
            mover = GetComponent<Mover>();
        }

        private void Update() {
            if (combatTarget && !isInRange)
            {
                mover.MoveTo(combatTarget.position);                
            } 
            else
            {
                mover.Stop();
            }
        }

        public void Attack(CombatTarget target)
        {
            combatTarget = target.transform;
        }

        private bool isInRange => Vector3.Distance(transform.position, combatTarget.position) < attackRange;
    }
}