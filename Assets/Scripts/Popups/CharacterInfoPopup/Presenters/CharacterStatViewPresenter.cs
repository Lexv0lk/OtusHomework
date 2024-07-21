using UniRx;

namespace Popups.CharacterInfoPopup.Presenters
{
    public interface ICharacterStatViewPresenter
    {
        string Name { get; }
        ReactiveProperty<string> Level { get; }
    }
    
    public class DefaultCharacterStatViewPresenter : ICharacterStatViewPresenter
    {
        
    }
}