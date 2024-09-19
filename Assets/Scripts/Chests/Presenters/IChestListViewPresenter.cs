using UniRx;

namespace Chests.Presenters
{
    public interface IChestListViewPresenter
    {
        IReadOnlyReactiveCollection<IChestViewPresenter> ChestPresenters { get; }
    }
}