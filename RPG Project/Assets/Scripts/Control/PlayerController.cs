using RPG.Combat;
using RPG.Movement;
using UnityEngine;
using RPG.Resources;
using System;
using UnityEngine.EventSystems;
using UnityEngine.AI;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Health health;

        [System.Serializable]
        public struct CursorMapping
        {
           public CursorType type;
           public Texture2D texture;
           public Vector2 hotspot;
        }

        [SerializeField] CursorMapping[] cursorMappings = null;
        [SerializeField] float navMeshProjectionDistance = 1f;
        [SerializeField] float maxNavDistance = 100f;

        bool moveStarted = false;

        private void Start() {
            health = GetComponent<Health>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                moveStarted = false;
            }

            if (InteractWithUI()) return;

            if (health.IsDead()) return;

            if (InteractWithComponents()) return;
            if (InteractWithMovement()) return;
            SetCursor(CursorType.None);
        }

        private bool InteractWithUI()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                SetCursor(CursorType.UI);
                return true;
            }
            return false;
        }

        private bool InteractWithComponents()
        {
            RaycastHit[] hits = RaycastAllSorted();
            foreach (RaycastHit hit in hits)
            {
                IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
                foreach (IRaycastable raycastable in raycastables)
                {
                    if (raycastable.HandleRaycast(this))
                    {
                        SetCursor(raycastable.GetCursorType());
                        return true;
                    }
                }
            }
            return false;
        }

        private static RaycastHit[] RaycastAllSorted()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            float[] distances = new float[hits.Length];
            for (int i = 0; i < distances.Length; i++)
            {
                distances[i] = hits[i].distance;
            }
            Array.Sort(distances, hits);
            return hits;
        }

        private bool InteractWithMovement()
        {
            Vector3 target;
            if (RaycastNavMesh(out target))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    moveStarted = true;
                }
                if (Input.GetMouseButton(0) && moveStarted)
                {
                    GetComponent<Mover>().StartMoveAction(target, 1f);
                }
                SetCursor(CursorType.Movement);
                return true;
            }
            return false;
        }

        private bool RaycastNavMesh(out Vector3 target)
        {
            target = new Vector3();
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (!hasHit) return false;

            NavMeshHit navMeshHit;
            bool hasCastToNavmesh = NavMesh.SamplePosition(hit.point, out navMeshHit, navMeshProjectionDistance, NavMesh.AllAreas);
            if (!hasCastToNavmesh) return false;

            NavMeshPath path = new NavMeshPath();
            bool canFindPath = NavMesh.CalculatePath(transform.position, navMeshHit.position, NavMesh.AllAreas, path);
            if (!canFindPath) return false;
            if (path.status != NavMeshPathStatus.PathComplete) return false;
            if (GetPathLength(path) > maxNavDistance) return false;

            target = navMeshHit.position;
            return true;
        }

        private static float GetPathLength(NavMeshPath path)
        {
            float total = 0;
            if (path.corners.Length < 2) return total;
            for (int i = 1; i < path.corners.Length; i++)
            {
                total += Vector3.Distance(path.corners[i - 1], path.corners[i]);
            }
            print(total);
            return total;
        }

        private void SetCursor(CursorType cursorType)
        {
            CursorMapping mapping = GetCursorMapping(cursorType);
            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
        }

        private CursorMapping GetCursorMapping(CursorType cursorType)
        {
            foreach (CursorMapping mapping in cursorMappings)
            {
                if (mapping.type == cursorType)
                {
                    return mapping;
                }
            }
            return cursorMappings[0];
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}