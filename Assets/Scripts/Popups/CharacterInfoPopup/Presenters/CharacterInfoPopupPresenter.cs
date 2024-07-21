using System.Collections.Generic;
using UniRx;
using UnityEngine;

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
        
    }
}