using System;
using System.Globalization;
using Cysharp.Threading.Tasks;
using DI.Contexts;
using SaveSystem.SaveLoaders;
using Session.View;
using Time;
using Zenject;

namespace Session
{
    public class SessionLogTimeController : IInitializable, IGameService
    {
        private readonly ServerTimeController _serverTimeController;
        private readonly SessionLogTimeView _view;

        public DateTime CurrentEnterTime { get; private set; }
        public DateTime CurrentExitTime { get; private set; }

        public SessionLogTimeController(ServerTimeController serverTimeController, SessionLogTimeView view)
        {
            _serverTimeController = serverTimeController;
            _view = view;
        }

        public void Initialize()
        {
            SetEnterTimeAsync().Forget();
        }

        private async UniTask SetEnterTimeAsync()
        {
            while (_serverTimeController.IsActualTimeReceived == false)
                await UniTask.Delay(1000, DelayType.Realtime);
            
            CurrentEnterTime = _serverTimeController.GetCurrentTime();
        }
        
        public void SetLastTime(SessionTimeSave timeSave)
        {
            if (string.IsNullOrEmpty(timeSave.StartTime) || string.IsNullOrEmpty(timeSave.EndTime))
                return;
            
            var enterTime = DateTime.Parse(timeSave.StartTime);
            var exitTime = DateTime.Parse(timeSave.EndTime);
            _view.SetData(enterTime.ToString("dd.MM.yyyy HH:mm:ss"), exitTime.ToString("dd.MM.yyyy HH:mm:ss"));
        }

        public SessionTimeSave GetCurrentTime()
        {
            var enterTime = CurrentEnterTime;
            var exitTime = _serverTimeController.GetCurrentTime();

            return new SessionTimeSave
            {
                StartTime = enterTime.ToString(),
                EndTime = exitTime.ToString()
            };
        }
    }
}