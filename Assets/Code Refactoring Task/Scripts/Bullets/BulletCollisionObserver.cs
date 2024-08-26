using ShootEmUp.Bullets;
using UnityEngine;

public class BulletCollisionObserver : MonoBehaviour
{
    [SerializeField] private BulletShooter _shooter;
    [SerializeField] private BulletPool _pool;

    private void OnEnable()
    {
        _shooter.Shooted += OnBulletShooted;
        _shooter.Released += OnBulletReleased;
    }

    private void OnDisable()
    {
        _shooter.Shooted -= OnBulletShooted;
        _shooter.Released -= OnBulletReleased;
    }

    private void OnBulletShooted(Bullet bullet)
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
        _shooter.ReleaseBullet(bullet);
    }
}