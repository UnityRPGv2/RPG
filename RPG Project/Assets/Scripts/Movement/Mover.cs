using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] Transform target;

        Ray lastRay;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            }
            Debug.DrawRay(lastRay.origin, lastRay.direction * 1000);
            GetComponent<NavMeshAgent>().destination = target.position;
        }
    }
}
