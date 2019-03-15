using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointContainer : MonoBehaviour
{
    private void OnDrawGizmos() {
        foreach (Transform waypoint in GetWaypoints())
        {
            Gizmos.DrawSphere(waypoint.position, 0.3f);
        }
    }

    Transform[] GetWaypoints()
    {
        return GetComponentsInChildren<Transform>();
    }
}
