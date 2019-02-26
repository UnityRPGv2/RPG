using UnityEngine;
using RPG.Movement;
using System;
using RPG.Combat;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour 
    {
        Mover mover;

        private void Start() {
            mover = GetComponent<Mover>();    
        }

        private void Update()
        {
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
            print("Hovering over nothing");
        }

        private bool InteractWithCombat()
        {
            var hits = Physics.RaycastAll(GetRay());
            foreach (var hit in hits)
            {
                var combatComponent = hit.collider.GetComponent<CombatTarget>();
                if (combatComponent != null)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        combatComponent.Attack(this);
                    }
                    return true;
                }
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetRay(), out hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    mover.StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }

        private void MoveToCursor()
        {
        }

        private static Ray GetRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
    