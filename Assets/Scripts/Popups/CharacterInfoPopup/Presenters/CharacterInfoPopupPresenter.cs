using System.Collections.Generic;
using System.Linq;
using Lessons.Architecture.PM;
using UniRx;
using UnityEngine;
using CharacterInfo = Lessons.Architecture.PM.CharacterInfo;

namespace Popups.CharacterInfoPopup.Presenters
{
    public interface ICharacterInfoPopupPresenter
    {
        IReadOnlyReactiveCollection<ICharacterStatViewPresenter> StatViewPresenters { get; }
        ICharacterLevelViewPresenter LevelViewPresenter { get; }

        IReadOnlyReactiveProperty<string> Name { get; }
        IReadOnlyReactiveProperty<string> Description { get; }
        IReadOnlyReactiveProperty<Sprite> Icon { get; }
    }
    
    public class DefaultCharacterInfoPopupPresenter : ICharacterInfoPopupPresenter
    {
        private readonly UserInfo _userInfo;
        private readonly CharacterInfo _characterInfo;

        private readonly ICharacterLevelViewPresenter _levelViewPresenter;
        
        private readonly ReactiveCollection<ICharacterStatViewPresenter> _statsPresenters = new();
        private readonly Dictionary<CharacterStat, ICharacterStatViewPresenter> _statPresenterConnections = new();

        private readonly StringReactiveProperty _name;
        private readonly StringReactiveProperty _description;
        private readonly ReactiveProperty<Sprite> _icon;
        
        public IReadOnlyReactiveCollection<ICharacterStatViewPresenter> StatViewPresenters => _statsPresenters;
        public ICharacterLevelViewPresenter LevelViewPresenter => _levelViewPresenter;
        public IReadOnlyReactiveProperty<string> Name => _name;
        public IReadOnlyReactiveProperty<string> Description => _description;
        public IReadOnlyReactiveProperty<Sprite> Icon => _icon;

        public DefaultCharacterInfoPopupPresenter(UserInfo userInfo, CharacterInfo characterInfo, PlayerLevel playerLevel)
        {
            _userInfo = userInfo;
            _characterInfo = characterInfo;

            _name = new StringReactiveProperty(_userInfo.Name);
            _description = new StringReactiveProperty(_userInfo.Description);
            _icon = new ReactiveProperty<Sprite>(_userInfo.Icon);

            _userInfo.OnNameChanged += OnNameChanged;
            _userInfo.OnDescriptionChanged += OnDescriptionChanged;
            _userInfo.OnIconChanged += OnIconChanged;
            _characterInfo.OnStatAdded += AddStatPresenter;
            _characterInfo.OnStatRemoved += RemoveStatPresenter;

            _levelViewPresenter = new DefaultCharacterLevelViewPresenter(playerLevel);
        }

        ~DefaultCharacterInfoPopupPresenter()
        {
            _userInfo.OnNameChanged -= OnNameChanged;
            _userInfo.OnDescriptionChanged -= OnDescriptionChanged;
            _userInfo.OnIconChanged -= OnIconChanged;
            _characterInfo.OnStatAdded -= AddStatPresenter;
            _characterInfo.OnStatRemoved -= RemoveStatPresenter;
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
    }
}