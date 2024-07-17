using System;
using ShootEmUp.GameStates;
using ShootEmUp.PauseSystem.UI;
using Zenject;

namespace ShootEmUp.PauseSystem
{
    public class GamePauseController : IInitializable, IDisposable
    {
        private readonly PausePlayButton _pauseButton;
        private readonly GameStateController _stateController;
        private readonly GameStateModel _stateModel;
        
        public GamePauseController(PausePlayButton pauseButton, GameStateController stateController, GameStateModel stateModel)
        {
            _pauseButton = pauseButton;
            _stateController = stateController;
            _stateModel = stateModel;
        }

        void IInitializable.Initialize()
        {
            _pauseButton.Clicked += OnButtonClicked;
        }

        void IDisposable.Dispose()
        {
            _pauseButton.Clicked -= OnButtonClicked;
        }

        private void OnButtonClicked()
        {
            if (_stateModel.CurrentState == GameState.PLAYING)
            {
                _stateController.PauseGame();
                _pauseButton.SetPlayIcon();
            }
            else if (_stateModel.CurrentState == GameState.PAUSED)
            {
                _stateController.ResumeGame();
                _pauseButton.SetPauseIcon();
            }
        }
    }
}