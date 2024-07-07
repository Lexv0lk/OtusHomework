using System;
using ShootEmUp.GameUpdate;
using UnityEngine;

namespace ShootEmUp.Level
{
    public sealed class LevelBackground : MonoBehaviour, IGameFixedUpdateListener
    {
        [SerializeField] private Params _params;

        private float _startPositionY;
        private float _endPositionY;
        private float _movingSpeedY;
        private float _positionX;
        private float _positionZ;

        private void Awake()
        {
            _startPositionY = _params.StartPositionY;
            _endPositionY = _params.EndPositionY;
            _movingSpeedY = _params.MovingSpeedY;
            _positionX = transform.position.x;
            _positionZ = transform.position.z;
        }

        void IGameFixedUpdateListener.OnFixedUpdate(float fixedDeltaTime)
        {
            if (transform.position.y <= _endPositionY)
            {
                transform.position = new Vector3(
                    _positionX,
                    _startPositionY,
                    _positionZ
                );
            }

            transform.position -= new Vector3(
                _positionX,
                _movingSpeedY * Time.fixedDeltaTime,
                _positionZ
            );
        }

        [Serializable]
        private sealed class Params
        {
            [SerializeField] private float _startPositionY;
            [SerializeField] private float _endPositionY;
            [SerializeField] private float _movingSpeedY;

            public float StartPositionY => _startPositionY;
            public float EndPositionY => _endPositionY;
            public float MovingSpeedY => _movingSpeedY;
        }
    }
}