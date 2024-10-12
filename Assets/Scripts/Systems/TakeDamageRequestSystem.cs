using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems
{
    public class TakeDamageRequestSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _defaultWorld = default;
        private readonly EcsWorldInject _eventsWorld = EcsWorlds.EVENTS;
        
        private readonly EcsFilterInject<Inc<TakeDamageRequest, TargetEntity, Damage>> _filter =
            EcsWorlds.EVENTS;

        private readonly EcsPoolInject<Health> _healthPool = default;
        private readonly EcsPoolInject<TakeDamageRequest> _requestsPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<TakeDamageEvent> _eventsPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Position> _defaultPositionPool = default;
        private readonly EcsPoolInject<Position> _eventsPositionPool = EcsWorlds.EVENTS;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var @event in _filter.Value)
            {
                if (_filter.Pools.Inc2.Get(@event).Value.Unpack(_defaultWorld.Value, out int targetEntity))
                {
                    if (_healthPool.Value.Has(targetEntity))
                    {
                        ref Health health = ref _healthPool.Value.Get(targetEntity);
                        health.Value = Mathf.Max(0, health.Value - _filter.Pools.Inc3.Get(@event).Value);

                        if (_defaultPositionPool.Value.Has(targetEntity))
                        {
                            ref var position = ref _eventsPositionPool.Value.Add(@event);
                            position.Value = _defaultPositionPool.Value.Get(targetEntity).Value;
                        }
                        
                        _requestsPool.Value.Del(@event);
                        _eventsPool.Value.Add(@event);
                    }
                    else
                    {
                        _eventsWorld.Value.DelEntity(@event);
                    }
                }
            }
        }
    }
}