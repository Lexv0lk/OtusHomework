using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp.GameStates
{
    public class GameStateController : MonoBehaviour
    {
        [SerializeField] private GameStateModel _gameStateModel;
        
        private List<IGameStateHandler> _stateHandlers;

        private readonly GameStartHandler _gameStartHandler = new();
        private readonly GamePauseHandler _gamePauseHandler = new();
        private readonly GameResumeHandler _gameResumeHandler = new();
        private readonly GameFinishHandler _gameFinishHandler = new();
        private readonly GameInitializeHandler _gameInitializeHandler = new();

        private void Start()
        {
            StartGame();
        }

        private void InitializeHandlersList()
        {
            _stateHandlers = new()
            {
                _gameStartHandler,
                _gamePauseHandler,
                _gameResumeHandler,
                _gameFinishHandler,
                _gameInitializeHandler
            };
        }

        public void Register(IGameStateListener listener)
        {
            if (_stateHandlers == null)
                InitializeHandlersList();
            
            foreach (var handler in _stateHandlers)
                handler.Register(listener);
        }

        public void InitializeGame()
        {
            Debug.Log("INITIALIZE");
            _gameInitializeHandler.Handle(_gameStateModel);
        }

        public void StartGame()
        {
             Debug.Log("START");
            _gameStartHandler.Handle(_gameStateModel);
        }

        public void FinishGame()
        {
            _gameFinishHandler.Handle(_gameStateModel);
        }

        public void PauseGame()
        {
            _gamePauseHandler.Handle(_gameStateModel);
        }

        public void ResumeGame()
        {
            _gameResumeHandler.Handle(_gameStateModel);
        }
    }
}