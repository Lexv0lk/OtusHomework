using ShootEmUp.Characters;
using ShootEmUp.Components;
using ShootEmUp.GameStates;
using ShootEmUp.GameUpdate;
using ShootEmUp.Input;
using ShootEmUp.Level;
using ShootEmUp.Managers;
using ShootEmUp.PauseSystem;
using ShootEmUp.PauseSystem.UI;
using ShootEmUp.Player;
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

        private void InstallGameObservers()
        {
            Container.BindInterfacesAndSelfTo<GameFinishObserver>().AsSingle();
        }

        private void InstallObjectsOnScene()
        {
            Container.BindInterfacesAndSelfTo<LevelBounds>().FromInstance(_levelBounds);
            Container.BindInterfacesAndSelfTo<PausePlayButton>().FromInstance(_pausePlayButton);
        }

        private void InstallPauseSystem()
        {
            Container.BindInterfacesAndSelfTo<GamePauseController>().AsSingle();
        }

        private void InstallPlayer()
        {
            Container.BindInterfacesAndSelfTo<TeamComponent>().AsTransient();
            
            Container.BindInterfacesAndSelfTo<Character>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<PlayerDeathObserver>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerMoveController>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerShootController>().AsSingle();
        }
    }
}