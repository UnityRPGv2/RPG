using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 2;

        Fighter fighter;
        Health health;

        State currentState = State.Patrolling;

        enum State
        {
            Attacking,
            Patrolling,
            Dead
        }

        void Start()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
        }

        void Update()
        {
            GameObject player = GetPlayer();
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (health.IsDead())
            {
                Transition(State.Dead);
            }
            else if (distanceToPlayer < chaseDistance && fighter.CanAttack(player))
            {
                Transition(State.Attacking);
            }
            else
            {
                Transition(State.Patrolling);
            }
        }

        private static GameObject GetPlayer()
        {
            return GameObject.FindWithTag("Player");
        }

        void Transition(State nextState)
        {
            if (currentState == nextState) return;

            switch (currentState)
            {
                case State.Attacking:
                    StopAttacking();
                    break;
            }

            switch (nextState)
            {
                case State.Attacking:
                    StartAttacking();
                    break;
            }

            currentState = nextState;
        }

        void StartAttacking()
        {
            fighter.Attack(GetPlayer());
        }

        void StopAttacking()
        {
            fighter.Cancel();
        }

        // Called from engine
        private void OnDrawGizmos() {
            Gizmos.color = new Color(0, 0, 255, .5f);
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}