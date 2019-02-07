using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {

        NavMeshAgent navMeshAgent;
        Animator animator;
        [SerializeField] float animatorSpeed = 2f;

        private void Start() {
            navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.updatePosition = false;
            animator = GetComponent<Animator>();
            animator.speed = animatorSpeed;
        }

        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }

            UpdateAnimator();

            // Move navmesh cylinder back to our location.
            navMeshAgent.nextPosition = transform.position;
        }

        private void MoveToCursor()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit);
            if (hasHit)
            {
                navMeshAgent.destination = hit.point;
            }
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            animator.SetFloat("forwardSpeed", (speed/animatorSpeed));
        }

        private void OnAnimatorMove()
        {
            var position = animator.rootPosition;
            position.y = navMeshAgent.nextPosition.y;
            transform.position = position;
        }
    }
}
