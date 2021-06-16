using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleMage
{
    /// <summary>
    /// Class that manages the spawn of the enemies
    /// </summary>
    public class EnemySpawner : MonoBehaviour
    {
        public static EnemySpawner Instance;

        [SerializeField] private GameObject objectToSpawn;
        [SerializeField] private float spawnInterval = 1f;

        // inside radius spawn range
        private float insideRadius = 35f;
        // outside radius spawn range
        private float outsideRadius = 45f;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            BeginSpawning();
        }

        /// <summary>
        /// Start the spawning of the enemies
        /// </summary>
        public void BeginSpawning()
        {
            StartCoroutine(Spawn());
        }

        /// <summary>
        /// Stop the spawning of the enemies
        /// </summary>
        public void StopSpawning()
        {
            StopCoroutine(Spawn());
        }


        /// <summary>
        /// Spawn an enemy after a time delay
        /// </summary>
        private IEnumerator Spawn()
        {
            // i keep spawning if the player is not dead
            while (!PlayerManager.PlayerDead)
            {
                yield return new WaitForSeconds(spawnInterval);
                spawnInterval -= spawnInterval * 0.02f;

                SpawnObject();
            }
        }

        /// <summary>
        /// Spawns the actual enemy prefab in a circular radius from the center of the map
        /// </summary>
        private GameObject SpawnObject()
        {
            // find a random positions to spawn
            // inside border is [insideRadius * Cos(theta), insideRadius * Sin(theta)]
            // outside border is [outsideRadius * Cos(theta), outsideRadius * Sin(theta)]

            // random angle
            float theta = Random.Range(0, 360);
            // random radius
            float randomRadius = Random.Range(insideRadius, outsideRadius);
            // random position
            Vector3 randomPos = new Vector3(randomRadius * Mathf.Cos(theta), .5f, randomRadius * Mathf.Sin(theta));

            GameObject obj = Instantiate(objectToSpawn, randomPos, Quaternion.LookRotation(-randomPos, Vector3.up));
            obj.SetActive(true);

            return obj;
        }
    }
}