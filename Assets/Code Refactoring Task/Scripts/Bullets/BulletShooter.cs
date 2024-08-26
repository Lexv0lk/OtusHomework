using ShootEmUp.Common;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp.Bullets
{
    public sealed class BulletShooter : MonoBehaviour
    {
        private readonly HashSet<Bullet> _activeBullets = new();

        [SerializeField] private BulletPool _bulletPool;
        [SerializeField] private BulletLevelBoundsWatcher _bulletBoundsWatcher;

        public event Action<Bullet> Shooted;
        public event Action<Bullet> Released;

        private void OnEnable()
        {
            _bulletBoundsWatcher.WentOutOfBounds += ReleaseBullet;
        }

        private void OnDisable()
        {
            _bulletBoundsWatcher.WentOutOfBounds -= ReleaseBullet;
        }

        public void ShootBullet(ShootArgs args)
        {
            Bullet bullet = _bulletPool.Get();
            _activeBullets.Add(bullet);
            bullet.Init(args);
            _bulletBoundsWatcher.AddTargetToWatch(bullet);
            Shooted?.Invoke(bullet);
        }

        public void ReleaseBullet(Bullet bullet)
        {
            if (_activeBullets.Remove(bullet))
            {
                _bulletPool.Release(bullet);
                Released?.Invoke(bullet);
            }
        }
        
        public struct ShootArgs
        {
            public Vector2 Position;
            public Vector2 Velocity;
            public Color Color;
            public int PhysicsLayer;
            public int Damage;
            public Team Team;
        }
    }
}