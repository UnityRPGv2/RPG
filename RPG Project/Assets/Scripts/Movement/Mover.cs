using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] Transform target;

        void Update()
        {
            GetComponent<NavMeshAgent>().destination = target.position;
        }
    }
}
