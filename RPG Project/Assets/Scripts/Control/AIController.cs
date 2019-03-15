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
        [SerializeField] float waypointProximity = 1f;
        [SerializeField] float dwellTime = 5f;

        Fighter fighter;
        Mover mover;
        Health health;

        Vector3 originalStation;
        float timeSinceLastSawPlayer = 1000f;
        int nextWaypointIndex = 0;
        float timeAtCurrentWaypoint = 0f;

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
                SuspicionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }

            timeSinceLastSawPlayer += Time.deltaTime;
            timeAtCurrentWaypoint += Time.deltaTime;
        }

        private void SuspicionBehaviour()
        {
            // Cancel all other actions while we wait.
            GetComponent<ActionScheduler>().StartAction(null);
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
            Vector3 nextLocation = originalStation;

            if (waypointContainer != null && IsAtCurrentWaypoint())
            {
                nextWaypointIndex = waypointContainer.GetNextIndex(nextWaypointIndex);
                timeAtCurrentWaypoint = 0;
            }

            if (waypointContainer != null)
            {
                nextLocation = waypointContainer.GetWaypoint(nextWaypointIndex);
            }

            if (timeAtCurrentWaypoint > dwellTime)
            {
                mover.StartMoveAction(nextLocation);
            }
        }

        private bool IsAtCurrentWaypoint()
        {
            Vector3 waypoint = waypointContainer.GetWaypoint(nextWaypointIndex);
            return Vector3.Distance(transform.position, waypoint) < waypointProximity;
        }

        private static GameObject GetPlayer()
        {
            return GameObject.FindWithTag("Player");
        }

        // Called from engine
        private void OnDrawGizmos() {
            Gizmos.color = new Color(0, 0, 255, .5f);
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
            if (waypointContainer != null)
                Gizmos.DrawSphere(waypointContainer.GetWaypoint(nextWaypointIndex), 2);
        }
    }
}