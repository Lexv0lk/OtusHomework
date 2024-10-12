using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems
{
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
                position.Value += direction.Value * (speed.CurrentSpeed * deltaTime);
            }
        }
    }
}