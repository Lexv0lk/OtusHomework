using UnityEngine;

namespace ShootEmUp
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        [SerializeField] private Transform _firePoint;
        [SerializeField] private BulletConfig _bulletConfig;

        public BulletSystem.Args GetFireArgs()
        {
            return new BulletSystem.Args
            {
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