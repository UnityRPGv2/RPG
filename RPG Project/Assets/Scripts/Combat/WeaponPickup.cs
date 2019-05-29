using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon = null;
        [SerializeField] float respawnTime = 10;
    
        private void OnTriggerEnter(Collider other) 
        {
            if (other.gameObject.tag == "Player")
            {
                other.GetComponent<Fighter>().EquipWeapon(weapon);
                StartCoroutine(HideForSecond(respawnTime));
            }
        }
        
        IEnumerator HideForSecond(float seconds)
        {
            SetChildrenActive(false);
            GetComponent<Collider>().enabled = false;
            yield return new WaitForSeconds(seconds);
            SetChildrenActive(true);
            GetComponent<Collider>().enabled = true;
        }

        private void SetChildrenActive(bool state)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(state);
            }
        }
    }
}