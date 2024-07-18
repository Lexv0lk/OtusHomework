using ShootEmUp.Characters;
using ShootEmUp.GameUpdate;
using ShootEmUp.Input;
using UnityEngine;
using Zenject;

namespace ShootEmUp.Player
{
    public sealed class PlayerMoveController : IGameFixedUpdateListener
    {
        private InputManager _inputManager;
        private Character _player;

        [Inject]
        public PlayerMoveController(Character player, InputManager inputManager)
        {
            _inputManager = inputManager;
            _player = player;
        }
        
        void IGameFixedUpdateListener.OnFixedUpdate(float fixedDeltaTime)
        {
            _player.MoveComponent.Move(_inputManager.GetDirection() * Time.fixedDeltaTime);
        }
    }
}