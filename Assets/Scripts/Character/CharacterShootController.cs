using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterShootController : MonoBehaviour
    {
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private GameObject _character;

        private void FixedUpdate()
        {
            if (_inputManager.IsAttackRequired())
                Shoot();
        }

        private void Shoot()
        {
            var weapon = _character.GetComponent<WeaponComponent>();
            var teamComponent = _character.GetComponent<TeamComponent>();
            BulletSystem.Args initialArgs = weapon.GetFireArgs();
            initialArgs.Team = teamComponent.Team;
            _bulletSystem.FlyBulletByArgs(initialArgs);
        }
    }
}