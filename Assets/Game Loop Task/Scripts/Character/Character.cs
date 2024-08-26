using ShootEmUp.Components;
using UnityEngine;

namespace ShootEmUp.Characters
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private WeaponComponent _weaponComponent;
        [SerializeField] private HitPointsComponent _hitPointsComponent;
        [SerializeField] private TeamComponent _teamComponent;
        [SerializeField] private MoveComponent _moveComponent;

        public WeaponComponent WeaponComponent => _weaponComponent;
        public HitPointsComponent HitPointsComponent => _hitPointsComponent;
        public TeamComponent TeamComponent => _teamComponent;
        public MoveComponent MoveComponent => _moveComponent;
    }
}