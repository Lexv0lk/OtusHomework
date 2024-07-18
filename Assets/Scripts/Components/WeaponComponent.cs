using System;
using ShootEmUp.Bullets;
using UnityEngine;

namespace ShootEmUp.Components
{
    [Serializable]
    public sealed class WeaponComponent
    {
        [SerializeField] private Transform _firePoint;
        [SerializeField] private BulletConfig _bulletConfig;
        [SerializeField] private TeamComponent _teamComponent;

        public BulletSpawner.ShootArgs GetShootArgs()
        {
            return new BulletSpawner.ShootArgs
            {
                Team = _teamComponent.Team,
                PhysicsLayer = (int)_bulletConfig.PhysicsLayer,
                Color = _bulletConfig.Color,
                Damage = _bulletConfig.Damage,
                Position = _firePoint.transform.position,
                Velocity = _firePoint.transform.rotation * Vector3.up * _bulletConfig.Speed
            };
        }   
        
        public BulletSpawner.ShootArgs GetShootArgs(Vector2 targetPosition)
        {
            BulletSpawner.ShootArgs args = GetShootArgs();
            args.Velocity = ((Vector2)targetPosition - args.Position).normalized * _bulletConfig.Speed;
            return args;
        }
    }
}