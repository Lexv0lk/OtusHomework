using System.Collections.Generic;
using ShootEmUp.GameStates;
using ShootEmUp.GameUpdate;
using Zenject;

namespace ShootEmUp.GameInitialization
{
    public class GameListenersController
    {
        private readonly GameStateController _gameStateController;
        private readonly GameUpdateController _gameUpdateController;
        private readonly List<IGameStateListener> _existingStateListeners;
        private readonly List<IGameUpdateListener> _existingUpdateListeners;
        
        [Inject]
        public GameListenersController(GameStateController gameStateController, GameUpdateController gameUpdateController, 
            List<IGameStateListener> existingStateListeners,
            List<IGameUpdateListener> existingUpdateListeners)
        {
            _gameStateController = gameStateController;
            _gameUpdateController = gameUpdateController;
            _existingStateListeners = existingStateListeners;
            _existingUpdateListeners = existingUpdateListeners;
            
            RegisterListeners();
        }

        private void RegisterListeners()
        {
            foreach (var existingStateListener in _existingStateListeners)
                _gameStateController.Register(existingStateListener);
            
            foreach (var existingUpdateListener in _existingUpdateListeners)
                _gameUpdateController.Register(existingUpdateListener);
        }
    }
}