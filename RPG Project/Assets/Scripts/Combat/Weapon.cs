using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "RPG Project/Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] GameObject equippedPrefab = null;
        [SerializeField] float damage = 3f;
        [SerializeField] float range = 3f;

        public void Spawn(Transform handTransform, Animator animator)
        {
            Instantiate(equippedPrefab, handTransform);
            animator.runtimeAnimatorController = animatorOverride;
        }

        public float GetDamage()
        {
            return damage;
        }

        public float GetRange()
        {
            return range;
        }
    }
}