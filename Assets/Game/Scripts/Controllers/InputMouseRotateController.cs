using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Configs.Input;
using Game.Scripts.Tech;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Controllers
{
    public class InputMouseRotateController : ITickable
    {
        private readonly Camera _camera;
        private readonly MouseRotationConfig _mouseRotationConfig;
        private readonly IAtomicValue<Vector3> _entityPosition;
        private readonly IAtomicVariable<Vector3> _entityForwardDirection;

        private Vector3 _cachedHitPosition;

        public InputMouseRotateController(Camera camera, IAtomicEntity entity, MouseRotationConfig mouseRotationConfig)
        {
            _camera = camera;
            _mouseRotationConfig = mouseRotationConfig;
            _entityPosition = entity.Get<IAtomicValue<Vector3>>(TransformAPI.POSITION);
            _entityForwardDirection = entity.Get<IAtomicVariable<Vector3>>(TransformAPI.FORWARD_DIRECTION);
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
    }
}