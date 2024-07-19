using System;
using Cysharp.Threading.Tasks;
using ShootEmUp.Common;
using ShootEmUp.GameStates;
using ShootEmUp.GameUpdate;
using UnityEngine;
using Zenject;

namespace ShootEmUp.Enemies
{
    public class EnemyGameObjectSpawner : IGameStartListener, IGameObjectSpawner, IGameSimpleUpdateListener
    {
        private readonly EnemyPool _pool;
        private readonly EnemyInitializer _initializer;
        private readonly EnemySpawnerConfig _config;

        private int _currentEnemyCount = 0;
        private float _timeBeforeSpawn;
        private bool _startedSpawning;

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
            _startedSpawning = true;
        }

        public void Release(Enemy enemy)
        {
            _currentEnemyCount--;
            _pool.Release(enemy);
            ReleasedObject?.Invoke(enemy.gameObject);
        }

        void IGameSimpleUpdateListener.OnUpdate(float deltaTime)
        {
            if (_startedSpawning == false) return;
            
            _timeBeforeSpawn = Mathf.Max(0, _timeBeforeSpawn - deltaTime);
            
            if (_timeBeforeSpawn <= 0 && _currentEnemyCount < _config.Count)
            {
                Enemy enemy = _pool.Get();
                _initializer.Initialize(enemy);
                _currentEnemyCount++;
                Spawned?.Invoke(enemy);
                SpawnedObject?.Invoke(enemy.gameObject);

                _timeBeforeSpawn = _config.SpawnDelay;
            }
        }
    }
}