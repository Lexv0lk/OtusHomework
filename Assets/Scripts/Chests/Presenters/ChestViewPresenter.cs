using System;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Chests.Presenters
{
    public class ChestViewPresenter : IChestViewPresenter
    {
        private readonly Chest _chest;
        private readonly ChestsController _chestsController;
        private readonly CompositeDisposable _disposable = new();
        
        public string Name { get; }
        public Sprite Icon { get; }
        public ReactiveProperty<string> TimeLeft { get; }
        public ReactiveProperty<bool> CanOpen { get; }
        public ReactiveCommand OpenCommand { get; }
        
        public ChestViewPresenter(Chest chest, ChestsController chestsController)
        {
            _chest = chest;
            _chestsController = chestsController;
            
            Name = chest.Name;
            Icon = chest.Icon;
            TimeLeft = new ReactiveProperty<string>();
            CanOpen = new ReactiveProperty<bool>();
            OpenCommand = new ReactiveCommand();
            
            UpdateTimeLeft().Forget();
            OpenCommand.Subscribe(_ => chestsController.Open(chest)).AddTo(_disposable);
        }

        private async UniTask UpdateTimeLeft()
        {
            while (CanOpen.Value == false)
            {
                if (_chestsController.TryGetChestTimeLeft(_chest, out TimeSpan timeLeft))
                    TimeLeft.Value = timeLeft.ToString(@"hh\:mm\:ss");
                else
                    TimeLeft.Value = "Loading...";

                if (timeLeft == TimeSpan.Zero)
                    CanOpen.Value = true;

                await UniTask.Delay(1000, DelayType.Realtime);
            }
        }
        
        ~ChestViewPresenter()
        {
            _disposable.Dispose();
        }
    }
}