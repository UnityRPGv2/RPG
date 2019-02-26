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
        [SerializeField] float minStoppingDistance = 0.1f;

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
            CalculateStoppingDistance();
            ApplyAcceleration();
            ApplyRotation();

            UpdateAnimator();

            // Move navmesh cylinder back to our location.
            navMeshAgent.nextPosition = transform.position;
        }

        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        public void Stop()
        {
            navMeshAgent.isStopped = true;
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
            // This approach is more stable with dynamic stopping distance
            if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance) return;

            // Formula for exponential decay
            float fractionThisFrame = 1 - Mathf.Exp(-rotationRate * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(velocity), fractionThisFrame);
        }

        private void CalculateStoppingDistance()
        {
            // Autobraking must be on for this to work. TODO: Why?
            float stoppingDistance = StoppingDistance(currentSpeed, navMeshAgent.acceleration);
            // Need a min to allow us to stop spinning with very small vectors.
            navMeshAgent.stoppingDistance = Mathf.Max(stoppingDistance, minStoppingDistance);
        }

        private void UpdateAnimator()
        {
            animator.SetFloat("forwardSpeed", currentSpeed);
        }

        private float StoppingDistance(float speed, float decelleration)
        {
            return speed * speed / (2 * decelleration);
        }

        private void OnAnimatorMove()
        {
            var position = animator.rootPosition;
            position.y = navMeshAgent.nextPosition.y;
            transform.position = position;
        }
    }
}
