using ShootEmUp.GameUpdate;
using UnityEngine;
using Zenject;

namespace ShootEmUp.Input
{
    public sealed class InputManager : IGameSimpleUpdateListener
    {
        private readonly InputConfig _inputConfig;
        private float _horizontalDirection;

        [Inject]
        public InputManager(InputConfig inputConfig)
        {
            _inputConfig = inputConfig;
        }

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