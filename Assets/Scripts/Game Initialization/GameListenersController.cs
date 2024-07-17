using System;
using System.Collections.Generic;
using ShootEmUp.Common;
using ShootEmUp.GameStates;
using ShootEmUp.GameUpdate;
using UnityEngine;
using Zenject;

namespace ShootEmUp.GameInitialization
{
    public class GameListenersController : IDisposable
    {
        private readonly GameStateController _gameStateController;
        private readonly GameUpdateController _gameUpdateController;
        private readonly List<IGameObjectSpawner> _spawners;
        private readonly List<IGameStateListener> _existingStateListeners;
        private readonly List<IGameUpdateListener> _existingUpdateListeners;
        
        [Inject]
        public GameListenersController(GameStateController gameStateController, GameUpdateController gameUpdateController,
            List<IGameObjectSpawner> spawners, List<IGameStateListener> existingStateListeners,
            List<IGameUpdateListener> existingUpdateListeners)
        {
            _gameStateController = gameStateController;
            _gameUpdateController = gameUpdateController;
            _spawners = spawners;
            _existingStateListeners = existingStateListeners;
            _existingUpdateListeners = existingUpdateListeners;
            
            RegisterListeners();
        }

        private void RegisterListeners()
        {
            foreach (var spawner in _spawners)
            {
                spawner.SpawnedObject += OnObjectSpawned;
                spawner.ReleasedObject += OnObjectReleased;
            }
            
            foreach (var existingStateListener in _existingStateListeners)
                _gameStateController.Register(existingStateListener);
            
            foreach (var existingUpdateListener in _existingUpdateListeners)
                _gameUpdateController.Register(existingUpdateListener);
        }

        public void Dispose()
        {
            foreach (var spawner in _spawners)
            {
                spawner.SpawnedObject -= OnObjectSpawned;
                spawner.ReleasedObject -= OnObjectReleased;
            }
        }
        
        private void OnObjectSpawned(GameObject obj)
        {
            foreach (var listener in obj.GetComponents<IGameStateListener>())
                _gameStateController.Register(listener);
            
            foreach (var listener in obj.GetComponents<IGameUpdateListener>())
                _gameUpdateController.Register(listener);
        }

        private void OnObjectReleased(GameObject obj)
        {
            foreach (var listener in obj.GetComponents<IGameStateListener>())
                _gameStateController.Remove(listener);
            
            foreach (var listener in obj.GetComponents<IGameUpdateListener>())
                _gameUpdateController.Remove(listener);
        }
    }
}