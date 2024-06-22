using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterMoveController : MonoBehaviour
    {
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private Character _character;

        private void FixedUpdate()
        {
            _character.MoveByRigidbodyVelocity(_inputManager.GetDirection() * Time.fixedDeltaTime);
        }
    }
}