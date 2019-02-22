using System;
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
        [SerializeField] float rotationRate = 2f;

        float currentSpeed = 0;

        private void Start() {
            navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.updatePosition = false;
            navMeshAgent.updateRotation = false;
            animator = GetComponent<Animator>();
            animator.speed = animatorSpeed;
        }

        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }

            ApplyAcceleration();
            ApplyRotation();

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

        private void ApplyAcceleration()
        {
            // Again using this as is before acceleration (see NavMeshAgent gizmos)
            Vector3 velocity = navMeshAgent.desiredVelocity;
            currentSpeed = Mathf.MoveTowards(currentSpeed, velocity.magnitude, navMeshAgent.acceleration * Time.deltaTime);

        }

        private void ApplyRotation()
        {
            // Use desired because we are doing our own rotation acceleration.
            Vector3 velocity = navMeshAgent.desiredVelocity;
            // Stop rotation when we stop moving.
            if (Mathf.Approximately(velocity.magnitude, 0)) return;

            // Formula for exponential decay
            float fractionThisFrame = 1 - Mathf.Exp(-rotationRate * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(velocity), fractionThisFrame);
        }

        private void UpdateAnimator()
        {
            animator.SetFloat("forwardSpeed", currentSpeed);
        }

        private void OnAnimatorMove()
        {
            var position = animator.rootPosition;
            position.y = navMeshAgent.nextPosition.y;
            transform.position = position;
        }
    }
}
