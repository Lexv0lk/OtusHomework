using DI.Contexts;
using SaveSystem;
using SaveSystem.DataSaving;
using SaveSystem.SaveLoaders;
using Session;
using Session.View;
using Time;
using Zenject;

namespace DI
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SessionDurationView>().FromComponentInHierarchy().AsCached();
            Container.Bind<SessionLogTimeView>().FromComponentInHierarchy().AsCached();
            
            Container.BindInterfacesAndSelfTo<ServerTimeController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SessionDurationController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SessionLogTimeController>().AsSingle().NonLazy();
            
            BindSaveContext();
        }

        private void BindSaveContext()
        {
            Container.Bind<GameContext>().AsSingle();
            
            Container.Bind<IDataSaver>().To<FileSaver>().AsSingle();
            Container.Bind<GameRepository>().AsSingle();

            Container.BindInterfacesAndSelfTo<LogTimeSaveLoader>().AsCached();

            Container.BindInterfacesAndSelfTo<SaveLoadingController>().AsSingle().NonLazy();
        }
    }
}