using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleMage
{
    public class EnemySpawner : MonoBehaviour
    {
        public static EnemySpawner Instance;

        [SerializeField] private GameObject objectToSpawn;
        [SerializeField] private float spawnInterval = 1f;

        private float insideRadius = 35f;
        private float outsideRadius = 45f;

        //public List<GameObject> objectList = new List<GameObject>();

        private void Awake()
        {
            Instance = this;
            BeginSpawning();
        }

        public void BeginSpawning()
        {
            StartCoroutine(Spawn());
        }

        public void StopSpawning()
        {
            StopCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            //while (!PlayerManager.PlayerDead)
            {
                yield return new WaitForSeconds(spawnInterval);
                spawnInterval -= spawnInterval * 0.02f;

                SpawnObject();
                //StartCoroutine(Spawn());
            }
        }

        private GameObject SpawnObject()
        {
            // random angle
            float theta = Random.Range(0, 360);
            // random radus
            float randomRadius = Random.Range(insideRadius, outsideRadius);
            Vector3 randomPos = new Vector3(randomRadius * Mathf.Cos(theta), .5f, randomRadius * Mathf.Sin(theta));

            GameObject obj = Instantiate(objectToSpawn, randomPos, Quaternion.LookRotation(-randomPos, Vector3.up));
            obj.SetActive(true);

            // find a random positions to spawn
            // inside border is insideRadius * Cos(theta), insideRadius * Sin(theta)
            // outside border is outsideRadius * Cos(theta), outsideRadius * Sin(theta)

            //objectList.Add(obj);

            return obj;
        }
    }
}