using ShootEmUp.Components;
using UnityEngine;
using Zenject;

namespace ShootEmUp.DI.Installers
{
    public class CharacterComponentsInstaller : MonoInstaller
    {
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private TeamComponent _teamComponent;
        [SerializeField] private WeaponComponent _weaponComponent;
        [SerializeField] private HitPointsComponent _hitPointsComponent;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MoveComponent>().FromInstance(_moveComponent);
            Container.BindInterfacesAndSelfTo<TeamComponent>().FromInstance(_teamComponent);
            Container.BindInterfacesAndSelfTo<WeaponComponent>().FromInstance(_weaponComponent);
            Container.BindInterfacesAndSelfTo<HitPointsComponent>().FromInstance(_hitPointsComponent);
        }
    }
}