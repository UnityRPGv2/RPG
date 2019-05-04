using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 1;
        [SerializeField] float maxLifeTime = 10;
        [SerializeField] float lifeAfterDestroy = 2;
        [SerializeField] GameObject[] destroyOnHit = null;
        [SerializeField] GameObject hitEffect = null;

        Health target = null;
        float damage = 0;

        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;

            transform.LookAt(GetAimLocation());

            Destroy(gameObject, maxLifeTime);
        }

        private void Update() {
            if (target == null) return;

            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null)
            {
                return target.transform.position;
            }

            return target.transform.position + Vector3.up * targetCapsule.height / 2;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != target) return;

            target.TakeDamage(damage);

            speed = 0;

            if (hitEffect != null)
            {
                Instantiate(hitEffect, GetAimLocation(), transform.rotation);
            }

            foreach (GameObject toDestroy in destroyOnHit)
            {
                Destroy(toDestroy);
            }
            Destroy(gameObject, lifeAfterDestroy);
        }
    }
}