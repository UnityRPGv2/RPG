using System;
using RPG.Resources;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "RPG Project/Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] GameObject equippedPrefab = null;
        [SerializeField] float damage = 3f;
        [SerializeField] float percentageBonus = 0;
        [SerializeField] float range = 3f;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile projectile = null;

        const string weaponName = "Weapon";

        public void Spawn(Transform leftHand, Transform rightHand, Animator animator)
        {
            DestroyOldWeapon(leftHand, rightHand);

            if (equippedPrefab != null)
            {
                GameObject weapon = Instantiate(equippedPrefab, GetTranform(leftHand, rightHand));
                weapon.name = weaponName;
            }
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
        }

        private Transform GetTranform(Transform leftHand, Transform rightHand)
        {
            if (isRightHanded)
            {
                return rightHand;
            }
            else
            {
                return leftHand;
            }
            //return isRightHanded ? rightHand : leftHand;
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }

        public void LaunchProjectile(GameObject instigator, Transform leftHand, Transform rightHand, Health target)
        {
            Projectile projectileInstance = Instantiate(projectile, GetTranform(leftHand, rightHand).position, Quaternion.identity);
            projectileInstance.SetTarget(instigator, target, damage);
        }

        private static void DestroyOldWeapon(Transform leftHand, Transform rightHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName);
            if (oldWeapon == null)
            {
                oldWeapon = leftHand.Find(weaponName);
            }
            if (oldWeapon == null) return;

            // So that we don't clash with a new weapon created immediately after.
            oldWeapon.name = "DESTROYING";
            Destroy(oldWeapon.gameObject);
        }

        public float GetDamage()
        {
            return damage;
        }

        public float GetRange()
        {
            return range;
        }

        internal float GetPercentageBonus()
        {
            return percentageBonus;
        }
    }
}