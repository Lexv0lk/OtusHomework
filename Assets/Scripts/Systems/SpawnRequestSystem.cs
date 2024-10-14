using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Systems
{
    public class SpawnRequestSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS;
        private readonly EcsFilterInject<Inc<SpawnRequest, Position, Rotation, Prefab>> _filter = EcsWorlds.EVENTS;

        private readonly EcsPoolInject<Parent> _parentPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<TransformView> _transformViewPool = default;
        
        private readonly EcsCustomInject<EntityManager> _entityManager;

        public void Run(IEcsSystems systems)
        {
            foreach (int @event in _filter.Value)
            {
                Vector3 position = _filter.Pools.Inc2.Get(@event).Value;
                Quaternion rotation = _filter.Pools.Inc3.Get(@event).Value;
                Entity prefab = _filter.Pools.Inc4.Get(@event).Value;
                Transform parentTransform = null;
                
                if (_parentPool.Value.Has(@event) && _transformViewPool)
                
                _entityManager.Value.Create(prefab, position, rotation, parentTransform);
                
                _eventWorld.Value.DelEntity(@event);
            }
        }
    }
}