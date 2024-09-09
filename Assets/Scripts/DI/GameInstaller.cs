using Controllers;
using EventBus.Handlers.Visual;
using Models;
using Pipeline;
using Pipeline.Visual;
using Zenject;

namespace DI
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<TeamViewSetup>().FromComponentInHierarchy().AsSingle();
            Container.Bind<VisualPipeline>().AsSingle();
            Container.Bind<TurnPipeline>().AsSingle();
            Container.Bind<CurrentTurn>().AsSingle();

            Container.Bind<EventBus.EventBus>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<StartTurnVisualHandler>().FromNew().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<TeamSetupController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerInputController>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<TurnPipelineInstaller>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TurnPipelineStartController>().AsSingle().NonLazy();

        }
    }
}