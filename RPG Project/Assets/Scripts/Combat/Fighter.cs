using System;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float attackDamage = 5f;
        [SerializeField] float timeBetweenAttacks = 2;

        Transform target; 
        float timeSinceLastAttack = 100;

        private void Update()
        {
            if (target == null) return;

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }

            timeSinceLastAttack += Time.deltaTime;
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }
        private void AttackBehaviour()
        {
            if (timeBetweenAttacks > timeSinceLastAttack) return;
            
            GetComponent<Animator>().SetTrigger("attack");
            timeSinceLastAttack = 0;
        }

        private void Hit()
        {
            if (target == null) return;
            Health healthComponent = target.GetComponent<Health>();
            if (healthComponent == null) return;
            healthComponent.TakeDamage(attackDamage);
        }
    }
}