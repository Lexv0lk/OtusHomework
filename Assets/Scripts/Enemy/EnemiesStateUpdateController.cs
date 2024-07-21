using System.Collections.Generic;
using ShootEmUp.GameUpdate;
using Zenject;

namespace ShootEmUp.Enemies
{
    public sealed class EnemiesStateUpdateController : IGameFixedUpdateListener
    {
        private readonly EnemySpawner _spawner;
        private readonly HashSet<Enemy> _activeEnemies = new();

        [Inject]
        public EnemiesStateUpdateController(EnemySpawner spawner)
        {
            _spawner = spawner;
            
            _spawner.Spawned += AddEnemy;
            _spawner.Released += RemoveEnemy;
        }

        ~EnemiesStateUpdateController()
        {
            _spawner.Spawned -= AddEnemy;
            _spawner.Released -= RemoveEnemy;
        }

        private void AddEnemy(Enemy enemy)
        {
            _activeEnemies.Add(enemy);
        }

        private void RemoveEnemy(Enemy enemy)
        {
            _activeEnemies.Remove(enemy);
        }

        void IGameFixedUpdateListener.OnFixedUpdate(float fixedDeltaTime)
        {
            foreach (var enemy in _activeEnemies)
                enemy.OnFixedUpdate(fixedDeltaTime);
        }
    }
}