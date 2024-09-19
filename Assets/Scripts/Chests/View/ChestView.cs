using Chests.Presenters;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Common;

namespace Chests.View
{
    public class ChestView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _timeLeft;
        [SerializeField] private Button _openButton;
        
        private IChestViewPresenter _presenter;
        private readonly CompositeDisposable _compositeDisposable = new();

        public void Initialize(IChestViewPresenter presenter)
        {
            _presenter = presenter;

            _name.text = presenter.Name;
            _icon.sprite = presenter.Icon;

            _presenter.OpenCommand.BindTo(_openButton).AddTo(_compositeDisposable);
            _presenter.TimeLeft.SubscribeToText(_timeLeft).AddTo(_compositeDisposable);
            _presenter.CanOpen.Subscribe(OnCanOpenChanged).AddTo(_compositeDisposable);
        }

        private void OnDestroy()
        {
            _compositeDisposable.Dispose();
        }

        private void OnCanOpenChanged(bool canOpen)
        {
            _openButton.gameObject.SetActive(canOpen);
        }
    }
}