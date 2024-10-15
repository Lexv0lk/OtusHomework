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
        private readonly EcsWorldInject _defaultWorld = default;
        
        private readonly EcsFilterInject<Inc<SpawnRequest, Position, Rotation, Prefab>> _filter = EcsWorlds.EVENTS;

        private readonly EcsPoolInject<Parent> _eventsParentPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Parent> _defaultParentPool = default;
        private readonly EcsPoolInject<TransformView> _transformViewPool = default;
        
        private readonly EcsCustomInject<EntityManager> _entityManager;

        public void Run(IEcsSystems systems)
        {
            var defaultWorld = _defaultWorld.Value;
            
            foreach (int @event in _filter.Value)
            {
                Vector3 position = _filter.Pools.Inc2.Get(@event).Value;
                Quaternion rotation = _filter.Pools.Inc3.Get(@event).Value;
                Entity prefab = _filter.Pools.Inc4.Get(@event).Value;
                Transform parentTransform = null;
                
                if (_eventsParentPool.Value.Has(@event))
                    if (_eventsParentPool.Value.Get(@event).Value.Unpack(defaultWorld, out int parentId))
                        if (_transformViewPool.Value.Has(parentId))
                            parentTransform = _transformViewPool.Value.Get(parentId).Value;
                
                var entity = _entityManager.Value.Create(prefab, position, rotation, parentTransform);

                if (_eventsParentPool.Value.Has(@event))
                    _defaultParentPool.Value.Add(entity.Id) = _eventsParentPool.Value.Get(@event);
                
                _eventWorld.Value.DelEntity(@event);
            }
        }
    }
}