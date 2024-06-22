using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour
    {
        [SerializeField] private GameObject character; 
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private BulletConfig _bulletConfig;
        [SerializeField] private TeamComponent _teamComponent;
        
        public bool _fireRequired;

        private void FixedUpdate()
        {
            if (this._fireRequired)
            {
                this.OnFlyBullet();
                this._fireRequired = false;
            }
        }

        private void OnFlyBullet()
        {
            var weapon = this.character.GetComponent<WeaponComponent>();
            BulletSystem.Args initialArgs = weapon.GetFireArgs();
            initialArgs.Team = _teamComponent.Team;
            _bulletSystem.FlyBulletByArgs(initialArgs);
        }
    }
}