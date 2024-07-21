using System;
using System.Collections.Generic;
using ShootEmUp.Characters;
using Zenject;

namespace ShootEmUp.Enemies
{
    public sealed class EnemyDeathObserver : IInitializable, IDisposable
    {
        private readonly HashSet<Enemy> _activeEnemies = new();
        private readonly EnemySpawner _spawner;
        
        [Inject]
        public EnemyDeathObserver(EnemySpawner spawner)
        {
            _spawner = spawner;
        }
        
        public event Action<Enemy> EnemyDied;

        void IInitializable.Initialize()
        {
            _spawner.Spawned += OnEnemySpawned;
        }

        void IDisposable.Dispose()
        {
            _spawner.Spawned -= OnEnemySpawned;
        }

        private void OnEnemySpawned(Enemy enemy)
        {
            if (_activeEnemies.Add(enemy))
                enemy.Died += OnEnemyDied;
        }

        private void OnEnemyDied(Character enemy)
        {
            if (enemy.TryGetComponent(out Enemy enemyComponent))
            {
                if (_activeEnemies.Remove(enemyComponent))
                {
                    enemyComponent.Died -= OnEnemyDied;
                    _spawner.Release(enemyComponent);
                    EnemyDied?.Invoke(enemyComponent);
                }
            }
        }
    }
}