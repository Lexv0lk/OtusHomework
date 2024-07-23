using PlayerData;
using UniRx;

namespace Popups.CharacterInfoPopup.Presenters
{
    public interface ICharacterStatViewPresenter
    {
        string Name { get; }
        IReadOnlyReactiveProperty<string> Level { get; }
    }
    
    public class DefaultCharacterStatViewPresenter : ICharacterStatViewPresenter
    {
        private readonly CharacterStat _stat;
        private readonly StringReactiveProperty _level;

        public DefaultCharacterStatViewPresenter(CharacterStat stat)
        {
            _stat = stat;

            Name = _stat.Name;
            _level = new StringReactiveProperty(_stat.Value.ToString());

            _stat.OnValueChanged += OnStatValueChanged;
        }

        ~DefaultCharacterStatViewPresenter()
        {
            _stat.OnValueChanged -= OnStatValueChanged;
        }

        public string Name { get; }
        public IReadOnlyReactiveProperty<string> Level => _level;

        private void OnStatValueChanged(int newValue)
            => _level.Value = newValue.ToString();
    }
}