using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        private void OnDrawGizmos() {
            for (int currentIndex = 0; currentIndex < transform.childCount; currentIndex++)
            {
                int nextIndex = GetNextIndex(currentIndex);
                Gizmos.DrawSphere(GetWaypoint(currentIndex), 0.2f);
                Gizmos.DrawLine(GetWaypoint(currentIndex), GetWaypoint(nextIndex));
            }
        }

        private int GetNextIndex(int currentIndex)
        {
            //CHALLENGE
            if (currentIndex + 1 == transform.childCount)
            {
                return 0;
            }
            return currentIndex + 1;
        }

        private Vector3 GetWaypoint(int index)
        {
            return transform.GetChild(index).position;
        }
    }
}