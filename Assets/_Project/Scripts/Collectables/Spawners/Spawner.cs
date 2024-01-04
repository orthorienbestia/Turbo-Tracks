using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Utility;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Collectables.Spawners
{
    public class Spawner : MonoBehaviour
    {
        private static string SpawnAreaTag => "SpawnArea";
        
        [SerializeField] protected List<GameObject> powerUpPrefabs;
        [SerializeField] protected GameObject coinPrefab;
        [SerializeField] protected Transform[] spawnAreas;
        
        // Spawn Points are children of the Spawn Areas
        private Transform[][] _spawnPointsByArea;
        
        private void Awake()
        {
            spawnAreas = GameObject.FindGameObjectsWithTag(SpawnAreaTag).Select(x => x.transform).ToArray();
            _spawnPointsByArea = spawnAreas.Select(spawnArea => spawnArea.GetComponentsInChildren<Transform>().Where(t=> t!=spawnArea).ToArray()).ToArray();
        }

        private void Start()
        {
            SpawnPowerUp();
            SpawnCoins();
        }

        [ContextMenu("Spawn PowerUp")]
        public void SpawnPowerUp()
        {
            const float probability = 0.3f;
            foreach (var spawnArea in _spawnPointsByArea)
            {
                foreach (var spawnPoint in spawnArea)
                {
                    if (Random.Range(0, 1.0f) > probability) continue;
                    Spawn(powerUpPrefabs.GetRandomItem(), spawnPoint.position, spawnPoint.rotation);
                }
            }
        }
        
        [ContextMenu("Spawn Coins")]
        public void SpawnCoins()
        {
            const float probability = 0.5f;
            foreach (var spawnArea in _spawnPointsByArea)
            {
                foreach (var spawnPoint in spawnArea)
                {
                    if (Random.Range(0, 1.0f) > probability) continue;
                    Spawn(coinPrefab, spawnPoint.position, spawnPoint.rotation);
                }
            }
        }

        private GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            return Instantiate(prefab, position, rotation, transform);
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            
            _spawnPointsByArea = spawnAreas.Select(spawnArea => spawnArea.GetComponentsInChildren<Transform>().Where(t=> t!=spawnArea).ToArray()).ToArray();
            foreach (var spawnArea in _spawnPointsByArea)
            {
                foreach (var spawnPoint in spawnArea)
                {
                    Gizmos.DrawSphere(spawnPoint.position + new Vector3(0,0.25f,0), 0.2f);
                }
            }
        }
    }
}