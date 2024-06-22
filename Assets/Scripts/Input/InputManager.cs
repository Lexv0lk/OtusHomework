using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputManager : MonoBehaviour
    {
        [SerializeField] private InputConfig _inputConfig;

        private float _horizontalDirection;

        private void Update()
        {
            if (Input.GetKey(_inputConfig.Left))
                _horizontalDirection = -1;
            else if (Input.GetKey(_inputConfig.Right))
                _horizontalDirection = 1;
            else
                _horizontalDirection = 0;
        }

        public Vector2 GetDirection()
        {
            return new Vector2(_horizontalDirection, 0);
        }

        public bool IsAttackRequired()
        {
            return Input.GetKeyDown(_inputConfig.Shoot);
        }
    }
}