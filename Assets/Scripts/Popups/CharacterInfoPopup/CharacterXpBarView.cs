using Popups.CharacterInfoPopup.Presenters;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Popups.CharacterInfoPopup
{
    public class CharacterXpBarView : ReactiveView
    {
        [SerializeField] private Slider _xpBar;
        [SerializeField] private Image _barForeground;
        [SerializeField] private TMP_Text _xpValue;
        
        private ICharacterXpBarViewPresenter _currentPresenter;

        public void Initialize(ICharacterXpBarViewPresenter presenter)
        {
            DisposeSubscriptions();
            
            _currentPresenter = presenter;
            SubscribeToPresenter(_currentPresenter);
        }

        private void SubscribeToPresenter(ICharacterXpBarViewPresenter presenter)
        {
            Subscriptions.Add(presenter.XpGainPart.Subscribe(UpdateSliderValue));
            Subscriptions.Add(presenter.CurrentXpValue.Subscribe(UpdateXpValue));
            Subscriptions.Add(presenter.CurrentSliderForeground.Subscribe(UpdateBarForeground));
        }

        private void UpdateXpValue(string newValue)
        {
            _xpValue.text = newValue;
        }

        private void UpdateSliderValue(float newValue)
        {
            _xpBar.value = newValue;
        }

        private void UpdateBarForeground(Sprite newForeground)
        {
            _barForeground.sprite = newForeground;
        }
    }
}