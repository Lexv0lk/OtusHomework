using Cysharp.Threading.Tasks;
using ShootEmUp.Components;
using ShootEmUp.GameUpdate;
using UnityEngine;

namespace ShootEmUp.Enemies.Agents
{
    public sealed class MoveToMechanic : IGameFixedUpdateListener
    {
        private UniTaskCompletionSource _reachedCompletionSource;
        
        private MoveComponent _moveComponent;
        private float _destinationAccuraccy = 0.25f;
        private Transform _transform;

        private Vector2 _destination;
        private bool _reachedCurrentDestination;
        private bool _canMove;

        public MoveToMechanic(MoveComponent moveComponent, float destinationAccuraccy, Transform transform)
        {
            _moveComponent = moveComponent;
            _destinationAccuraccy = destinationAccuraccy;
            _transform = transform;
        }
        
        public async UniTask MoveTo(Vector2 endPoint)
        {
            _destination = endPoint;
            
            _reachedCurrentDestination = false;
            _canMove = true;
            
            _reachedCompletionSource = new UniTaskCompletionSource();
            await _reachedCompletionSource.Task;
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            if (_canMove == false || _reachedCurrentDestination)
                return;
            
            Vector2 direction = _destination - (Vector2)_transform.position;

            if (direction.magnitude <= _destinationAccuraccy)
            {
                _reachedCurrentDestination = true;
                _reachedCompletionSource.TrySetResult();
                return;
            }

            Vector2 resultDirection = direction.normalized * Time.fixedDeltaTime;
            _moveComponent.Move(resultDirection);
        }
    }
}