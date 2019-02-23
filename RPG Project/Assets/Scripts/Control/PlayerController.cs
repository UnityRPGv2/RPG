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
            InteractWithCombat();
            InteractWithMovement();
        }

        private void InteractWithCombat()
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
                }
            }
        }

        private void InteractWithMovement()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }

        private void MoveToCursor()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetRay(), out hit);
            if (hasHit)
            {
                mover.MoveTo(hit.point);
            }
        }

        private static Ray GetRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
    