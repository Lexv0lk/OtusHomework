using Lessons.Architecture.PM;
using UniRx;

namespace Popups.CharacterInfoPopup.Presenters
{
    public interface ICharacterLevelViewPresenter
    {
        IReadOnlyReactiveProperty<float> XpGainPart { get; }
        IReadOnlyReactiveProperty<string> Experience { get; }
        IReadOnlyReactiveProperty<string> Level { get; }
    }
    
    public class DefaultCharacterLevelViewPresenter : ICharacterLevelViewPresenter
    {
        private readonly PlayerLevel _playerLevel;
        private readonly FloatReactiveProperty _xpGainPart;
        private readonly StringReactiveProperty _currentXpValue;
        private readonly StringReactiveProperty _level;

        public DefaultCharacterLevelViewPresenter(PlayerLevel playerLevel)
        {
            _playerLevel = playerLevel;

            _level = new StringReactiveProperty($"Level: {_playerLevel.CurrentLevel}");
            _xpGainPart = 
                new FloatReactiveProperty(_playerLevel.CurrentExperience / (float)_playerLevel.RequiredExperience);
            _currentXpValue =
                new StringReactiveProperty($"{_playerLevel.CurrentExperience}/{_playerLevel.RequiredExperience}");

            _playerLevel.OnLevelUp += OnChangedLevel;
            _playerLevel.OnExperienceChanged += OnChangedExperience;
        }

        ~DefaultCharacterLevelViewPresenter()
        {
            _playerLevel.OnLevelUp -= OnChangedLevel;
            _playerLevel.OnExperienceChanged -= OnChangedExperience;
        }

        public IReadOnlyReactiveProperty<float> XpGainPart => _xpGainPart;
        public IReadOnlyReactiveProperty<string> Experience => _currentXpValue;
        public IReadOnlyReactiveProperty<string> Level => _level;

        private void OnChangedExperience(int _)
        {
            _xpGainPart.Value = _playerLevel.CurrentExperience / (float)_playerLevel.RequiredExperience;
            _currentXpValue.Value = $"{_playerLevel.CurrentExperience}/{_playerLevel.RequiredExperience}";
        }

        private void OnChangedLevel()
        {
            _level.Value = $"Level: {_playerLevel.CurrentLevel}";
        }
    }
}