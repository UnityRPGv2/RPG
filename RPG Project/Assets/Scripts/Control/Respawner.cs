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
        [SerializeField] Transform[] respawnPoints;
        [SerializeField] float delay;
        [SerializeField] float fadeOutTime;
        [SerializeField] float fadeInTime;
        [SerializeField] float playerHealthRegen = 20;
        [SerializeField] float enemyHealthRegen = 20;

        Health playerHealth;

        private void Awake() {
            playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
            playerHealth.onDie.AddListener(Respawn);
        }

        private void Start() {
            if (playerHealth.IsDead()) Respawn();
        }

        public void Respawn()
        {
            StartCoroutine(FadeAndRespawn());
        }

        private IEnumerator FadeAndRespawn()
        {
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
            savingWrapper.Save();
            yield return new WaitForSeconds(delay);
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeOutTime);
            RespawnPlayer(playerHealth);
            ResetEnemies();
            playerHealth.Heal(playerHealthRegen / 100 * playerHealth.GetMaxHealthPoints());
            yield return fader.FadeIn(fadeInTime);
            savingWrapper.Save();
        }

        private void ResetEnemies()
        {
            foreach (var enemy in FindObjectsOfType<AIController>())
            {
                Health health = enemy.GetComponent<Health>();
                if (health && !health.IsDead())
                {
                    health.Heal(enemyHealthRegen / 100 * health.GetMaxHealthPoints());
                    enemy.Reset();
                }
            }
        }

        private void RespawnPlayer(Health health)
        {
            ICinemachineCamera activeVirtualCamera = FindObjectOfType<CinemachineBrain>().ActiveVirtualCamera;
            Vector3 respawnPoint = GetClosestRespawnPoint(health);
            NavMeshAgent navMeshAgent = health.GetComponent<NavMeshAgent>();
            Vector3 delta = respawnPoint - health.transform.position;
            if (activeVirtualCamera.Follow == health.transform)
            {
                activeVirtualCamera.OnTargetObjectWarped(health.transform, delta);
            }
            navMeshAgent.Warp(respawnPoint);
        }

        private Vector3 GetClosestRespawnPoint(Health health)
        {
            Vector3 currentPosition = health.transform.position;
            Vector3 closest = transform.position;
            float closestDist = float.MaxValue;
            foreach (Transform candidate in respawnPoints)
            {
                float candidateDist = Vector3.Distance(candidate.position, currentPosition);
                if (candidateDist < closestDist) 
                {
                    closest = candidate.position;
                    closestDist = candidateDist;
                }
            }
            return closest;
        }
    }
}