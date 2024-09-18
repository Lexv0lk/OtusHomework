using System;
using Session;
using Time;

namespace SaveSystem.SaveLoaders
{
    public class LogTimeSaveLoader : SaveLoader<SessionTimeSave, SessionLogTimeController>
    {
        private readonly ServerTimeController _serverTimeController;

        public LogTimeSaveLoader(ServerTimeController serverTimeController)
        {
            _serverTimeController = serverTimeController;
        }
        
        protected override SessionTimeSave GetSaveData(SessionLogTimeController service)
        {
            return new SessionTimeSave
            {
                StartTime = service.CurrentEnterTime.ToString(),
                EndTime = _serverTimeController.GetCurrentTime().ToString()
            };
        }

        protected override void SetSaveData(SessionLogTimeController service, SessionTimeSave data)
        {
            if (string.IsNullOrEmpty(data.StartTime) || string.IsNullOrEmpty(data.EndTime))
                return;
            
            service.SetupLastTime(DateTime.Parse(data.StartTime), DateTime.Parse(data.EndTime));
        }
    }
}