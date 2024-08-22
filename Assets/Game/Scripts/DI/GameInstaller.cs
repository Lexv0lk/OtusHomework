using Atomic.Objects;
using Game.Scripts.Controllers;
using Game.Scripts.Fabrics;
using Game.Scripts.Models;
using Game.Scripts.Pools;
using Game.Scripts.Utilities;
using UnityEngine;
using Zenject;

namespace Game.Scripts.DI
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private AtomicEntity _character;
        [SerializeField] private GamePools _gamePools;
        [SerializeField] private EnemySpawnPositions _enemySpawnPositions;
        
        public override void InstallBindings()
        {
            Container.Bind<GamePools>().FromInstance(_gamePools).AsSingle();
            
            Container.Bind<EnemySpawnPositions>().FromInstance(_enemySpawnPositions).AsSingle();
            Container.Bind<RiffleStoreModel>().FromNew().AsSingle();
            
            Container.Bind<Camera>().FromComponentInHierarchy().AsCached();
            
            Container.BindInterfacesTo<PoolBulletFabric>().AsSingle();

            Container.BindInterfacesAndSelfTo<AtomicEntity>().FromInstance(_character).AsCached();
            
            Container.BindInterfacesAndSelfTo<InputMoveController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<InputMouseRotateController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ShootController>().AsSingle().NonLazy();
            Container.Bind<PlayerDeathObserver>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AmmunitionRefillController>().AsSingle().NonLazy();
            Container.Bind<BulletPoolReleaseController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EnemySpawnController>().AsSingle().NonLazy();
            Container.Bind<EnemyDeathObserver>().AsSingle().NonLazy();
        }
    }
}