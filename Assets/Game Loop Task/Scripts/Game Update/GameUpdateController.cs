using System.Collections.Generic;
using ShootEmUp.GameStates;
using UnityEngine;

namespace ShootEmUp.GameUpdate
{
    public class GameUpdateController : MonoBehaviour
    {
        [SerializeField] private GameStateModel _stateModel;
        
        private readonly List<IGameUpdateListener> _updateListeners = new();
        private List<IGameUpdateHandler> _handlers;

        private readonly GameSimpleUpdateHandler _gameSimpleUpdateHandler = new();
        private readonly GameFixedUpdateHandler _gameFixedUpdateHandler = new();

        private void InitializeHandlersList()
        {
            _handlers = new()
            {
                _gameSimpleUpdateHandler,
                _gameFixedUpdateHandler
            };
        }

        private void Update()
        {
            if (_stateModel.CurrentState != GameState.PLAYING)
                return;
            
            _gameSimpleUpdateHandler.Handle(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            if (_stateModel.CurrentState != GameState.PLAYING)
                return;
            
            _gameFixedUpdateHandler.Handle(Time.fixedDeltaTime);
        }

        public void Register(IGameUpdateListener listener)
        {
            if (_handlers == null)
                InitializeHandlersList();
            
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