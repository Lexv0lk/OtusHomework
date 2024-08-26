using ShootEmUp.GameStates;
using UnityEngine;

namespace ShootEmUp.GameInitialization
{
    public class GameBootstraper : MonoBehaviour
    {
        [SerializeField] private GameListenersController listenersController;
        [SerializeField] private GameStateController _stateController;

        private void Awake()
        {
            listenersController.RegisterListeners();
            _stateController.InitializeGame();
        }
    }
}