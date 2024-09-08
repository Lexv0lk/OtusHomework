using Controllers;
using UnityEngine;
using Zenject;

namespace DI
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<TeamViewSetup>().FromComponentInHierarchy().AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerInputController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TeamSetupController>().AsSingle().NonLazy();
        }
    }
}