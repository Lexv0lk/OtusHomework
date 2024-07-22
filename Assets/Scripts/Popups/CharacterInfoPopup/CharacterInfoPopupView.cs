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
        [SerializeField] private Button _levelUpButton;
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
            UpdateStats(presenter.StatViewPresenters);
        }

        private void UpdateStats(IEnumerable<ICharacterStatViewPresenter> statPresenters)
        {
            for (int i = 0; i < _currentStats.Count; i++)
                Destroy(_currentStats[i].gameObject);
            
            _currentStats.Clear();

            foreach (var presenter in statPresenters)
                AddStatView(presenter);
        }

        private void AddStatView(ICharacterStatViewPresenter presenter)
        {
            var statView = Instantiate(_statViewPrefab, _statsRoot);
            statView.Initialize(presenter);
            _currentStats.Add(statView);
        }

        private void SubscribeToPresenter(ICharacterInfoPopupPresenter presenter)
        {
            presenter.Name.Subscribe(OnNameChanged).AddTo(Subscriptions);
            presenter.Description.Subscribe(OnDescriptionChanged).AddTo(Subscriptions);
            presenter.Level.Subscribe(OnLevelChanged).AddTo(Subscriptions);
            presenter.Icon.Subscribe(OnIconChanged).AddTo(Subscriptions);
            
            presenter.StatViewPresenters.ObserveAdd().Subscribe(OnStatAdded).AddTo(Subscriptions);
            presenter.StatViewPresenters.ObserveRemove().Subscribe(OnStatRemoved).AddTo(Subscriptions);
            presenter.StatViewPresenters.ObserveReplace().Subscribe(OnStatReplaced).AddTo(Subscriptions);
            presenter.StatViewPresenters.ObserveMove().Subscribe(OnStatMoved).AddTo(Subscriptions);
        }
        
        #region COLLECTION_HANDLERS

        private void OnStatMoved(CollectionMoveEvent<ICharacterStatViewPresenter> moveEvent) 
            => UpdateStats(_currentPresenter.StatViewPresenters);

        private void OnStatReplaced(CollectionReplaceEvent<ICharacterStatViewPresenter> replaceEvent) 
            => UpdateStats(_currentPresenter.StatViewPresenters);

        private void OnStatRemoved(CollectionRemoveEvent<ICharacterStatViewPresenter> removeEvent) 
            => UpdateStats(_currentPresenter.StatViewPresenters);

        private void OnStatAdded(CollectionAddEvent<ICharacterStatViewPresenter> addEvent) 
            => AddStatView(addEvent.Value);

        #endregion

        #region PROPERTIES_HANDLERS

        private void OnNameChanged(string newValue) 
            => _name.text = newValue;

        private void OnDescriptionChanged(string newValue) 
            => _description.text = newValue;

        private void OnLevelChanged(string newValue) 
            => _level.text = newValue;

        private void OnIconChanged(Sprite newValue) 
            => _icon.sprite = newValue;

        #endregion
    }
}