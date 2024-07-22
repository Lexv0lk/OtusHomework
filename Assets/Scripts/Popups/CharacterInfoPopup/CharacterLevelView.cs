using Popups.CharacterInfoPopup.Presenters;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Popups.CharacterInfoPopup
{
    public class CharacterLevelView : ReactiveView
    {
        [SerializeField] private Slider _xpBar;
        [SerializeField] private Image _barForeground;
        [SerializeField] private TMP_Text _xpValue;
        [SerializeField] private TMP_Text _level;

        [Header("Sprites")] 
        [SerializeField] private Sprite _notFilledSprite;
        [SerializeField] private Sprite _filledSprite;
        
        public void Initialize(ICharacterLevelViewPresenter presenter)
        {
            DisposeSubscriptions();
            SubscribeToPresenter(presenter);
        }

        private void SubscribeToPresenter(ICharacterLevelViewPresenter presenter)
        {
            presenter.XpGainPart.Subscribe(UpdateSliderValue).AddTo(Subscriptions);
            presenter.Experience.Subscribe(UpdateXpValue).AddTo(Subscriptions);
            presenter.Level.Subscribe(UpdateLevelValue).AddTo(Subscriptions);
        }

        private void UpdateXpValue(string newValue)
        {
            _xpValue.text = newValue;
        }

        private void UpdateSliderValue(float newValue)
        {
            _xpBar.value = newValue;

            if (newValue < 1)
                _barForeground.sprite = _notFilledSprite;
            else
                _barForeground.sprite = _filledSprite;
        }

        private void UpdateLevelValue(string level)
        {
            _level.text = level;
        }
    }
}