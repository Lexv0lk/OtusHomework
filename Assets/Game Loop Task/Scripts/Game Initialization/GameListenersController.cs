using ShootEmUp.Common;
using ShootEmUp.GameStates;
using ShootEmUp.GameUpdate;
using UnityEngine;

namespace ShootEmUp.GameInitialization
{
    public class GameListenersController : MonoBehaviour
    {
        [SerializeField] private GameStateController _gameStateController;
        [SerializeField] private GameUpdateController _gameUpdateController;
        
        private IGameObjectSpawner[] _spawners;

        public void RegisterListeners()
        {
            _spawners = GetComponentsInChildren<IGameObjectSpawner>();

            foreach (var spawner in _spawners)
            {
                spawner.SpawnedObject += OnObjectSpawned;
                spawner.ReleasedObject += OnObjectReleased;
            }

            var existingStateListeners = GetComponentsInChildren<IGameStateListener>();

            foreach (var existingStateListener in existingStateListeners)
                _gameStateController.Register(existingStateListener);
            
            var existingUpdateListeners = GetComponentsInChildren<IGameUpdateListener>();

            foreach (var existingUpdateListener in existingUpdateListeners)
                _gameUpdateController.Register(existingUpdateListener);
        }

        private void OnDestroy()
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