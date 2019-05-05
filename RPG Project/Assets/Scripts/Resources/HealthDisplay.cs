using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Resources
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health = null;


        private void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
        }

        private void Update() {
            GetComponent<Text>().text = String.Format("{0:0}%", health.GetPercentage() * 100);
        }
    }
}