using Atomic.Objects;
using Game.Scripts.Controllers;
using Game.Scripts.Fabrics;
using Game.Scripts.Models;
using UnityEngine;
using Zenject;

namespace Game.Scripts.DI
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private AtomicEntity _character;
        
        public override void InstallBindings()
        {
            Container.Bind<RiffleStoreModel>().FromNew().AsSingle();
            
            Container.Bind<Camera>().FromComponentInHierarchy().AsCached();
            
            Container.BindInterfacesTo<CreatingBulletFabric>().AsCached();
            
            Container.BindInterfacesAndSelfTo<IAtomicEntity>().FromInstance(_character).AsCached();
            
            Container.BindInterfacesAndSelfTo<InputMoveController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<InputMouseRotateController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ShootController>().AsSingle().NonLazy();
            Container.Bind<PlayerDeathObserver>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AmmunitionRefillController>().AsSingle().NonLazy();
        }
    }
}