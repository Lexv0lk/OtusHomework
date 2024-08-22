using Atomic.Elements;
using Atomic.Objects;
using Cysharp.Threading.Tasks.Triggers;
using Game.Scripts.Configs.Input;
using Game.Scripts.Tech;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Controllers
{
    public class InputMouseRotateController : ITickable
    {
        private readonly Camera _camera;
        private readonly AtomicEntity _entity;
        private readonly MouseRotationConfig _mouseRotationConfig;
        private readonly IAtomicValue<Vector3> _entityPosition;
        private readonly IAtomicVariable<Vector3> _entityForwardDirection;

        private Vector3 _cachedHitPosition;

        public InputMouseRotateController(Camera camera, AtomicEntity entity, MouseRotationConfig mouseRotationConfig)
        {
            _camera = camera;
            _entity = entity;
            _mouseRotationConfig = mouseRotationConfig;
            _entityPosition = new AtomicFunction<Vector3>(GetEntityPosition);
            _entityForwardDirection = entity.Get<IAtomicVariable<Vector3>>(MoveAPI.FORWARD_DIRECTION);
        }
        
        public void Tick()
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, _mouseRotationConfig.MaximalRayDistance,
                    _mouseRotationConfig.GroundMask))
            {
                _cachedHitPosition = hit.point;
                _cachedHitPosition.y = _entityPosition.Value.y;

                _entityForwardDirection.Value = _cachedHitPosition - _entityPosition.Value;
            }
        }

        private Vector3 GetEntityPosition()
        {
            return _entity.transform.position;
        }
    }
}