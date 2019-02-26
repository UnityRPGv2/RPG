using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;

namespace RPG.Combat
{
    public class CombatTarget : MonoBehaviour
    {
        public void Attack(PlayerController playerController)
        {
            var playerCombatComponent = GameObject.FindGameObjectWithTag("Player").GetComponent<Fighter>();
            playerCombatComponent.Attack(this);
        }
    }
}