using ShootEmUp.Bullets;
using UnityEngine;

namespace ShootEmUp.Components
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        [SerializeField] private Transform _firePoint;
        [SerializeField] private BulletConfig _bulletConfig;
        [SerializeField] private TeamComponent _teamComponent;

        public BulletSystem.Args GetFireArgs()
        {
            return new BulletSystem.Args
            {
                Team = _teamComponent.Team,
                PhysicsLayer = (int)_bulletConfig.PhysicsLayer,
                Color = _bulletConfig.Color,
                Damage = _bulletConfig.Damage,
                Position = _firePoint.transform.position,
                Velocity = _firePoint.transform.rotation * Vector3.up * _bulletConfig.Speed
            };
        }   
        
        public BulletSystem.Args GetFireArgsAtTarget(Vector2 targetPosition)
        {
            BulletSystem.Args args = GetFireArgs();
            args.Velocity = ((Vector2)targetPosition - args.Position).normalized * _bulletConfig.Speed;
            return args;
        }
    }
}