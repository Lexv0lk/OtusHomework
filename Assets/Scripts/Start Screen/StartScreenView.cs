using System;
using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp.StartScreen
{
    public class StartScreenView : MonoBehaviour
    {
        [SerializeField] private Button _startButton;

        public event Action StartButtonClicked;

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
            StartButtonClicked?.Invoke();
        }

        public void DisableStartButton()
        {
            _startButton.gameObject.SetActive(false);
        }

        public void EnableStartButton()
        {
            _startButton.gameObject.SetActive(true);
        }
    }
}