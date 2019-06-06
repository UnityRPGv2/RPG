using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Resources
{
    public class ExperienceDisplay : MonoBehaviour
    {
        Experience experience;

        private void Awake()
        {
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
        }

        private void Update()
        {
            GetComponent<Text>().text = String.Format("{0:0}", experience.GetPoints());
        }
    }
}