using Atomic.Objects;
using Game.Scripts.Controllers;
using UnityEngine;
using Zenject;

namespace Game.Scripts.DI
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private AtomicEntity _character;
        
        public override void InstallBindings()
        {
            Container.Bind<Camera>().FromComponentInHierarchy().AsCached();
            
            Container.BindInterfacesAndSelfTo<IAtomicEntity>().FromInstance(_character).AsCached();
            Container.BindInterfacesAndSelfTo<InputMoveController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<InputMouseRotateController>().AsSingle().NonLazy();
        }
    }
}