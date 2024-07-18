using ShootEmUp.Bullets;
using ShootEmUp.Input;
using ShootEmUp.Characters;
using ShootEmUp.Components;
using ShootEmUp.GameUpdate;
using Zenject;

namespace ShootEmUp.Player
{
    public sealed class PlayerShootController : IGameSimpleUpdateListener
    {
        private InputManager _inputManager;
        private BulletSpawner _bulletSpawner;
        private Character _player;

        [Inject]
        public PlayerShootController(InputManager inputManager, BulletSpawner bulletSpawner, Character player)
        {
            _inputManager = inputManager;
            _bulletSpawner = bulletSpawner;
            _player = player;
        }

        private void Shoot()
        {
            WeaponComponent weapon = _player.WeaponComponent;
            _bulletSpawner.ShootBullet(weapon.GetShootArgs());
        }

        void IGameSimpleUpdateListener.OnUpdate(float deltaTime)
        {
            if (_inputManager.IsAttackRequired())
                Shoot();
        }
    }
}