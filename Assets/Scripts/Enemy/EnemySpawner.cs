using System;
using System.Collections;
using ShootEmUp.GameStates;
using UnityEngine;

namespace ShootEmUp.Enemies
{
    public class EnemySpawner : MonoBehaviour, IGameStartListener
    {
        [SerializeField] private EnemyPool _pool;
        [SerializeField] private EnemyInitializer _initializer;

        [Header("Spawn Settings")]
        [SerializeField] private int _count = 7;
        [SerializeField] private float _spawnDelay = 1;
        [SerializeField] private bool _spawnOnStart;

        private int _currentEnemyCount = 0;

        public event Action<Enemy> Spawned;

        void IGameStartListener.OnStart()
        {
            Debug.Log("SPAWN START");
            if (_spawnOnStart)
                StartSpawning();
        }

        public void StartSpawning()
        {
            StartCoroutine(SpawnEnemiesWithDelay());
        }

        public void Release(Enemy enemy)
        {
            _currentEnemyCount--;
            _pool.Release(enemy);
        }

        private IEnumerator SpawnEnemiesWithDelay()
        {
            WaitForSeconds delay = new WaitForSeconds(_spawnDelay);

            while (true)
            {
                yield return delay;

                if (_currentEnemyCount < _count)
                {
                    Enemy enemy = _pool.Get();
                    _initializer.Initialize(enemy);
                    _currentEnemyCount++;
                    Spawned?.Invoke(enemy);
                }
            }
        }
    }
}