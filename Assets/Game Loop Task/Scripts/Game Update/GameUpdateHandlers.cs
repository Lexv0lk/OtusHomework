using System.Collections.Generic;

namespace ShootEmUp.GameUpdate
{
    public interface IGameUpdateHandler
    {
        void Register(IGameUpdateListener listener);
        void Remove(IGameUpdateListener listener);
        void Handle(float deltaTime);
    }

    public class GameSimpleUpdateHandler : IGameUpdateHandler
    {
        private readonly List<IGameSimpleUpdateListener> _listeners = new();

        public void Register(IGameUpdateListener listener)
        {
            if (listener is IGameSimpleUpdateListener simpleUpdateListener)
                _listeners.Add(simpleUpdateListener);
        }

        public void Remove(IGameUpdateListener listener)
        {
            if (listener is IGameSimpleUpdateListener simpleUpdateListener)
                _listeners.Remove(simpleUpdateListener);
        }

        public void Handle(float deltaTime)
        {
            foreach (var listener in _listeners)
                listener.OnUpdate(deltaTime);
        }
    }
    
    public class GameFixedUpdateHandler : IGameUpdateHandler
    {
        private readonly List<IGameFixedUpdateListener> _listeners = new();

        public void Register(IGameUpdateListener listener)
        {
            if (listener is IGameFixedUpdateListener fixedUpdateListener)
                _listeners.Add(fixedUpdateListener);
        }

        public void Remove(IGameUpdateListener listener)
        {
            if (listener is IGameFixedUpdateListener fixedUpdateListener)
                _listeners.Remove(fixedUpdateListener);
        }

        public void Handle(float deltaTime)
        {
            foreach (var listener in _listeners)
                listener.OnFixedUpdate(deltaTime);
        }
    }
}