using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Helpers;

namespace Client.Systems
{
    public class BulletCollisionRequestSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _defaultWorld = default;
        private readonly EcsWorldInject _eventsWorld = EcsWorlds.EVENTS;
        
        private readonly EcsFilterInject<Inc<CollisionEnterRequest, BulletTag, SourceEntity, TargetEntity, Position>>
            _filter = EcsWorlds.EVENTS;
        
        private readonly EcsPoolInject<Damage> _damagePool = default;
        private readonly EcsPoolInject<Inactive> _inactivePool = default;
        private readonly EcsPoolInject<TeamData> _teamPool = default;
        
        private readonly EcsFactoryInject<TakeDamageRequest, TargetEntity, Damage> _factory = EcsWorlds.EVENTS;
        
        public void Run(IEcsSystems systems)
        {
            var defaultWorld = _defaultWorld.Value;
            var eventsWorld = _eventsWorld.Value;
            
            foreach (var @event in _filter.Value)
            {
                if (_filter.Pools.Inc3.Get(@event).Value.Unpack(defaultWorld, out int sourceEntity))
                {
                    if (_inactivePool.Value.Has(sourceEntity))
                    {
                        eventsWorld.DelEntity(@event);
                        continue;
                    }
                    
                    if (_filter.Pools.Inc4.Get(@event).Value.Unpack(defaultWorld, out int targetEntity))
                    {
                        if (_teamPool.Value.Has(sourceEntity) && _teamPool.Value.Has(targetEntity))
                        {
                            if (_teamPool.Value.Get(sourceEntity).Value != _teamPool.Value.Get(targetEntity).Value)
                            {
                                if (_damagePool.Value.Has(sourceEntity))
                                {
                                    _factory.Value.NewEntity(
                                        new TakeDamageRequest(),
                                        _filter.Pools.Inc4.Get(@event),
                                        _damagePool.Value.Get(sourceEntity)
                                    );
                                }
                                
                                _inactivePool.Value.Add(sourceEntity);
                            }
                        }
                    }

                }
                
                eventsWorld.DelEntity(@event);
            }
        }
    }
}