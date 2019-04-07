using System;
using UnityEngine;

namespace RPG.Core
{
    public class PeristentObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistentObjectPrefab;

        static bool hasSpawned = false;

        private void Awake() {
            if (hasSpawned) return;

            SpawnPersistentObjects();

            hasSpawned = true;
        }

        private void SpawnPersistentObjects()
        {
            GameObject persistentObject = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }
    }
}