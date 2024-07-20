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

            Container.BindInterfacesAndSelfTo<GameStateModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameStateController>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameUpdateController>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<GameFinishObserver>().AsSingle();
            Container.BindInterfacesAndSelfTo<GamePauseController>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameListenersController>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<PlayerDeathObserver>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerMoveController>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerShootController>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<BulletSpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<BulletCollisionObserver>().AsSingle();
            Container.BindInterfacesAndSelfTo<BulletLevelBoundsWatcher>().AsSingle();

            Container.BindInterfacesAndSelfTo<EnemyInitializer>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyGameObjectSpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyDeathObserver>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyShootController>().AsSingle();

            Container.BindInterfacesAndSelfTo<StartTimerController>().AsSingle();
        }
    }
}