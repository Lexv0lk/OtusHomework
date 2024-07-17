using ShootEmUp.Components;
using UnityEngine;
using Zenject;

namespace ShootEmUp.Characters
{
    public class Character : MonoBehaviour
    {
        private readonly WeaponComponent _weaponComponent;
        private readonly HitPointsComponent _hitPointsComponent;
        private readonly TeamComponent _teamComponent;
        private readonly MoveComponent _moveComponent;
        
        [Inject]
        public Character(WeaponComponent weaponComponent, HitPointsComponent hitPointsComponent,
            TeamComponent teamComponent, MoveComponent moveComponent)
        {
            _weaponComponent = weaponComponent;
            _hitPointsComponent = hitPointsComponent;
            _teamComponent = teamComponent;
            _moveComponent = moveComponent;
        }

        public WeaponComponent WeaponComponent => _weaponComponent;
        public HitPointsComponent HitPointsComponent => _hitPointsComponent;
        public TeamComponent TeamComponent => _teamComponent;
        public MoveComponent MoveComponent => _moveComponent;
    }
}