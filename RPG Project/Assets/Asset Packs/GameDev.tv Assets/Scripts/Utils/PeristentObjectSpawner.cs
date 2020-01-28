using System;
using UnityEngine;

namespace GameDevTV.Utils
{
    /// <summary>
    /// An alternative to using the singleton pattern. Will handle spawning a
    /// prefab only once across multiple scenes.
    ///
    /// Place this in a prefab that exists in every scene. Point to another
    /// prefab that contains all GameObjects that should be singletons. The
    /// class will spawn the prefab only once and set it to persist between
    /// scenes.
    /// </summary>
    public class PeristentObjectSpawner : MonoBehaviour
    {
        // CONFIG DATA
        [Tooltip("This prefab will only be spawned once and persisted between " +
        "scenes.")]
        [SerializeField] GameObject persistentObjectPrefab = null;

        // PRIVATE STATE
        static bool hasSpawned = false;

        // PRIVATE
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