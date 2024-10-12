using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems
{
    public class MoveToTargetSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<TargetEntity, MoveDirection, Position, AttackData, MoveSpeed>> _filterToUpdateDirection;
        private readonly EcsPoolInject<Position> _positionPool;
        
        public void Run(IEcsSystems systems)
        {
            EcsPool<TargetEntity> targetPool = _filterToUpdateDirection.Pools.Inc1;
            
            foreach (var entity in _filterToUpdateDirection.Value)
            {
                Position position = _positionPool.Value.Get(entity);
                AttackData attackData = _filterToUpdateDirection.Pools.Inc4.Get(entity);
                
                if (targetPool.Get(entity).Value.Unpack(systems.GetWorld(), out int targetEntity))
                {
                    Position targetPosition = _positionPool.Value.Get(targetEntity);
                    float distance = Vector3.Distance(position.Value, targetPosition.Value);
                    
                    ref MoveDirection direction = ref _filterToUpdateDirection.Pools.Inc2.Get(entity);
                    direction.Value = (targetPosition.Value - position.Value).normalized;

                    ref MoveSpeed moveSpeed = ref _filterToUpdateDirection.Pools.Inc5.Get(entity);
                    moveSpeed.CurrentSpeed = distance <= attackData.Range ? 0 : moveSpeed.BaseSpeed;
                }
            }
        }
    }
}