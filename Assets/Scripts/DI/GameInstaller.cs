using Controllers;
using EventBus.Handlers.Logic;
using EventBus.Handlers.Visual;
using Models;
using Pipeline;
using Pipeline.Visual;
using UI;
using Zenject;

namespace DI
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<UIService>().FromComponentInHierarchy().AsSingle();
            Container.Bind<AudioPlayer>().FromComponentInHierarchy().AsSingle();
            
            Container.Bind<VisualPipeline>().AsSingle();
            Container.Bind<TurnPipeline>().AsSingle();
            Container.Bind<CurrentTurn>().AsSingle();
            Container.Bind<TeamsSetup>().AsSingle();

            Container.Bind<EventBus.EventBus>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<AttackEventHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TakeDamageEventHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<HealEventHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<DestroyEventHandler>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<StartTurnVisualHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AttackVisualHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TakeDamageVisualHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<HealVisualHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<DestroyVisualHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SpecialAbilityVisualHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<LowHealthVisualHandler>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<TeamSetupInstaller>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerInputController>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<TurnPipelineInstaller>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TurnPipelineStartController>().AsSingle().NonLazy();
        }
    }
}