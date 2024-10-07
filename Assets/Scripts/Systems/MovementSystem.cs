using Client.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Client.Systems
{
    public class MovementSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            float deltaTime = Time.deltaTime;
            EcsWorld world = systems.GetWorld();
            EcsFilter filter = world.Filter<MoveDirection>().Inc<MoveSpeed>().Inc<Position>().End();

            EcsPool<MoveDirection> directionPool = world.GetPool<MoveDirection>();
            EcsPool<MoveSpeed> speedPool = world.GetPool<MoveSpeed>();
            EcsPool<Position> positionPool = world.GetPool<Position>();
            
            foreach (var entity in filter)
            {
                MoveDirection direction = directionPool.Get(entity);
                MoveSpeed speed = speedPool.Get(entity);
                ref Position position = ref positionPool.Get(entity);
                position.Value += direction.Value * (speed.Value * deltaTime);
            }
        }
    }
}