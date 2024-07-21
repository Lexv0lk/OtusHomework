using System.Collections.Generic;
using Popups.CharacterInfoPopup.Presenters;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Popups.CharacterInfoPopup
{
    public class CharacterInfoPopupView : ReactiveView
    {
        private readonly List<CharacterStatView> _currentStats = new();
        
        [SerializeField] private TMP_Text _name;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _level;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Transform _statsRoot;
        
        [Header("Views")]
        [SerializeField] private CharacterXpBarView _xpBarView;

        [Header("Prefabs")] 
        [SerializeField] private CharacterStatView _statViewPrefab;

        private ICharacterInfoPopupPresenter _currentPresenter;

        public void Initialize(ICharacterInfoPopupPresenter presenter)
        {
            DisposeSubscriptions();

            _currentPresenter = presenter;
            SubscribeToPresenter(_currentPresenter);
            
            _xpBarView.Initialize(presenter.XpBarViewPresenter);
            CreateStats(presenter.StatViewPresenters);
        }

        private void CreateStats(IEnumerable<ICharacterStatViewPresenter> statPresenters)
        {
            for (int i = 0; i < _currentStats.Count; i++)
                Destroy(_currentStats[i].gameObject);
            
            _currentStats.Clear();

            foreach (var presenter in statPresenters)
            {
                var statView = Instantiate(_statViewPrefab, _statsRoot);
                statView.Initialize(presenter);
                _currentStats.Add(statView);
            }
        }

        private void SubscribeToPresenter(ICharacterInfoPopupPresenter presenter)
        {
            Subscriptions.Add(presenter.Name.Subscribe(OnNameChanged));
            Subscriptions.Add(presenter.Description.Subscribe(OnDescriptionChanged));
            Subscriptions.Add(presenter.Level.Subscribe(OnLevelChanged));
            Subscriptions.Add(presenter.Icon.Subscribe(OnIconChanged));
        }

        private void OnNameChanged(string newValue) 
            => _name.text = newValue;

        private void OnDescriptionChanged(string newValue) 
            => _description.text = newValue;

        private void OnLevelChanged(string newValue) 
            => _level.text = newValue;

        private void OnIconChanged(Sprite newValue) 
            => _icon.sprite = newValue;
    }
}