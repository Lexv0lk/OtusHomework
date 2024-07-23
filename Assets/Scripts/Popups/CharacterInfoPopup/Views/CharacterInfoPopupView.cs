using System;
using System.Collections.Generic;
using Popups.CharacterInfoPopup.Presenters;
using Popups.Common;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Popups.CharacterInfoPopup.Views
{
    public class CharacterInfoPopupView : ReactiveView
    {
        private readonly List<CharacterStatView> _currentStats = new();
        
        [SerializeField] private TMP_Text _name;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Transform _statsRoot;
        [SerializeField] private TMP_Text _level;

        [Header("Buttons")] 
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _levelUpButton;

        [Header("Views")]
        [SerializeField] private CharacterExperienceView experienceView;

        [Header("Prefabs")] 
        [SerializeField] private CharacterStatView _statViewPrefab;

        [Header("Sprites")] 
        [SerializeField] private Sprite _inactiveLevelUpButtonSprite;
        [SerializeField] private Sprite _activeLevelUpButtonSprite;

        private ICharacterInfoPopupPresenter _currentPresenter;

        public event Action CloseButtonClicked;

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(OnCloseButtonClicked);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(OnCloseButtonClicked);
        }

        public void Initialize(ICharacterInfoPopupPresenter presenter)
        {
            DisposeSubscriptions();

            _currentPresenter = presenter;
            SubscribeToPresenter(_currentPresenter);
            
            experienceView.Initialize(presenter.ExperienceViewPresenter);
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
            presenter.Icon.Subscribe(OnIconChanged).AddTo(Subscriptions);
            presenter.Level.Subscribe(UpdateLevelValue).AddTo(Subscriptions);
            presenter.CanLevelUp.Subscribe(OnLevelUpConditionChanged).AddTo(Subscriptions);

            presenter.StatViewPresenters.ObserveAdd().Subscribe(OnStatAdded).AddTo(Subscriptions);
            presenter.StatViewPresenters.ObserveRemove().Subscribe(OnStatRemoved).AddTo(Subscriptions);
            
            presenter.LevelUpCommand.BindTo(_levelUpButton).AddTo(Subscriptions);
        }
        
        #region COLLECTION_HANDLERS

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

        private void OnIconChanged(Sprite newValue) 
            => _icon.sprite = newValue;
        
        private void UpdateLevelValue(string level) 
            => _level.text = level;

        private void OnLevelUpConditionChanged(bool canLevelUp)
        {
            if (canLevelUp)
                _levelUpButton.image.sprite = _activeLevelUpButtonSprite;
            else
                _levelUpButton.image.sprite = _inactiveLevelUpButtonSprite;
        }

        #endregion

        private void OnCloseButtonClicked()
        {
            CloseButtonClicked?.Invoke();
        }
    }
}