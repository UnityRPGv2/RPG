using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter = null;


        private void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }

        private void Update() {

            if (fighter.GetTarget() == null)
            {
                GetComponent<Text>().text = "";
                return;
            }

            GetComponent<Text>().text = String.Format("{0:0}%", fighter.GetTarget().GetPercentage() * 100);
        }
    }
}