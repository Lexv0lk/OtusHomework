using GameEngine;
using UnityEngine;
using Zenject;

namespace DI
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Transform _unitContainer;

        public override void InstallBindings()
        {
            Container.Bind<Transform>().FromInstance(_unitContainer);
            Container.Bind<Resource>().FromComponentsInHierarchy().AsCached();
            Container.Bind<UnitManager>().AsSingle();
        }
    }
}