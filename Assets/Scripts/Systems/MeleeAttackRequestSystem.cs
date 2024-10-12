using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Helpers;

namespace Client.Systems
{
    public class RangeAttackRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<RangeAttackRequest, SourceEntity, Position>> _filter = EcsWorlds.EVENTS;
        
        private readonly EcsPoolInject<TargetEntity> _defaultTargetPool = default;
        private readonly EcsPoolInject<AttackData> _attackDataPool = default;
        
        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS;
        private readonly EcsWorldInject _defaultWorld = default;
        
        private readonly EcsFactoryInject<SpawnRequest, Position, Rotation, Prefab> _factory = EcsWorlds.EVENTS;
        
        public void Run(IEcsSystems systems)
        {
            var defaultWorld = _defaultWorld.Value;
            var eventsWorld = _eventWorld.Value;

            foreach (var @event in _filter.Value)
            {
                
            }
        }
    }
    
    public class MeleeAttackRequestSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS;
        private readonly EcsWorldInject _defaultWorld = default;

        private readonly EcsFactoryInject<TakeDamageRequest, TargetEntity, Damage> _factory = EcsWorlds.EVENTS;
        
        private readonly EcsFilterInject<Inc<MeleeAttackRequest, SourceEntity>> _filter = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<TargetEntity> _defaultTargetPool = default;
        private readonly EcsPoolInject<AttackData> _attackDataPool = default;
        
        public void Run(IEcsSystems systems)
        {
            var defaultWorld = _defaultWorld.Value;
            
            foreach (var @event in _filter.Value)
            {
                if (_filter.Pools.Inc2.Get(@event).Value.Unpack(defaultWorld, out int sourceEntity))
                {
                    if (_defaultTargetPool.Value.Has(sourceEntity))
                    {
                        if (_attackDataPool.Value.Has(sourceEntity))
                        {
                            float damage = _attackDataPool.Value.Get(sourceEntity).Damage;

                            _factory.Value.NewEntity(
                                new TakeDamageRequest(),
                                _defaultTargetPool.Value.Get(sourceEntity),
                                new Damage() { Value = damage }
                            );
                        }
                    }
                }
                
                _eventWorld.Value.DelEntity(@event);
            }
        }
    }
}