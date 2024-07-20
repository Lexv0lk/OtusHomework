using System;
using ShootEmUp.Bullets;
using Zenject;

namespace ShootEmUp.Enemies
{
    public class EnemyShootController : IInitializable, IDisposable
    {
        private readonly EnemyGameObjectSpawner _gameObjectSpawner;
        private readonly EnemyDeathObserver _deathObserver;
        private readonly BulletSpawner _bulletSpawner;

        [Inject]
        public EnemyShootController(EnemyGameObjectSpawner gameObjectSpawner, EnemyDeathObserver deathObserver,
            BulletSpawner bulletSpawner)
        {
            _gameObjectSpawner = gameObjectSpawner;
            _deathObserver = deathObserver;
            _bulletSpawner = bulletSpawner;
        }

        void IInitializable.Initialize()
        {
            _gameObjectSpawner.Spawned += OnEnemySpawned;
            _deathObserver.EnemyDied += OnEnemyDied;
        }

        void IDisposable.Dispose()
        {
            _gameObjectSpawner.Spawned -= OnEnemySpawned;
            _deathObserver.EnemyDied -= OnEnemyDied;
        }

        private void OnEnemySpawned(Enemy enemy)
        {
            enemy.Attacked += OnEnemyAttacked;
        }

        private void OnEnemyDied(Enemy enemy)
        {
            enemy.Attacked -= OnEnemyAttacked;
        }

        private void OnEnemyAttacked(BulletSpawner.TeamShootArgs args)
        {
            _bulletSpawner.ShootBullet(args);
        }
    }
}