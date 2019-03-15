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

        Fighter fighter;
        Mover mover;
        Health health;

        Vector3 positionBeforeAttack;

        void Start()
        {
            fighter = GetComponent<Fighter>();
            mover = GetComponent<Mover>();
            health = GetComponent<Health>();

            positionBeforeAttack = transform.position;
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
            else
            {
                PatrolBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            GameObject player = GetPlayer();
            if (fighter.IsAttacking(player)) return;

            positionBeforeAttack = transform.position;
            fighter.Attack(player);
        }

        private void PatrolBehaviour()
        {
            mover.StartMoveAction(positionBeforeAttack);
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