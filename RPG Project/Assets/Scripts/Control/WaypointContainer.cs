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

    public int GetNextIndex(int index)
    {
        if (index + 1 == GetWaypointCount())
        {
            return 0;
        }
        return index + 1;
    }

    private void OnDrawGizmos() {
        for (int currentIndex = 0; currentIndex < GetWaypointCount(); currentIndex++)
        {
            int nextIndex = GetNextIndex(currentIndex);
            Vector3 currentPos = GetWaypoint(currentIndex);
            Vector3 nextPos = GetWaypoint(nextIndex);
            Gizmos.DrawLine(nextPos, currentPos);
            Gizmos.DrawSphere(currentPos, 0.3f);
        }
    }
}
