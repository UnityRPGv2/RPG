using UnityEngine;
using GameDevTV.Saving;
using System;
using Newtonsoft.Json.Linq;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experiencePoints = 0;

        public event Action onExperienceGained;

        private void Update() {
            if (Input.GetKey(KeyCode.E))
            {
                GainExperience(Time.deltaTime * 1000);
            }
        }

        public void GainExperience(float experience)
        {
            experiencePoints += experience;
            onExperienceGained();
        }

        public float GetPoints()
        {
            return experiencePoints;
        }

        public JToken CaptureState()
        {
            return experiencePoints;
        }

        public void RestoreState(JToken state)
        {
            experiencePoints = (float)state;
        }
    }
}