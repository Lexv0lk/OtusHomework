using Lessons.Architecture.PM;
using UniRx;

namespace Popups.CharacterInfoPopup.Presenters
{
    public interface ICharacterStatViewPresenter
    {
        IReadOnlyReactiveProperty<string> Name { get; }
        IReadOnlyReactiveProperty<string> Level { get; }
    }
    
    public class DefaultCharacterStatViewPresenter : ICharacterStatViewPresenter
    {
        private readonly StringReactiveProperty _name;
        private readonly StringReactiveProperty _level;

        public DefaultCharacterStatViewPresenter(CharacterStat stat)
        {
            _name = stat.Name;
            _level = new StringReactiveProperty(stat.Level.Value.ToString());
        }

        public IReadOnlyReactiveProperty<string> Name => _name;
        public IReadOnlyReactiveProperty<string> Level => _level;
    }
}