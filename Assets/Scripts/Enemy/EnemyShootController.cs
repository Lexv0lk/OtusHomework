using System;
using ShootEmUp.Bullets;
using Zenject;

namespace ShootEmUp.Enemies
{
    public class EnemyShootController : IInitializable, IDisposable
    {
        private readonly EnemySpawner _spawner;
        private readonly EnemyDeathObserver _deathObserver;
        private readonly BulletSpawner _bulletSpawner;

        [Inject]
        public EnemyShootController(EnemySpawner spawner, EnemyDeathObserver deathObserver,
            BulletSpawner bulletSpawner)
        {
            _spawner = spawner;
            _deathObserver = deathObserver;
            _bulletSpawner = bulletSpawner;
        }

        void IInitializable.Initialize()
        {
            _spawner.Spawned += OnEnemySpawned;
            _deathObserver.EnemyDied += OnEnemyDied;
        }

        void IDisposable.Dispose()
        {
            _spawner.Spawned -= OnEnemySpawned;
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