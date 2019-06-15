using UnityEngine;
using RPG.Resources;
using RPG.Control;

namespace RPG.Combat
{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour, IRaycastable
    {
        public bool HandleRaycast(PlayerController playerController)
        {
            Fighter playerFighter = playerController.GetComponent<Fighter>();
            if (!playerFighter.CanAttack(gameObject))
            {
                return false;
            }

            if (Input.GetMouseButton(0))
            {
                playerFighter.Attack(gameObject);
            }
            return true;
        }
    }
}