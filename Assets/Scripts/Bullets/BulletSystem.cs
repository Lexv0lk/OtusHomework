using ShootEmUp.Common;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp.Bullets
{
    public sealed class BulletSystem : MonoBehaviour
    {
        private readonly HashSet<Bullet> _activeBullets = new();

        [SerializeField] private BulletPool _bulletPool;
        [SerializeField] private BulletLevelBoundsWatcher _bulletBoundsWatcher;

        public void SendBulletByArgs(Args args)
        {
            Bullet bullet = _bulletPool.Get();
            bullet.Init(args);
            _bulletBoundsWatcher.AddTargetToWatch(bullet);
            
            if (_activeBullets.Add(bullet))
                bullet.CollisionEntered += OnBulletCollision;
        }
        
        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            BulletUtils.DealDamage(bullet, collision.gameObject);
            RemoveBullet(bullet);
        }

        private void RemoveBullet(Bullet bullet)
        {
            if (_activeBullets.Remove(bullet))
            {
                bullet.CollisionEntered -= this.OnBulletCollision;
                _bulletPool.Release(bullet);
            }
        }
        
        public struct Args
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