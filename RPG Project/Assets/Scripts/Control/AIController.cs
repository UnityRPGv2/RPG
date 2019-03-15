using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 2;
        [SerializeField] WaypointContainer waypointContainer;
        [SerializeField] float suspicionTime = 3;

        Fighter fighter;
        Mover mover;
        Health health;

        Vector3 originalStation;
        [SerializeField] float timeSinceLastSawPlayer = 0;

        void Start()
        {
            fighter = GetComponent<Fighter>();
            mover = GetComponent<Mover>();
            health = GetComponent<Health>();

            originalStation = transform.position;
        }

        void Update()
        {
            if (health.IsDead())
            {
                return;
            }

            GameObject player = GetPlayer();
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer < chaseDistance && fighter.CanAttack(player))
            {
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                // Cancel all other actions while we wait.
                GetComponent<ActionScheduler>().StartAction(null);
            }
            else
            {
                PatrolBehaviour();
            }

            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void AttackBehaviour()
        {
            timeSinceLastSawPlayer = 0;

            GameObject player = GetPlayer();
            if (fighter.IsAttacking(player)) return;

            fighter.Attack(player);
        }

        private void PatrolBehaviour()
        {
            mover.StartMoveAction(originalStation);
        }

        private static GameObject GetPlayer()
        {
            return GameObject.FindWithTag("Player");
        }

        // Called from engine
        private void OnDrawGizmos() {
            Gizmos.color = new Color(0, 0, 255, .5f);
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}