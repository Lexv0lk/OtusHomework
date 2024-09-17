using Session;

namespace SaveSystem.SaveLoaders
{
    public class LogTimeSaveLoader : SaveLoader<SessionTimeSave, SessionLogTimeController>
    {
        protected override SessionTimeSave GetSaveData(SessionLogTimeController service)
        {
            return service.GetCurrentTime();
        }

        protected override void SetSaveData(SessionLogTimeController service, SessionTimeSave data)
        {
            service.SetLastTime(data);
        }
    }
}