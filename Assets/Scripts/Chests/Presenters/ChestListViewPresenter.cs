using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Chests.Presenters
{
    public class ChestListViewPresenter : IChestListViewPresenter
    {
        private readonly CompositeDisposable _disposable = new();
        private readonly Dictionary<Chest, IChestViewPresenter> _chestPresenterConnections = new();
        private readonly ReactiveCollection<IChestViewPresenter> _chestPresenters = new();

        public IReadOnlyReactiveCollection<IChestViewPresenter> ChestPresenters => _chestPresenters;
        
        public ChestListViewPresenter(ChestsController chestsController)
        {
            chestsController.CurrentChests.ObserveAdd().Subscribe(x =>
            {
                Debug.Log("Presenter Add");
                var presenter = new ChestViewPresenter(x.Value, chestsController);
                _chestPresenters.Add(presenter);
                _chestPresenterConnections.Add(x.Value, presenter);
            }).AddTo(_disposable);
            
            chestsController.CurrentChests.ObserveRemove().Subscribe(x =>
            {
                var presenter = _chestPresenterConnections[x.Value];
                _chestPresenters.Remove(presenter);
                _chestPresenterConnections.Remove(x.Value);
            }).AddTo(_disposable);
        }
        
        ~ChestListViewPresenter()
        {
            _disposable.Dispose();
        }
    }
}