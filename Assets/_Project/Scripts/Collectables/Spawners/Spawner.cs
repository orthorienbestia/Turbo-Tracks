using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Utility;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Collectables.Spawners
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] protected List<GameObject> powerUpPrefabs;
        [SerializeField] protected GameObject coinPrefab;
        [SerializeField] protected Transform[] spawnAreas;

        // Spawn Points are children of the Spawn Areas
        private Transform[][] _spawnPointsByArea;

        private readonly HashSet<Vector3> _objectSpawnedPositions = new();
        private List<Coin> _spawnedCoins = new List<Coin>();

        private void Awake()
        {
            _spawnPointsByArea = spawnAreas.Select(spawnArea =>
                spawnArea.GetComponentsInChildren<Transform>().Where(t => t != spawnArea).ToArray()).ToArray();
        }

        private void Start()
        {
            SpawnPowerUp(-1);
            SpawnCoins(-1);
            GameplayManager.Instance.OnLapComplete += OnLapCompleted;
        }

        private void OnLapCompleted()
        {
            // Destroy all coins and spawn new ones.
            foreach (var coin in _spawnedCoins)
            {
                if (coin == null) continue;
                _objectSpawnedPositions.Remove(coin.transform.position);
                coin.transform.DOScale(Vector3.zero, 0.5f).OnComplete(() =>
                {
                    if (coin == null) return;
                    Destroy(coin.gameObject);
                });
            }

            _spawnedCoins.Clear();
            SpawnCoins(-1);
        }

        [ContextMenu("Spawn PowerUp")]
        // Spawn a power up at a random spawn point.
        public void SpawnPowerUp(int count)
        {
            const float probability = 0.17f;
            foreach (var spawnArea in _spawnPointsByArea)
            {
                foreach (var spawnPoint in spawnArea)
                {
                    if (count == 0)
                    {
                        return;
                    }

                    var position = spawnPoint.position;

                    if (Random.Range(0, 1.0f) > probability || _objectSpawnedPositions.Contains(position)) continue;

                    var spawnedObj = Spawn(powerUpPrefabs.GetRandomItem(), position, spawnPoint.rotation);
                    var powerUp = spawnedObj.GetComponent<Collectable>();
                    powerUp.OnObjectCollected += _ =>
                    {
                        RemoveSpawnedPosition(position);
                        SpawnPowerUp(1);
                    };

                    _objectSpawnedPositions.Add(position);
                    count--;
                }
            }
        }

        [ContextMenu("Spawn Coins")]
        // Spawn coins at a random spawn point.
        public void SpawnCoins(int count)
        {
            const float probability = 0.35f;
            foreach (var spawnArea in _spawnPointsByArea)
            {
                foreach (var spawnPoint in spawnArea)
                {
                    if (count == 0)
                    {
                        return;
                    }

                    var position = spawnPoint.position;

                    if (Random.Range(0, 1.0f) > probability || _objectSpawnedPositions.Contains(position)) continue;
                    var spawnedObj = Spawn(coinPrefab, spawnPoint.position, spawnPoint.rotation);
                    var coin = spawnedObj.GetComponent<Coin>();
                    _spawnedCoins.Add(coin);
                    coin.OnObjectCollected += _ =>
                    {
                        _spawnedCoins.Remove(coin);
                        RemoveSpawnedPosition(position);
                    };
                    _objectSpawnedPositions.Add(position);
                    count--;
                }
            }
        }

        private GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            var spawnedObj = Instantiate(prefab, position, rotation, transform);

            spawnedObj.transform.DOScale(Vector3.zero, 1f).From();

            return spawnedObj;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;

            _spawnPointsByArea = spawnAreas.Select(spawnArea =>
                spawnArea.GetComponentsInChildren<Transform>().Where(t => t != spawnArea).ToArray()).ToArray();
            foreach (var spawnArea in _spawnPointsByArea)
            {
                foreach (var spawnPoint in spawnArea)
                {
                    Gizmos.DrawSphere(spawnPoint.position + new Vector3(0, 0.25f, 0), 0.2f);
                }
            }
        }

        private void RemoveSpawnedPosition(Vector3 position)
        {
            _objectSpawnedPositions.Remove(position);
        }
    }
}