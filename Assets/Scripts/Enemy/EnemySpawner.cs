using System;
using System.Collections;
using UnityEngine;

namespace ShootEmUp.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyPool _pool;
        [SerializeField] private EnemyInitializer _initializer;

        [Header("Spawn Settings")]
        [SerializeField] private int _count = 7;
        [SerializeField] private float _spawnDelay = 1;
        [SerializeField] private bool _spawnOnStart;

        public event Action<Enemy> Spawned;

        private void Start()
        {
            if (_spawnOnStart)
                SpawnAll();
        }

        public void SpawnAll()
        {
            StartCoroutine(SpawnEnemiesWithDelay());
        }

        public void Release(Enemy enemy)
        {
            _pool.Release(enemy);
        }

        private IEnumerator SpawnEnemiesWithDelay()
        {
            WaitForSeconds delay = new WaitForSeconds(_spawnDelay);

            for (int i = 0; i < _count; i++)
            {
                yield return delay;
                Enemy enemy = _pool.Get();
                _initializer.Initialize(enemy);
                Spawned?.Invoke(enemy);
            }
        }
    }
}