using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems
{
    public class LookAtTargetSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<TargetEntity, Rotation>> _filter;
        
        public void Run(IEcsSystems systems)
        {
            EcsPool<TargetEntity> targetPool = _filter.Pools.Inc1;
            EcsPool<Rotation> rotationPool = _filter.Pools.Inc2;

            foreach (var entity in _filter.Value)
            {
                if (targetPool.Get(entity).Value.Unpack(systems.GetWorld(), out int targetEntity))
                {
                    ref Rotation rotation = ref rotationPool.Get(entity);
                    
                    Position position = systems.GetWorld().GetPool<Position>().Get(entity);
                    Position targetPosition = systems.GetWorld().GetPool<Position>().Get(targetEntity);
                    
                    Vector3 direction = targetPosition.Value - position.Value;
                    rotation.Value = Quaternion.LookRotation(direction, Vector3.up);
                }
            }
        }
    }
}