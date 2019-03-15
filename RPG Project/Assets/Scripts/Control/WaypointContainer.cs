using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointContainer : MonoBehaviour
{
    public Vector3 GetWaypoint(int index)
    {
        return transform.GetChild(index).position;
    }

    public float GetWaypointCount()
    {
        return transform.childCount;
    }

    private void OnDrawGizmos() {
        for (int currentIndex = 0; currentIndex < GetWaypointCount(); currentIndex++)
        {
            int nextIndex = currentIndex + 1;
            if (nextIndex >= GetWaypointCount())
            {
                nextIndex = 0;
            }
            Vector3 currentPos = GetWaypoint(currentIndex);
            Vector3 nextPos = GetWaypoint(nextIndex);
            Gizmos.DrawLine(nextPos, currentPos);
            Gizmos.DrawSphere(currentPos, 0.3f);
        }
    }
}
