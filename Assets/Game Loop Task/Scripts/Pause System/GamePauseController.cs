using ShootEmUp.GameStates;
using ShootEmUp.PauseSystem.UI;
using UnityEngine;

namespace ShootEmUp.PauseSystem
{
    public class GamePauseController : MonoBehaviour
    {
        [SerializeField] private PausePlayButton _pauseButton;
        [SerializeField] private GameStateController _stateController;
        [SerializeField] private GameStateModel _stateModel;

        private void OnEnable()
        {
            _pauseButton.Clicked += OnButtonClicked;
        }

        private void OnDisable()
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