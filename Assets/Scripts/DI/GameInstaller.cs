using DI.Contexts;
using Game;
using Game.Units;
using GameEngine;
using SaveSystem;
using SaveSystem.DataSaving;
using SaveSystem.SaveLoaders;
using UnityEngine;
using Zenject;

namespace DI
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Transform _unitContainer;

        public override void InstallBindings()
        {
            Container.Bind<Transform>().FromInstance(_unitContainer).AsCached();
            Container.Bind<Resource>().FromComponentsInHierarchy().AsCached();
            Container.Bind<Unit>().FromComponentsInHierarchy().AsCached();
            
            Container.BindInterfacesAndSelfTo<UnitManager>().AsCached();
            Container.BindInterfacesAndSelfTo<ResourceService>().AsCached();

            Container.Bind<GameContext>().AsSingle();

            Container.BindInterfacesAndSelfTo<EncryptedFileSaver>().AsSingle();
            Container.Bind<GameRepository>().AsSingle();
            Container.Bind<UnitPrefabController>().AsSingle();

            Container.BindInterfacesAndSelfTo<UnitsSaveLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<ResourcesSaveLoader>().AsSingle();

            Container.Bind<SaveLoadingController>().AsSingle().NonLazy();

            Container.Bind<SaveLoadingEditorController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<UnitCountControllerMock>().FromComponentInHierarchy().AsSingle();
        }
    }
}