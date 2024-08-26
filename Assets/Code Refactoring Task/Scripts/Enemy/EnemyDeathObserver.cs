using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp.Enemies
{
    public sealed class EnemyDeathObserver : MonoBehaviour
    {
        private readonly HashSet<Enemy> _activeEnemies = new();

        [SerializeField] private EnemySpawner _spawner;       

        public event Action<Enemy> EnemyDied;

        private void OnEnable()
        {
            _spawner.Spawned += OnEnemySpawned;
        }

        private void OnDisable()
        {
            _spawner.Spawned -= OnEnemySpawned;
        }

        private void OnEnemySpawned(Enemy enemy)
        {
            if (_activeEnemies.Add(enemy))
                enemy.Character.HitPointsComponent.HitPointsEnded += OnEnemyDied;
        }

        private void OnEnemyDied(GameObject enemy)
        {
            if (enemy.TryGetComponent(out Enemy enemyComponent))
            {
                if (_activeEnemies.Remove(enemyComponent))
                {
                    enemyComponent.Character.HitPointsComponent.HitPointsEnded -= OnEnemyDied;
                    _spawner.Release(enemyComponent);
                    EnemyDied?.Invoke(enemyComponent);
                }
            }
        }
    }
}