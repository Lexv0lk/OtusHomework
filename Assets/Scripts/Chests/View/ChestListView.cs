using System.Collections.Generic;
using Chests.Presenters;
using UniRx;
using UnityEngine;

namespace Chests.View
{
    public class ChestListView : MonoBehaviour
    {
        [SerializeField] private ChestView _chestViewPrefab;
        [SerializeField] private Transform _chestViewsRoot;
        
        private readonly CompositeDisposable _compositeDisposable = new();
        private readonly Dictionary<IChestViewPresenter, ChestView> _chestViews = new();
        
        private IChestListViewPresenter _presenter;
        
        public void Initialize(IChestListViewPresenter presenter)
        {
            _presenter = presenter;

            _presenter.ChestPresenters.ObserveAdd().Subscribe(x =>
            {
                Debug.Log("Chest View Add");
                var chestView = Instantiate(_chestViewPrefab, _chestViewsRoot);
                chestView.Initialize(x.Value);
                _chestViews.Add(x.Value, chestView);
            }).AddTo(_compositeDisposable);
            
            _presenter.ChestPresenters.ObserveRemove().Subscribe(x =>
            {
                var chestView = _chestViews[x.Value];
                _chestViews.Remove(x.Value);
                Destroy(chestView.gameObject);
            }).AddTo(_compositeDisposable);
        }

        private void OnDestroy()
        {
            _compositeDisposable.Dispose();
        }
    }
}