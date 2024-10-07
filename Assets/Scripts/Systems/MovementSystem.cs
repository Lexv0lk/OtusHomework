using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems
{
    public class TransformViewSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<TransformView, Position>> _filter;
        private readonly EcsPoolInject<Rotation> _rotationPool;
        
        public void Run(IEcsSystems systems)
        {
            EcsPool<Rotation> rotationPool = _rotationPool.Value;

            foreach (var entity in _filter.Value)
            {
                ref TransformView transform = ref _filter.Pools.Inc1.Get(entity);
                Position position = _filter.Pools.Inc2.Get(entity);
                
                transform.Value.position = position.Value;

                if (rotationPool.Has(entity))
                {
                    Quaternion rotation = rotationPool.Get(entity).Value;
                    transform.Value.rotation = rotation;
                }
            }
        }
    }
    
    public class MovementSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MoveDirection, MoveSpeed, Position>> _filter;
        
        public void Run(IEcsSystems systems)
        {
            float deltaTime = Time.deltaTime;

            EcsPool<MoveDirection> directionPool = _filter.Pools.Inc1;
            EcsPool<MoveSpeed> speedPool = _filter.Pools.Inc2;
            EcsPool<Position> positionPool = _filter.Pools.Inc3;
            
            foreach (var entity in _filter.Value)
            {
                MoveDirection direction = directionPool.Get(entity);
                MoveSpeed speed = speedPool.Get(entity);
                ref Position position = ref positionPool.Get(entity);
                position.Value += direction.Value * (speed.Value * deltaTime);
            }
        }
    }
}