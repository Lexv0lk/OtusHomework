using ShootEmUp.Bullets;
using UnityEngine;

namespace ShootEmUp.Enemies
{
    public class EnemyShootController : MonoBehaviour
    {
        [SerializeField] private EnemySpawner _spawner;
        [SerializeField] private EnemyDeathObserver _deathObserver;
        [SerializeField] private BulletShooter _bulletSystem;

        private void OnEnable()
        {
            _spawner.Spawned += OnEnemySpawned;
            _deathObserver.EnemyDied += OnEnemyDied;
        }

        private void OnDisable()
        {
            _spawner.Spawned -= OnEnemySpawned;
            _deathObserver.EnemyDied -= OnEnemyDied;
        }

        private void OnEnemySpawned(Enemy enemy)
        {
            enemy.AttackAgent.Fired += OnEnemyFired;
        }

        private void OnEnemyDied(Enemy enemy)
        {
            enemy.AttackAgent.Fired -= OnEnemyFired;
        }

        private void OnEnemyFired(BulletShooter.ShootArgs args)
        {
            _bulletSystem.ShootBullet(args);
        }
    }
}