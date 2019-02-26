using System;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float attackRange = 2;
        [SerializeField] float timeBetweenAttacks = 2;

        Transform combatTarget;
        float timeSinceLastAttack = 100;

        Mover mover;

        private void Start() {
            mover = GetComponent<Mover>();
        }

        private void Update() {
            if (!combatTarget) return;

            // Disable movement to check that cancelling is working both ways
            if (!isInRange)
            {
                mover.MoveTo(combatTarget.position);                
            } 
            else
            {
                mover.Cancel();
                AttackBehaviour();
            }
            // To here

            timeSinceLastAttack += Time.deltaTime;
        }

        public void Attack(CombatTarget target)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            combatTarget = target.transform;
        }

        public void Cancel()
        {
            combatTarget = null;
        }

        private void AttackBehaviour()
        {
            if (timeBetweenAttacks > timeSinceLastAttack) return;
            
            GetComponent<Animator>().SetTrigger("attack");
            timeSinceLastAttack = 0;
        }

        private bool isInRange => Vector3.Distance(transform.position, combatTarget.position) < attackRange;
    }
}