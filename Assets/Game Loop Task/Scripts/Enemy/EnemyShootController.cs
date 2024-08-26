using ShootEmUp.Bullets;
using UnityEngine;

namespace ShootEmUp.Enemies
{
    public class EnemyShootController : MonoBehaviour
    {
        [SerializeField] private EnemyGameObjectSpawner gameObjectSpawner;
        [SerializeField] private EnemyDeathObserver _deathObserver;
        [SerializeField] private BulletSpawner _bulletSystem;

        private void OnEnable()
        {
            gameObjectSpawner.Spawned += OnEnemySpawned;
            _deathObserver.EnemyDied += OnEnemyDied;
        }

        private void OnDisable()
        {
            gameObjectSpawner.Spawned -= OnEnemySpawned;
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

        private void OnEnemyFired(BulletSpawner.ShootArgs args)
        {
            _bulletSystem.ShootBullet(args);
        }
    }
}