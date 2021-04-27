using System;
using RPG.Combat;
using RPG.Core;
using UnityEngine;

namespace RPG.Abilities.Effects
{
    [CreateAssetMenu(fileName = "Spawn Projectile", menuName = "Abilities/Effects/Spawn Projectile", order = 0)]
    public class SpawnProjectileEffect : EffectStrategy
    {
        [SerializeField] Projectile prefabToSpawn;
        [SerializeField] SpawnLocation spawnLocation;
        [SerializeField] float damage;

        enum SpawnLocation
        {
            Center,
            RightHand,
            LeftHand
        }

        class EffectInstance : IAction
        {
            TargetingData data;
            Action complete;
            SpawnProjectileEffect config;

            public EffectInstance(SpawnProjectileEffect config, TargetingData data, Action complete)
            {
                this.data = data;
                this.complete = complete;
                this.config = config;
            }

            public void Start()
            {
                var scheduler = data.GetSource().GetComponent<ActionScheduler>();
                scheduler.SuspendAndStartAction(this, 3, 3);
            }

            public void Cancel()
            {
                Debug.Log("Canceled");
                AnimationNotifications animationNotifications = data.GetSource().GetComponent<AnimationNotifications>();
                animationNotifications.animationNotify -= Handler;
            }

            private void Handler(AnimationEvent evnt)
            {
                AnimationNotifications animationNotifications = data.GetSource().GetComponent<AnimationNotifications>();
                var scheduler = data.GetSource().GetComponent<ActionScheduler>();
                if (evnt.functionName == "Shoot")
                {
                    animationNotifications.animationNotify -= Handler;
                    if (!scheduler.isCurrentAction(this)) return;
                    Projectile instance = Instantiate(config.prefabToSpawn, config.GetPosition(data), Quaternion.identity);
                    instance.SetTarget(null, data.GetSource(), config.damage * data.GetEffectScaling(), data.GetTargetPoint());
                    scheduler.FinishAction(this);
                    if (complete != null) complete();
                }
            }

            public void Activated()
            {
                Debug.Log("Activated");
                AnimationNotifications animationNotifications = data.GetSource().GetComponent<AnimationNotifications>();
                animationNotifications.animationNotify += Handler;
            }
        }

        public override void StartEffect(TargetingData data, Action complete)
        {
            new EffectInstance(this, data, complete).Start();
        }

        private Vector3 GetPosition(TargetingData data)
        {
            switch (spawnLocation)
            {
                case SpawnLocation.RightHand:
                    return data.GetSource().GetComponent<Fighter>().GetHandTransform(true).position;
                case SpawnLocation.LeftHand:
                    return data.GetSource().GetComponent<Fighter>().GetHandTransform(false).position;
                case SpawnLocation.Center:
                default:
                    return data.GetSource().transform.position;
            }
        }
    }
}