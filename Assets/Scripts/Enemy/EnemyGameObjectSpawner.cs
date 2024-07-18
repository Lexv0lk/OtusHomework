using System;
using Cysharp.Threading.Tasks;
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
        private readonly EnemySpawnerConfig _config;

        private int _currentEnemyCount = 0;

        [Inject]
        public EnemyGameObjectSpawner(EnemyPool pool, EnemyInitializer initializer,
            EnemySpawnerConfig spawnerConfig)
        {
            _pool = pool;
            _initializer = initializer;
            _config = spawnerConfig;
        }

        public event Action<Enemy> Spawned;
        public event Action<GameObject> SpawnedObject;
        public event Action<GameObject> ReleasedObject;
        
        void IGameStartListener.OnStart()
        {
            if (_config.SpawnOnStart)
                StartSpawning();
        }

        public void StartSpawning()
        {
            SpawnEnemiesWithDelay();
        }

        public void Release(Enemy enemy)
        {
            _currentEnemyCount--;
            _pool.Release(enemy);
            ReleasedObject?.Invoke(enemy.gameObject);
        }

        private UniTaskVoid SpawnEnemiesWithDelay()
        {
            while (true)
            {
                UniTask.WaitForSeconds(_config.SpawnDelay);

                if (_currentEnemyCount < _config.Count)
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