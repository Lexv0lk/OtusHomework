using ShootEmUp.GameUpdate;
using UnityEngine;

namespace ShootEmUp.Input
{
    public sealed class InputManager : MonoBehaviour, IGameSimpleUpdateListener
    {
        [SerializeField] private InputConfig _inputConfig;

        private float _horizontalDirection;

        public Vector2 GetDirection()
        {
            return new Vector2(_horizontalDirection, 0);
        }

        public bool IsAttackRequired()
        {
            return UnityEngine.Input.GetKeyDown(_inputConfig.Shoot);
        }

        void IGameSimpleUpdateListener.OnUpdate(float deltaTime)
        {
            if (UnityEngine.Input.GetKey(_inputConfig.Left))
                _horizontalDirection = -1;
            else if (UnityEngine.Input.GetKey(_inputConfig.Right))
                _horizontalDirection = 1;
            else
                _horizontalDirection = 0;
        }
    }
}