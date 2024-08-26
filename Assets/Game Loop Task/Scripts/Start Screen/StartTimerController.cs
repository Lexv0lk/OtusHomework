using System;
using Cysharp.Threading.Tasks;
using ShootEmUp.GameStates;
using ShootEmUp.StartScreen.UI;
using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp.StartScreen
{
    public class StartTimerController : MonoBehaviour
    {
        [SerializeField] private StartTimerView _timerView;
        [SerializeField] private GameStateController _gameStateController;
        [SerializeField] private Button _startButton;
        [SerializeField] private int _delayBeforeStart = 3;

        private void OnEnable()
        {
            _startButton.onClick.AddListener(OnStartButtonClicked);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(OnStartButtonClicked);
        }

        private void OnStartButtonClicked()
        {
            _startButton.gameObject.SetActive(false);
            StartGameAsync().Forget();
        }

        private async UniTaskVoid StartGameAsync()
        {
            _timerView.gameObject.SetActive(true);
            await _timerView.AnimateTimeAsync(_delayBeforeStart);
            _timerView.gameObject.SetActive(false);
            
            _gameStateController.StartGame();
        }
    }
}