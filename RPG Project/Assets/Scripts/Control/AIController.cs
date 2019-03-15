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

        Fighter fighter;
        Mover mover;
        Health health;

        Vector3 originalPosition;

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
                fighter.Attack(player);
            }
            else
            {
                mover.StartMoveAction(originalPosition);
            }
        }

        // Called from engine
        private void OnDrawGizmos() {
            Gizmos.color = new Color(0, 0, 255, .5f);
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}