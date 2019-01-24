using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToPointer();
            }
        }

        private void MoveToPointer()
        {
            // Shoots a ray through the cursor location on the camera.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Out parameters, need to be explained
            RaycastHit hit;
            // Raycasting needs a slide or two.
            bool hasHit = Physics.Raycast(ray, out hit);
            if (hasHit)
            {
                GetComponent<NavMeshAgent>().destination = hit.point;
            }
        }
    }
}
