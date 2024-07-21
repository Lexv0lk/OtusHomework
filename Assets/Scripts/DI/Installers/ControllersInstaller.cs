using ShootEmUp.Bullets;
using ShootEmUp.Enemies;
using ShootEmUp.GameInitialization;
using ShootEmUp.GameStates;
using ShootEmUp.GameUpdate;
using ShootEmUp.Input;
using ShootEmUp.Managers;
using ShootEmUp.PauseSystem;
using ShootEmUp.Player;
using ShootEmUp.StartScreen;
using Zenject;

namespace ShootEmUp.DI.Installers
{
    public class ControllersInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputManager>().AsSingle();

            Container.Bind<GameStateModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameStateController>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameUpdateController>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<GameFinishObserver>().AsSingle();
            Container.BindInterfacesAndSelfTo<GamePauseController>().AsSingle();
            Container.Bind<GameListenersController>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<PlayerMoveController>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerShootController>().AsSingle();
            Container.Bind<PlayerDeathObserver>().AsSingle().NonLazy();
            
            Container.Bind<BulletSpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<BulletCollisionObserver>().AsSingle();
            Container.BindInterfacesAndSelfTo<BulletLevelBoundsWatcher>().AsSingle();
            Container.BindInterfacesAndSelfTo<BulletsStateUpdateController>().AsSingle().NonLazy();

            Container.Bind<EnemyInitializer>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyDeathObserver>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyShootController>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemiesStateUpdateController>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<StartTimerController>().AsSingle();
        }
    }
}