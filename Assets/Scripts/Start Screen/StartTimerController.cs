using System;
using Cysharp.Threading.Tasks;
using ShootEmUp.GameStates;
using ShootEmUp.StartScreen.UI;
using Zenject;

namespace ShootEmUp.StartScreen
{
    public class StartTimerController : IInitializable, IDisposable
    {
        private readonly StartTimerView _timerView;
        private readonly GameStateController _gameStateController;
        private readonly StartScreenView _startScreen;
        private readonly StartScreenConfig _startScreenConfig;
        
        [Inject]
        public StartTimerController(StartTimerView timerView, GameStateController gameStateController,
            StartScreenView startScreen, StartScreenConfig startScreenConfig)
        {
            _timerView = timerView;
            _gameStateController = gameStateController;
            _startScreen = startScreen;
            _startScreenConfig = startScreenConfig;
        }

        void IInitializable.Initialize()
        {
            _startScreen.StartButtonClicked += OnStartButtonClicked;
        }

        void IDisposable.Dispose()
        {
            _startScreen.StartButtonClicked -= OnStartButtonClicked;
        }

        private void OnStartButtonClicked()
        {
            _startScreen.DisableStartButton();
            StartGameAsync().Forget();
        }

        private async UniTaskVoid StartGameAsync()
        {
            _timerView.gameObject.SetActive(true);
            await _timerView.AnimateTimeAsync(_startScreenConfig.DelayBeforeStart);
            _timerView.gameObject.SetActive(false);
            
            _gameStateController.StartGame();
        }
    }
}