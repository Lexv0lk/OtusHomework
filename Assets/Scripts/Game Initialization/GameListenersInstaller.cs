using System;
using ShootEmUp.Common;
using ShootEmUp.GameStates;
using ShootEmUp.GameUpdate;
using UnityEngine;

namespace ShootEmUp.GameInitialization
{
    public class GameListenersInstaller : MonoBehaviour
    {
        [SerializeField] private GameStateController _gameStateController;
        [SerializeField] private GameUpdateController _gameUpdateController;
        
        private MonoOjbectsFabric[] _fabrics;
        
        private void Awake()
        {
            _fabrics = GetComponentsInChildren<MonoOjbectsFabric>();

            foreach (var fabric in _fabrics)
                fabric.Created += OnObjectCreated;

            var existingStateListeners = GetComponentsInChildren<IGameStateListener>();

            foreach (var existingStateListener in existingStateListeners)
                _gameStateController.Register(existingStateListener);
            
            var existingUpdateListeners = GetComponentsInChildren<IGameUpdateListener>();

            foreach (var existingUpdateListener in existingUpdateListeners)
                _gameUpdateController.Register(existingUpdateListener);
            
            _gameStateController.InitializeGame();
        }

        private void OnDestroy()
        {
            foreach (var fabric in _fabrics)
                fabric.Created -= OnObjectCreated;
        }
        
        private void OnObjectCreated(GameObject obj)
        {
            foreach (var listener in obj.GetComponents<IGameStateListener>())
                _gameStateController.Register(listener);
            
            foreach (var listener in obj.GetComponents<IGameUpdateListener>())
                _gameUpdateController.Register(listener);
        }
    }
}