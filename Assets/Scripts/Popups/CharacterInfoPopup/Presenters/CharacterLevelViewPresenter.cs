using PlayerData;
using UniRx;

namespace Popups.CharacterInfoPopup.Presenters
{
    public interface ICharacterExperienceViewPresenter
    {
        IReadOnlyReactiveProperty<float> XpGainPart { get; }
        IReadOnlyReactiveProperty<string> Experience { get; }
    }
    
    public class DefaultCharacterExperienceViewPresenter : ICharacterExperienceViewPresenter
    {
        private readonly PlayerLevel _playerLevel;
        private readonly FloatReactiveProperty _xpGainPart;
        private readonly StringReactiveProperty _currentXpValue;

        public DefaultCharacterExperienceViewPresenter(PlayerLevel playerLevel)
        {
            _playerLevel = playerLevel;

            _xpGainPart = 
                new FloatReactiveProperty(_playerLevel.CurrentExperience / (float)_playerLevel.RequiredExperience);
            _currentXpValue =
                new StringReactiveProperty($"{_playerLevel.CurrentExperience}/{_playerLevel.RequiredExperience} XP");

            _playerLevel.OnExperienceChanged += OnChangedExperience;
        }

        ~DefaultCharacterExperienceViewPresenter()
        {
            _playerLevel.OnExperienceChanged -= OnChangedExperience;
        }

        public IReadOnlyReactiveProperty<float> XpGainPart => _xpGainPart;
        public IReadOnlyReactiveProperty<string> Experience => _currentXpValue;

        private void OnChangedExperience(int _)
        {
            _xpGainPart.Value = _playerLevel.CurrentExperience / (float)_playerLevel.RequiredExperience;
            _currentXpValue.Value = $"{_playerLevel.CurrentExperience}/{_playerLevel.RequiredExperience}";
        }
    }
}