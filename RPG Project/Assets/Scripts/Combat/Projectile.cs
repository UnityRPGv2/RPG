using UnityEngine;
using RPG.Attributes;
using UnityEngine.Events;
using System;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 1;
        [SerializeField] bool isHoming = true;
        [SerializeField] GameObject hitEffect = null;
        [SerializeField] float maxLifeTime = 10;
        [SerializeField] GameObject[] destroyOnHit = null;
        [SerializeField] float lifeAfterImpact = 2;
        [SerializeField] UnityEvent onHit;

        Vector3 targetPosition;
        Health target = null;
        GameObject instigator = null;
        float damage = 0;

        private void Start()
        {
            transform.LookAt(GetAimLocation());
        }

        void Update()
        {
            if (isHoming && target != null && !target.IsDead())
            {
                transform.LookAt(GetAimLocation());
            }
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        public static void Launch(Projectile projectile, Vector3 position, Health target, GameObject instigator, float calculatedDamage)
        {
            Launch(projectile, position, target.transform.position, instigator, calculatedDamage, target);
        }

        public static void Launch(Projectile projectile, Vector3 position, Vector3 targetPosition, GameObject instigator, float calculatedDamage, Health target=null)
        {
            Projectile projectileInstance = Instantiate(projectile, position, Quaternion.identity);
            projectileInstance.SetTarget(targetPosition, target, instigator, calculatedDamage);
        }

        private void SetTarget(Vector3 targetPosition, Health target, GameObject instigator, float damage)
        {
            this.targetPosition = targetPosition;
            this.target = target;
            this.damage = damage;
            this.instigator = instigator;

            Destroy(gameObject, maxLifeTime);
        }

        private Vector3 GetAimLocation()
        {
            if (target == null)
            {
                return targetPosition;
            }
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null)
            {
                return target.transform.position;
            }
            return target.transform.position + Vector3.up * targetCapsule.height / 2;
        }

        private void OnTriggerEnter(Collider other)
        {
            Health health = other.GetComponent<Health>();
            if (target != null && health != target) return;
            if (health == null || health.IsDead() || other.gameObject == instigator ) return;

            health.TakeDamage(instigator, damage);

            speed = 0;

            onHit.Invoke();

            if (hitEffect != null)
            {
                Instantiate(hitEffect, GetAimLocation(), transform.rotation);
            }

            foreach (GameObject toDestroy in destroyOnHit)
            {
                Destroy(toDestroy);
            }

            Destroy(gameObject, lifeAfterImpact);

        }
    }

}