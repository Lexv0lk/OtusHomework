using ShootEmUp.Characters;
using ShootEmUp.Input;
using UnityEngine;

namespace ShootEmUp.Player
{
    public sealed class PlayerMoveController : MonoBehaviour
    {
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private Character _player;

        private void FixedUpdate()
        {
            _player.MoveComponent.Move(_inputManager.GetDirection() * Time.fixedDeltaTime);
        }
    }
}