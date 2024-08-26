using ShootEmUp.Bullets;
using ShootEmUp.Input;
using UnityEngine;
using ShootEmUp.Characters;
using ShootEmUp.Components;
using ShootEmUp.GameUpdate;

namespace ShootEmUp.Player
{
    public sealed class PlayerShootController : MonoBehaviour, IGameSimpleUpdateListener
    {
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private BulletSpawner _bulletSystem;
        [SerializeField] private Character _player;

        private void Shoot()
        {
            WeaponComponent weapon = _player.WeaponComponent;
            _bulletSystem.ShootBullet(weapon.GetShootArgs());
        }

        void IGameSimpleUpdateListener.OnUpdate(float deltaTime)
        {
            if (_inputManager.IsAttackRequired())
                Shoot();
        }
    }
}