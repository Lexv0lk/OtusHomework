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
        
        public void SetupLastTime(DateTime startTime, DateTime endTime)
        {
            _view.SetData(startTime.ToString("dd.MM.yyyy HH:mm:ss"), endTime.ToString("dd.MM.yyyy HH:mm:ss"));
        }
    }
}