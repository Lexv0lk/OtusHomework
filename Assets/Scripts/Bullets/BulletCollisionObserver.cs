using System;
using ShootEmUp.Bullets;
using UnityEngine;
using Zenject;

public class BulletCollisionObserver : IInitializable, IDisposable
{
    private readonly BulletSpawner _spawner;

    [Inject]
    public BulletCollisionObserver(BulletSpawner spawner)
    {
        _spawner = spawner;
    }

    void IInitializable.Initialize()
    {
        _spawner.Spawned += OnBulletSpawned;
        _spawner.Released += OnBulletReleased;
    }

    void IDisposable.Dispose()
    {
        _spawner.Spawned -= OnBulletSpawned;
        _spawner.Released -= OnBulletReleased;
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
        _spawner.ReleaseBullet(bullet);
    }
}