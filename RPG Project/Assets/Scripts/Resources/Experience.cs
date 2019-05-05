using UnityEngine;

namespace RPG.Resources
{
    public class Experience : MonoBehaviour
    {
        [SerializeField] float experiencePoints = 0;

        public void GainPoints(float earnedPoints)
        {
            experiencePoints += earnedPoints;
        }
    }
}