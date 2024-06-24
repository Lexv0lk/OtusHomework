using ShootEmUp.Components;
using UnityEngine;

namespace ShootEmUp.Enemies.Agents
{
    public sealed class EnemyMoveAgent : MonoBehaviour
    {
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private float _destinationAccuraccy = 0.25f;

        private Vector2 _destination;
        private bool _isReached;

        public bool IsReached => _isReached;

        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            _isReached = false;
        }

        private void FixedUpdate()
        {
            if (_isReached)
                return;
            
            Vector2 direction = _destination - (Vector2)transform.position;

            if (direction.magnitude <= _destinationAccuraccy)
            {
                _isReached = true;
                return;
            }

            Vector2 resultDirection = direction.normalized * Time.fixedDeltaTime;
            _moveComponent.Move(resultDirection);
        }
    }
}