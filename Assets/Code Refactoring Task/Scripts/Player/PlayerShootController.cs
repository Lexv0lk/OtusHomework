using ShootEmUp.Bullets;
using ShootEmUp.Input;
using UnityEngine;
using ShootEmUp.Characters;
using ShootEmUp.Components;

namespace ShootEmUp.Player
{
    public sealed class PlayerShootController : MonoBehaviour
    {
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private BulletShooter _bulletSystem;
        [SerializeField] private Character _player;

        private void Update()
        {
            if (_inputManager.IsAttackRequired())
                Shoot();
        }

        private void Shoot()
        {
            WeaponComponent weapon = _player.WeaponComponent;
            _bulletSystem.ShootBullet(weapon.GetShootArgs());
        }
    }
}