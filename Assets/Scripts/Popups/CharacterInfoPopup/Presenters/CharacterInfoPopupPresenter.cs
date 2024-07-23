using System.Collections.Generic;
using PlayerData;
using UniRx;
using UnityEngine;
using CharacterInfo = PlayerData.CharacterInfo;

namespace Popups.CharacterInfoPopup.Presenters
{
    public interface ICharacterInfoPopupPresenter
    {
        IReadOnlyReactiveCollection<ICharacterStatViewPresenter> StatViewPresenters { get; }
        ICharacterExperienceViewPresenter ExperienceViewPresenter { get; }

        IReadOnlyReactiveProperty<string> Name { get; }
        IReadOnlyReactiveProperty<string> Description { get; }
        IReadOnlyReactiveProperty<string> Level { get; }
        IReadOnlyReactiveProperty<Sprite> Icon { get; }
        IReadOnlyReactiveProperty<bool> CanLevelUp { get; }

        ReactiveCommand LevelUpCommand { get; }
    }
    
    public class DefaultCharacterInfoPopupPresenter : ICharacterInfoPopupPresenter
    {
        private readonly UserInfo _userInfo;
        private readonly CharacterInfo _characterInfo;
        private readonly PlayerLevel _playerLevel;

        private readonly ICharacterExperienceViewPresenter _experienceViewPresenter;
        
        private readonly ReactiveCollection<ICharacterStatViewPresenter> _statsPresenters = new();
        private readonly Dictionary<CharacterStat, ICharacterStatViewPresenter> _statPresenterConnections = new();

        private readonly StringReactiveProperty _name;
        private readonly StringReactiveProperty _description;
        private readonly StringReactiveProperty _level;
        private readonly ReactiveProperty<Sprite> _icon;
        private readonly BoolReactiveProperty _canLevelUp;
        private readonly ReactiveCommand _levelUpCommand;

        private readonly CompositeDisposable _subscriptions = new();

        public IReadOnlyReactiveCollection<ICharacterStatViewPresenter> StatViewPresenters => _statsPresenters;
        public ICharacterExperienceViewPresenter ExperienceViewPresenter => _experienceViewPresenter;
        public IReadOnlyReactiveProperty<string> Name => _name;
        public IReadOnlyReactiveProperty<string> Description => _description;
        public IReadOnlyReactiveProperty<string> Level => _level;
        public IReadOnlyReactiveProperty<Sprite> Icon => _icon;
        public IReadOnlyReactiveProperty<bool> CanLevelUp => _canLevelUp;
        public ReactiveCommand LevelUpCommand => _levelUpCommand;

        public DefaultCharacterInfoPopupPresenter(UserInfo userInfo, CharacterInfo characterInfo, PlayerLevel playerLevel)
        {
            _userInfo = userInfo;
            _characterInfo = characterInfo;
            _playerLevel = playerLevel;

            _name = new StringReactiveProperty(_userInfo.Name);
            _description = new StringReactiveProperty(_userInfo.Description);
            _icon = new ReactiveProperty<Sprite>(_userInfo.Icon);
            _level = new StringReactiveProperty($"Level: {_playerLevel.CurrentLevel.ToString()}");

            _canLevelUp = new BoolReactiveProperty(playerLevel.CanLevelUp());
            _levelUpCommand = new ReactiveCommand(_canLevelUp);

            _levelUpCommand.Subscribe(OnLevelUpCommand).AddTo(_subscriptions);

            _userInfo.OnNameChanged += OnNameChanged;
            _userInfo.OnDescriptionChanged += OnDescriptionChanged;
            _userInfo.OnIconChanged += OnIconChanged;
            _characterInfo.OnStatAdded += AddStatPresenter;
            _characterInfo.OnStatRemoved += RemoveStatPresenter;
            _playerLevel.OnLevelUp += OnChangedLevel;
            _playerLevel.OnExperienceChanged += OnChangedExperience;

            _experienceViewPresenter = new DefaultCharacterExperienceViewPresenter(playerLevel);
        }

        ~DefaultCharacterInfoPopupPresenter()
        {
            _userInfo.OnNameChanged -= OnNameChanged;
            _userInfo.OnDescriptionChanged -= OnDescriptionChanged;
            _userInfo.OnIconChanged -= OnIconChanged;
            _characterInfo.OnStatAdded -= AddStatPresenter;
            _characterInfo.OnStatRemoved -= RemoveStatPresenter;
            _playerLevel.OnLevelUp -= OnChangedLevel;
            _playerLevel.OnExperienceChanged -= OnChangedExperience;

            _subscriptions.Dispose();
        }

        private void OnLevelUpCommand(Unit unit)
        {
            _playerLevel.LevelUp();
        }

        private void AddStatPresenter(CharacterStat stat)
        {
            var statPresenter = new DefaultCharacterStatViewPresenter(stat);
            _statsPresenters.Add(statPresenter);
            _statPresenterConnections.Add(stat, statPresenter);
        }

        private void RemoveStatPresenter(CharacterStat stat)
        {
            var presenter = _statPresenterConnections[stat];
            _statsPresenters.Remove(presenter);
            _statPresenterConnections.Remove(stat);
        }
        
        private void OnNameChanged(string newName) 
            => _name.Value = $"@{newName}";
        
        private void OnDescriptionChanged(string newDescription) 
            => _description.Value = newDescription;
        
        private void OnIconChanged(Sprite newIcon) 
            => _icon.Value = newIcon;
        
        private void OnChangedLevel() 
            => _level.Value = $"Level: {_playerLevel.CurrentLevel}";

        private void OnChangedExperience(int newValue) 
            => _canLevelUp.Value = _playerLevel.CanLevelUp();
    }
}