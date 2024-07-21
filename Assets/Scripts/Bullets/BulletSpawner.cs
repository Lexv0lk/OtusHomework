using ShootEmUp.Common;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp.Bullets
{
    public sealed class BulletSpawner
    {
        private readonly HashSet<Bullet> _activeBullets = new();
        private readonly BulletPool _bulletPool;

        [Inject]
        public BulletSpawner(BulletPool pool)
        {
            _bulletPool = pool;
        }

        public event Action<Bullet> Spawned;
        public event Action<Bullet> Released;

        public void ShootBullet(TeamShootArgs args)
        {
            Bullet bullet = _bulletPool.Get();
            _activeBullets.Add(bullet);
            bullet.Init(args);
            Spawned?.Invoke(bullet);
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
        }
        
        public struct TeamShootArgs
        {
            public ShootArgs ShootArgs;
            public Team Team;
        }
    }
}