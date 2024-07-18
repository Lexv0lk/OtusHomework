using ShootEmUp.Bullets;
using ShootEmUp.Characters;
using ShootEmUp.Components;
using ShootEmUp.Enemies;
using ShootEmUp.GameStates;
using ShootEmUp.GameUpdate;
using ShootEmUp.Input;
using ShootEmUp.Level;
using ShootEmUp.Managers;
using ShootEmUp.PauseSystem;
using ShootEmUp.PauseSystem.UI;
using ShootEmUp.Player;
using ShootEmUp.StartScreen;
using ShootEmUp.StartScreen.UI;
using UnityEngine;
using Zenject;

namespace ShootEmUp.DI.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameListenersInstaller _gameListenersInstaller;
        [SerializeField] private InputConfig _inputConfig;
        [SerializeField] private LevelBounds _levelBounds;
        [SerializeField] private PausePlayButton _pausePlayButton;
        [SerializeField] private Character _player;
        [SerializeField] private BulletPool _bulletPool;
        [SerializeField] private EnemyPositions _enemyPositions;
        [SerializeField] private EnemySpawnerConfig _enemySpawnerConfig;
        [SerializeField] private StartScreenView _startScreenView;
        [SerializeField] private StartTimerView _startTimerView;
        
        public override void InstallBindings()
        {
            _gameListenersInstaller.InstallBindings();
            InstallInputSystem();
        }

        private void InstallGameStates()
        {
            Container.BindInterfacesAndSelfTo<GameStateModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameStateController>().AsSingle();
        }

        private void InstallGameUpdate()
        {
            Container.BindInterfacesAndSelfTo<GameUpdateController>().AsSingle();
        }

        private void InstallInputSystem()
        {
            Container.BindInterfacesAndSelfTo<InputConfig>().FromInstance(_inputConfig).AsSingle();
            Container.BindInterfacesAndSelfTo<InputManager>().AsSingle();
        }

        private void InstallControllers()
        {
            Container.BindInterfacesAndSelfTo<GameFinishObserver>().AsSingle();
            Container.BindInterfacesAndSelfTo<BulletLevelBoundsWatcher>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<GamePauseController>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<PlayerDeathObserver>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerMoveController>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerShootController>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<BulletCollisionObserver>().AsSingle();

            Container.BindInterfacesAndSelfTo<EnemyDeathObserver>().AsSingle();

            Container.BindInterfacesAndSelfTo<StartTimerController>().AsSingle();
        }

        private void InstallObjectsOnScene()
        {
            Container.BindInterfacesAndSelfTo<LevelBounds>().FromInstance(_levelBounds);
            Container.BindInterfacesAndSelfTo<PausePlayButton>().FromInstance(_pausePlayButton);
            Container.BindInterfacesAndSelfTo<Character>().FromInstance(_player).AsCached();
            Container.BindInterfacesAndSelfTo<EnemyPositions>().FromInstance(_enemyPositions);
            Container.BindInterfacesAndSelfTo<EnemySpawnerConfig>().FromInstance(_enemySpawnerConfig);
            Container.BindInterfacesAndSelfTo<StartTimerView>().FromInstance(_startTimerView);
            Container.BindInterfacesAndSelfTo<StartScreenView>().FromInstance(_startScreenView);
        }

        private void InstallConfigs()
        {
            Container.BindInterfacesAndSelfTo<EnemySpawnerConfig>();
        }

        private void InstallPools()
        {
            Container.BindInterfacesAndSelfTo<BulletPool>().FromInstance(_bulletPool);
        }

        private void InstallBulletSystem()
        {
            Container.BindInterfacesAndSelfTo<BulletSpawner>().AsSingle();
        }

        private void InstalEnemiesSystem()
        {
            Container.BindInterfacesAndSelfTo<EnemyInitializer>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyGameObjectSpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyShootController>().AsSingle();
        }
    }
}