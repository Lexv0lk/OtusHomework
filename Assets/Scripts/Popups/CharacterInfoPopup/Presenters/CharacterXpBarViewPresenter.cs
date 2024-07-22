using Lessons.Architecture.PM;
using UniRx;

namespace Popups.CharacterInfoPopup.Presenters
{
    public interface ICharacterXpBarViewPresenter
    {
        IReadOnlyReactiveProperty<float> XpGainPart { get; }
        IReadOnlyReactiveProperty<string> CurrentXpValue { get; }
    }
    
    public class DefaultCharacterXpBarViewPresenter : ICharacterXpBarViewPresenter
    {
        private readonly PlayerLevel _playerLevel;
        private readonly FloatReactiveProperty _xpGainPart = new();
        private readonly StringReactiveProperty _currentXpValue = new();

        private readonly CompositeDisposable _subscriptions = new();

        public DefaultCharacterXpBarViewPresenter(PlayerLevel playerLevel)
        {
            _playerLevel = playerLevel;

            _playerLevel.CurrentExperience.Subscribe(OnChangedExperience).AddTo(_subscriptions);
            _playerLevel.CurrentLevel.SkipLatestValueOnSubscribe().Subscribe(OnChangedLevel).AddTo(_subscriptions);
        }

        ~DefaultCharacterXpBarViewPresenter()
        {
            _subscriptions.Dispose();
        }

        public IReadOnlyReactiveProperty<float> XpGainPart => _xpGainPart;
        public IReadOnlyReactiveProperty<string> CurrentXpValue => _currentXpValue;

        private void UpdateExperience()
        {
            _xpGainPart.Value = _playerLevel.CurrentExperience.Value / (float)_playerLevel.RequiredExperience;
            _currentXpValue.Value = $"{_playerLevel.CurrentExperience}/{_playerLevel.RequiredExperience}";
        }

        private void OnChangedExperience(int _) 
            => UpdateExperience();

        private void OnChangedLevel(int _) 
            => UpdateExperience();
    }
}