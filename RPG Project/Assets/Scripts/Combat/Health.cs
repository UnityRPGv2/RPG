using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour {
        [SerializeField] float healthPoints = 100;

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(0, healthPoints - damage);
            print(healthPoints);
        }
    }
}