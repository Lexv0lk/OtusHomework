using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp.GameStates
{
    public class GameStateController : IInitializable
    {
        private readonly GameStateModel _gameStateModel;
        private readonly List<IGameStateHandler> _stateHandlers;

        private readonly GameStartHandler _gameStartHandler = new();
        private readonly GamePauseHandler _gamePauseHandler = new();
        private readonly GameResumeHandler _gameResumeHandler = new();
        private readonly GameFinishHandler _gameFinishHandler = new();
        private readonly GameInitializeHandler _gameInitializeHandler = new();

        [Inject]
        public GameStateController(GameStateModel gameStateModel)
        {
            _gameStateModel = gameStateModel;
            
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
            foreach (var handler in _stateHandlers)
                handler.Register(listener);
        }
        
        public void Remove(IGameStateListener listener)
        {
            foreach (var handler in _stateHandlers)
                handler.Remove(listener);
        }

        void IInitializable.Initialize()
        {
            _gameInitializeHandler.Handle(_gameStateModel);
        }

        public void StartGame()
        {
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