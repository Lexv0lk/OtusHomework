using System.Collections.Generic;
using ShootEmUp.GameStates;
using UnityEngine;
using Zenject;

namespace ShootEmUp.GameUpdate
{
    public class GameUpdateController : ITickable, IFixedTickable
    {
        private readonly GameStateModel _stateModel;
        
        private readonly List<IGameUpdateListener> _updateListeners;
        private readonly List<IGameUpdateHandler> _handlers;

        private readonly GameSimpleUpdateHandler _gameSimpleUpdateHandler = new();
        private readonly GameFixedUpdateHandler _gameFixedUpdateHandler = new();
        
        [Inject]
        public GameUpdateController(GameStateModel stateModel, List<IGameUpdateListener> updateListeners)
        {
            _stateModel = stateModel;
            _updateListeners = updateListeners;
            
            _handlers = new()
            {
                _gameSimpleUpdateHandler,
                _gameFixedUpdateHandler
            };
        }

        void ITickable.Tick()
        {
            if (_stateModel.CurrentState != GameState.PLAYING)
                return;
            
            _gameSimpleUpdateHandler.Handle(Time.deltaTime);
        }
        
        void IFixedTickable.FixedTick()
        {
            if (_stateModel.CurrentState != GameState.PLAYING)
                return;
            
            _gameFixedUpdateHandler.Handle(Time.fixedDeltaTime);
        }

        public void Register(IGameUpdateListener listener)
        {
            foreach (var handler in _handlers)
                handler.Register(listener);
        }
        
        public void Remove(IGameUpdateListener listener)
        {
            foreach (var handler in _handlers)
                handler.Remove(listener);
        }
    }
}