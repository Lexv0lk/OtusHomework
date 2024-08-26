namespace ShootEmUp.GameStates
{
    public interface IGameStateListener
    {
        
    }

    public interface IGameStartListener : IGameStateListener
    {
        void OnStart();
    }

    public interface IGameFinishListener : IGameStateListener
    {
        void OnFinish();
    }
    
    public interface IGamePauseListener : IGameStateListener
    {
        void OnPause();
    }

    public interface IGameResumeListener : IGameStateListener
    {
        void OnResume();
    }

    public interface IGameInitializeListener : IGameStateListener
    {
        void OnInitialize();
    }
}