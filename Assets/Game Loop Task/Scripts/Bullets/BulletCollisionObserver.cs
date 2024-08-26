using ShootEmUp.Bullets;
using UnityEngine;

public class BulletCollisionObserver : MonoBehaviour
{
    [SerializeField] private BulletSpawner spawner;
    [SerializeField] private BulletPool _pool;

    private void OnEnable()
    {
        spawner.Spawned += OnBulletSpawned;
        spawner.Released += OnBulletReleased;
    }

    private void OnDisable()
    {
        spawner.Spawned -= OnBulletSpawned;
        spawner.Released -= OnBulletReleased;
    }

    private void OnBulletSpawned(Bullet bullet)
    {
        bullet.CollisionEntered += OnBulletCollision;
    }

    private void OnBulletReleased(Bullet bullet)
    {
        bullet.CollisionEntered -= OnBulletCollision;
    }

    private void OnBulletCollision(Bullet bullet, Collision2D collision)
    {
        BulletUtils.DealDamage(bullet, collision.gameObject);
        spawner.ReleaseBullet(bullet);
    }
}