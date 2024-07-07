using System.Collections.Generic;

namespace ShootEmUp.GameStates
{
    public interface IGameStateHandler
    {
        void Register(IGameStateListener listener);
        void Remove(IGameStateListener listener);
        void Handle(GameStateModel model);
    }

    public class GameStartHandler : IGameStateHandler
    {
        private readonly List<IGameStartListener> _listeners = new();

        public void Register(IGameStateListener listener)
        {
            if (listener is IGameStartListener startListener)
                _listeners.Add(startListener);
        }

        public void Remove(IGameStateListener listener)
        {
            if (listener is IGameStartListener startListener)
                _listeners.Remove(startListener);
        }

        public void Handle(GameStateModel model)
        {
            model.ChangeCurrentState(GameState.PLAYING);
            
            foreach (var listener in _listeners)
                listener.OnStart();
        }
    }
    
    public class GamePauseHandler : IGameStateHandler
    {
        private readonly List<IGamePauseListener> _listeners = new();

        public void Register(IGameStateListener listener)
        {
            if (listener is IGamePauseListener pauseListener)
                _listeners.Add(pauseListener);
        }

        public void Remove(IGameStateListener listener)
        {
            if (listener is IGamePauseListener pauseListener)
                _listeners.Remove(pauseListener);
        }

        public void Handle(GameStateModel model)
        {
            model.ChangeCurrentState(GameState.PAUSED);
            
            foreach (var listener in _listeners)
                listener.OnPause();
        }
    }
    
    public class GameResumeHandler : IGameStateHandler
    {
        private readonly List<IGameResumeListener> _listeners = new();

        public void Register(IGameStateListener listener)
        {
            if (listener is IGameResumeListener resumeListener)
                _listeners.Add(resumeListener);
        }

        public void Remove(IGameStateListener listener)
        {
            if (listener is IGameResumeListener resumeListener)
                _listeners.Remove(resumeListener);
        }

        public void Handle(GameStateModel model)
        {
            model.ChangeCurrentState(GameState.PLAYING);
            
            foreach (var listener in _listeners)
                listener.OnResume();
        }
    }
    
    public class GameFinishHandler : IGameStateHandler
    {
        private readonly List<IGameFinishListener> _listeners = new();

        public void Register(IGameStateListener listener)
        {
            if (listener is IGameFinishListener finishListener)
                _listeners.Add(finishListener);
        }

        public void Remove(IGameStateListener listener)
        {
            if (listener is IGameFinishListener finishListener)
                _listeners.Remove(finishListener);
        }

        public void Handle(GameStateModel model)
        {
            model.ChangeCurrentState(GameState.FINISHED);
            
            foreach (var listener in _listeners)
                listener.OnFinish();
        }
    }
    
    public class GameInitializeHandler : IGameStateHandler
    {
        private readonly List<IGameInitializeListener> _listeners = new();

        public void Register(IGameStateListener listener)
        {
            if (listener is IGameInitializeListener initializeListener)
                _listeners.Add(initializeListener);
        }

        public void Remove(IGameStateListener listener)
        {
            if (listener is IGameInitializeListener initializeListener)
                _listeners.Remove(initializeListener);
        }

        public void Handle(GameStateModel model)
        {
            foreach (var listener in _listeners)
                listener.OnInitialize();
        }
    }
}