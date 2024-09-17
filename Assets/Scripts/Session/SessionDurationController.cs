using System;
using Cysharp.Threading.Tasks;
using Session.View;
using Time;
using Zenject;

namespace Session
{
    public class SessionDurationController : IInitializable
    {
        private readonly SessionDurationView _view;
        private readonly ServerTimeController _serverTimeController;

        private TimeSpan _currentDuration;

        public SessionDurationController(SessionDurationView view, ServerTimeController serverTimeController)
        {
            _view = view;
            _serverTimeController = serverTimeController;
        }
        
        public void Initialize()
        {
            DisplayTime().Forget();
        }

        private async UniTask DisplayTime()
        {
            while (true)
            {
                _view.SetText(_currentDuration.ToString(@"hh\:mm\:ss"));
                await UniTask.Delay(1000);
                _currentDuration = _currentDuration.Add(TimeSpan.FromSeconds(1));
            }
        }
    }
}