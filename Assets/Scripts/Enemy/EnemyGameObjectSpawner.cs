using System;
using System.Collections;
using ShootEmUp.Common;
using ShootEmUp.GameStates;
using UnityEngine;
using Zenject;

namespace ShootEmUp.Enemies
{
    public class EnemyGameObjectSpawner : IGameStartListener, IGameObjectSpawner
    {
        private readonly EnemyPool _pool;
        private readonly EnemyInitializer _initializer;
        private readonly EnemySpawnerSettings _settings;

        private int _currentEnemyCount = 0;

        [Inject]
        public EnemyGameObjectSpawner(EnemyPool pool, EnemyInitializer initializer,
            EnemySpawnerSettings spawnerSettings)
        {
            _pool = pool;
            _initializer = initializer;
            _settings = spawnerSettings;
        }

        public event Action<Enemy> Spawned;
        public event Action<GameObject> SpawnedObject;
        public event Action<GameObject> ReleasedObject;
        
        void IGameStartListener.OnStart()
        {
            if (_settings.SpawnOnStart)
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
            ReleasedObject?.Invoke(enemy.gameObject);
        }

        private IEnumerator SpawnEnemiesWithDelay()
        {
            WaitForSeconds delay = new WaitForSeconds(_settings.SpawnDelay);

            while (true)
            {
                yield return delay;

                if (_currentEnemyCount < _settings.Count)
                {
                    Enemy enemy = _pool.Get();
                    _initializer.Initialize(enemy);
                    _currentEnemyCount++;
                    Spawned?.Invoke(enemy);
                    SpawnedObject?.Invoke(enemy.gameObject);
                }
            }
        }
    }
}