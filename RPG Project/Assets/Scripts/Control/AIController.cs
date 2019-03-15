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
        [SerializeField] float suspicionTime = 3;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointProximity = 3;

        Fighter fighter;
        Mover mover;
        Health health;

        Vector3 originalPosition;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        int nextWaypointIndex = 0;

        void Start()
        {
            fighter = GetComponent<Fighter>();
            mover = GetComponent<Mover>();
            health = GetComponent<Health>();

            originalPosition = transform.position;
        }

        void Update()
        {
            if (health.IsDead()) return;

            GameObject player = GameObject.FindWithTag("Player");
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
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = originalPosition;
            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    CycleWaypoint();
                }
                nextPosition = GetWaypoint();
            }

            mover.StartMoveAction(nextPosition);
        }

        private Vector3 GetWaypoint()
        {
            // CHALLENGE
            return patrolPath.GetWaypoint(nextWaypointIndex);
        }

        private void CycleWaypoint()
        {
            // CHALLENGE
            nextWaypointIndex = patrolPath.GetNextIndex(nextWaypointIndex);
        }

        private bool AtWaypoint()
        {
            return Vector3.Distance(transform.position, GetWaypoint()) < waypointProximity;
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelAction();
        }

        private void AttackBehaviour()
        {
            GameObject player = GameObject.FindWithTag("Player");
            timeSinceLastSawPlayer = 0;
            fighter.Attack(player);
        }

        // Called from engine
        private void OnDrawGizmos() {
            Gizmos.color = new Color(0, 0, 255, .5f);
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}