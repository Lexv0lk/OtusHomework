using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Helpers;
using UnityEngine;

namespace Client.Systems
{
    public class RangeAttackRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<RangeAttackRequest, SourceEntity, Position, Prefab>> _filter = EcsWorlds.EVENTS;
        
        private readonly EcsPoolInject<TargetEntity> _defaultTargetPool = default;
        private readonly EcsPoolInject<AttackData> _attackDataPool = default;
        private readonly EcsPoolInject<Position> _positionPool = default;
        
        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS;
        private readonly EcsWorldInject _defaultWorld = default;
        
        private readonly EcsFactoryInject<SpawnRequest, Position, Rotation, Prefab> _factory = EcsWorlds.EVENTS;
        
        public void Run(IEcsSystems systems)
        {
            var defaultWorld = _defaultWorld.Value;
            var eventsWorld = _eventWorld.Value;

            foreach (var @event in _filter.Value)
            {
                if (_filter.Pools.Inc2.Get(@event).Value.Unpack(defaultWorld, out int sourceEntity))
                {
                    if (_defaultTargetPool.Value.Has(sourceEntity) && _attackDataPool.Value.Has(sourceEntity))
                    {
                        TargetEntity target = _defaultTargetPool.Value.Get(sourceEntity);
                        
                        if (target.Value.Unpack(defaultWorld, out int targetEntity))
                        {
                            Position sourcePosition = _positionPool.Value.Get(sourceEntity);
                            Position targetPosition = _positionPool.Value.Get(targetEntity);
                            Vector3 direction = targetPosition.Value - sourcePosition.Value;
                            
                            Position position = _filter.Pools.Inc3.Get(@event);
                            Prefab prefab = _filter.Pools.Inc4.Get(@event);
                            
                            _factory.Value.NewEntity(
                                new SpawnRequest(),
                                position,
                                new Rotation() { Value = Quaternion.LookRotation(direction.normalized) },
                                prefab
                            );
                        }
                    }
                }
                
                eventsWorld.DelEntity(@event);
            }
        }
    }
}