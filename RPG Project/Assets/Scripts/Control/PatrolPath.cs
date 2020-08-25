using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {

        const float waypointGizmoRadius = 0.3f;
        // Start is called before the first frame update
        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {

                Gizmos.DrawSphere(GetWayPoint(i), waypointGizmoRadius);
                Gizmos.DrawLine(GetWayPoint(i), GetWayPoint(GetNextIndex(i , transform.childCount) ));


            }
        }

        private static int GetNextIndex( int i, int numberOfIndex ) {
            Debug.Log(numberOfIndex);
            if (i >= numberOfIndex-1)
            {
                return 0 ;
            } else return i+1;
        }

        private Vector3 GetWayPoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}
