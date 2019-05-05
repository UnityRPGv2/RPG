using RPG.Saving;
using UnityEngine;

namespace RPG.Resources
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experiencePoints = 0;

        public void GainPoints(float earnedPoints)
        {
            experiencePoints += earnedPoints;
        }

        public void RestoreState(object state)
        {
            experiencePoints = (float)state;
        }

        public object CaptureState()
        {
            return experiencePoints;
        }
    }
}