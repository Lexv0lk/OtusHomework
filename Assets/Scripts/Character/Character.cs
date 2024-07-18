using System;
using ShootEmUp.Components;
using UnityEngine;

namespace ShootEmUp.Characters
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private WeaponComponent _weaponComponent;
        [SerializeField] private HitPointsComponent _hitPointsComponent;
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private TeamComponent _teamComponent;

        public WeaponComponent WeaponComponent => _weaponComponent;
        public HitPointsComponent HitPointsComponent => _hitPointsComponent;
        public TeamComponent TeamComponent => _teamComponent;
        public MoveComponent MoveComponent => _moveComponent;

        public event Action<Character> Died;

        private void OnEnable()
        {
            _hitPointsComponent.HitPointsEnded += OnHealthEnded;
        }

        private void OnDisable()
        {
            _hitPointsComponent.HitPointsEnded -= OnHealthEnded;
        }

        private void OnHealthEnded()
        {
            Died?.Invoke(this);
        }
    }
}