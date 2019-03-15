using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointContainer : MonoBehaviour
{
    private void OnDrawGizmos() {
        for (int currentIndex = 0; currentIndex < transform.childCount; currentIndex++)
        {
            int nextIndex = currentIndex + 1;
            if (nextIndex >= transform.childCount)
            {
                nextIndex = 0;
            }
            Vector3 currentPos = transform.GetChild(currentIndex).position;
            Vector3 nextPos = transform.GetChild(nextIndex).position;
            Gizmos.DrawLine(nextPos, currentPos);
            Gizmos.DrawSphere(currentPos, 0.3f);
        }
    }
}
