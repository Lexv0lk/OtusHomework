using UniRx;
using UnityEngine;

namespace Chests.Presenters
{
    public interface IChestViewPresenter
    {
        string Name { get; }
        Sprite Icon { get; }
        ReactiveProperty<string> TimeLeft { get; }
        ReactiveProperty<bool> CanOpen { get; }
        ReactiveCommand OpenCommand { get; }
    }
}