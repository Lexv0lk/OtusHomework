using UniRx;
using UnityEngine;

namespace Popups.CharacterInfoPopup.Presenters
{
    public interface ICharacterXpBarViewPresenter
    {
        IReadOnlyReactiveProperty<float> XpGainPart { get; }
        IReadOnlyReactiveProperty<string> CurrentXpValue { get; }
        IReadOnlyReactiveProperty<Sprite> CurrentSliderForeground { get; }
    }
    
    public class DefaultCharacterXpBarViewPresenter : ICharacterXpBarViewPresenter
    {
        
    }
}