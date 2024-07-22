using System.Collections.Generic;
using Lessons.Architecture.PM;
using UniRx;
using UnityEngine;
using CharacterInfo = Lessons.Architecture.PM.CharacterInfo;

namespace Popups.CharacterInfoPopup.Presenters
{
    public interface ICharacterInfoPopupPresenter
    {
        IReadOnlyReactiveCollection<ICharacterStatViewPresenter> StatViewPresenters { get; }
        ICharacterXpBarViewPresenter XpBarViewPresenter { get; }

        IReadOnlyReactiveProperty<string> Name { get; }
        IReadOnlyReactiveProperty<string> Level { get; }
        IReadOnlyReactiveProperty<string> Description { get; }
        IReadOnlyReactiveProperty<Sprite> Icon { get; }
    }
    
    public class DefaultCharacterInfoPopupPresenter : ICharacterInfoPopupPresenter
    {
        private readonly CharacterInfo _characterInfo;

        private readonly ICharacterXpBarViewPresenter _xpBarViewPresenter;
        private readonly ReactiveCollection<ICharacterStatViewPresenter> _statsPresenters = new();
        
        private readonly StringReactiveProperty _name;
        private readonly StringReactiveProperty _level;
        private readonly StringReactiveProperty _description;
        private readonly ReactiveProperty<Sprite> _icon;
        
        private readonly CompositeDisposable _subscriptions = new();
        
        public IReadOnlyReactiveCollection<ICharacterStatViewPresenter> StatViewPresenters => _statsPresenters;
        public ICharacterXpBarViewPresenter XpBarViewPresenter => _xpBarViewPresenter;
        public IReadOnlyReactiveProperty<string> Name => _name;
        public IReadOnlyReactiveProperty<string> Level => _level;
        public IReadOnlyReactiveProperty<string> Description => _description;
        public IReadOnlyReactiveProperty<Sprite> Icon => _icon;

        public DefaultCharacterInfoPopupPresenter(UserInfo userInfo, CharacterInfo characterInfo, PlayerLevel playerLevel)
        {
            _characterInfo = characterInfo;

            _name = userInfo.Name;
            _level = new StringReactiveProperty($"Level: {playerLevel.CurrentLevel}");
            _description = userInfo.Description;
            _icon = userInfo.Icon;

            _xpBarViewPresenter = new DefaultCharacterXpBarViewPresenter(playerLevel);

            playerLevel.CurrentLevel.Subscribe(OnLevelChanged).AddTo(_subscriptions);

            characterInfo.Stats.ObserveAdd().Subscribe(OnStatAdded).AddTo(_subscriptions);
            characterInfo.Stats.ObserveRemove().Subscribe(OnStatRemoved).AddTo(_subscriptions);
            characterInfo.Stats.ObserveReplace().Subscribe(OnStatReplaced).AddTo(_subscriptions);
            characterInfo.Stats.ObserveMove().Subscribe(OnStatMoved).AddTo(_subscriptions);
        }

        ~DefaultCharacterInfoPopupPresenter()
        {
            _subscriptions.Dispose();
        }

        private void UpdateStatsPresenters()
        {
            _statsPresenters.Clear();

            foreach (CharacterStat characterStat in _characterInfo.Stats)
                _statsPresenters.Add(new DefaultCharacterStatViewPresenter(characterStat));
        }

        #region PROPERTIES_HANDLERS

        private void OnLevelChanged(int newLevel) 
            => _level.Value = $"Level: {newLevel}";

        #endregion

        #region COLLECTION_HANDLERS

        private void OnStatMoved(CollectionMoveEvent<CharacterStat> moveEvent) 
            => UpdateStatsPresenters();

        private void OnStatReplaced(CollectionReplaceEvent<CharacterStat> replaceEvent) 
            => UpdateStatsPresenters();

        private void OnStatRemoved(CollectionRemoveEvent<CharacterStat> removeEvent) 
            => UpdateStatsPresenters();

        private void OnStatAdded(CollectionAddEvent<CharacterStat> addEvent) 
            => _statsPresenters.Add(new DefaultCharacterStatViewPresenter(addEvent.Value));

        #endregion
    }
}