using ShootEmUp.Characters;
using ShootEmUp.GameUpdate;
using ShootEmUp.Input;
using UnityEngine;

namespace ShootEmUp.Player
{
    public sealed class PlayerMoveController : MonoBehaviour, IGameFixedUpdateListener
    {
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private Character _player;
        
        void IGameFixedUpdateListener.OnFixedUpdate(float fixedDeltaTime)
        {
            _player.MoveComponent.Move(_inputManager.GetDirection() * Time.fixedDeltaTime);
        }
    }
}