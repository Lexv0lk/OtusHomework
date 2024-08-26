using ShootEmUp.Bullets;
using UnityEngine;

namespace ShootEmUp.Components
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        [SerializeField] private Transform _firePoint;
        [SerializeField] private BulletConfig _bulletConfig;
        [SerializeField] private TeamComponent _teamComponent;

        public BulletShooter.ShootArgs GetShootArgs()
        {
            return new BulletShooter.ShootArgs
            {
                Team = _teamComponent.Team,
                PhysicsLayer = (int)_bulletConfig.PhysicsLayer,
                Color = _bulletConfig.Color,
                Damage = _bulletConfig.Damage,
                Position = _firePoint.transform.position,
                Velocity = _firePoint.transform.rotation * Vector3.up * _bulletConfig.Speed
            };
        }   
        
        public BulletShooter.ShootArgs GetShootArgs(Vector2 targetPosition)
        {
            BulletShooter.ShootArgs args = GetShootArgs();
            args.Velocity = ((Vector2)targetPosition - args.Position).normalized * _bulletConfig.Speed;
            return args;
        }
    }
}