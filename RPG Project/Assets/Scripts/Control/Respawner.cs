using System;
using System.Collections;
using Cinemachine;
using RPG.Attributes;
using RPG.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Control
{
    public class Respawner : MonoBehaviour {
        [SerializeField] Transform respawnLocation;
        [SerializeField] float respawnDelay = 3;
        [SerializeField] float fadeTime = 0.2f;
        [SerializeField] float healthRegenPercentage = 20;
        [SerializeField] float enemyHealthRegenPercentage = 20;

        private void Awake()
        {
            GetComponent<Health>().onDie.AddListener(Respawn);
        }

        private void Start() {
            if (GetComponent<Health>().IsDead())
            {
                Respawn();
            }
        }

        private void Respawn()
        {
            StartCoroutine(RespawnRoutine());
        }

        private IEnumerator RespawnRoutine()
        {
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
            // savingWrapper.Save();
            yield return new WaitForSeconds(respawnDelay);
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeTime);
            RespawnPlayer();
            ResetEnemies();
            // savingWrapper.Save();
            yield return fader.FadeIn(fadeTime);
        }

        private void ResetEnemies()
        {
            foreach (AIController enemyControllers in FindObjectsOfType<AIController>())
            {
                Health health = enemyControllers.GetComponent<Health>();
                if (health && !health.IsDead())
                {
                    enemyControllers.Reset();
                    health.Heal(health.GetMaxHealthPoints() * enemyHealthRegenPercentage / 100);
                }
            }
        }

        private void RespawnPlayer()
        {
            Vector3 postionDelta = respawnLocation.position - transform.position;
            GetComponent<NavMeshAgent>().Warp(respawnLocation.position);
            Health health = GetComponent<Health>();
            health.Heal(health.GetMaxHealthPoints() * healthRegenPercentage / 100);
            ICinemachineCamera activeVirtualCamera = FindObjectOfType<CinemachineBrain>().ActiveVirtualCamera;
            if (activeVirtualCamera.Follow == transform)
            {
                activeVirtualCamera.OnTargetObjectWarped(transform, postionDelta);
            }
        }
    }
}