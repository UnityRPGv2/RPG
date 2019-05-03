using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon = null;

        private void OnTriggerEnter(Collider other) {
            if (other.tag != "Player") return;
            other.GetComponent<Fighter>().EquipWeapon(weapon);
            Destroy(gameObject);
        }
    }
}