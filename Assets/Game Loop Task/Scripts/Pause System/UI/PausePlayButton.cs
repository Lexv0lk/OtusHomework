using System;
using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp.PauseSystem.UI
{
    public class PausePlayButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _icon;
        [SerializeField] private Sprite _pauseIcon;
        [SerializeField] private Sprite _playIcon;

        public event Action Clicked;

        private void Start()
        {
            _icon.sprite = _pauseIcon;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClicked);
        }

        public void SetPlayIcon()
        {
            _icon.sprite = _playIcon;
        }

        public void SetPauseIcon()
        {
            _icon.sprite = _pauseIcon;
        }

        private void OnClicked()
        {
            Clicked?.Invoke();
        }
    }
}