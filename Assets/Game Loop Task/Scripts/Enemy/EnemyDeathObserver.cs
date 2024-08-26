using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp.Enemies
{
    public sealed class EnemyDeathObserver : MonoBehaviour
    {
        private readonly HashSet<Enemy> _activeEnemies = new();

        [SerializeField] private EnemyGameObjectSpawner gameObjectSpawner;       

        public event Action<Enemy> EnemyDied;

        private void OnEnable()
        {
            gameObjectSpawner.Spawned += OnEnemySpawned;
        }

        private void OnDisable()
        {
            gameObjectSpawner.Spawned -= OnEnemySpawned;
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
                    gameObjectSpawner.Release(enemyComponent);
                    EnemyDied?.Invoke(enemyComponent);
                }
            }
        }
    }
}