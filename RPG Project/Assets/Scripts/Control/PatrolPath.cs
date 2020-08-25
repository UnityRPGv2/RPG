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

                Gizmos.DrawSphere(transform.GetChild(i).position, waypointGizmoRadius);
                
            }
        }
    }
}
