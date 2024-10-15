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

        private readonly EcsPoolInject<TakeDamageRequest> _requestsPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<TakeDamageEvent> _eventsPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Position> _eventsPositionPool = EcsWorlds.EVENTS;
        
        private readonly EcsPoolInject<Health> _healthPool = default;
        private readonly EcsPoolInject<Position> _defaultPositionPool = default;
        private readonly EcsPoolInject<CenterPointView> _centerPointPool = default;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var @event in _filter.Value)
            {
                if (_filter.Pools.Inc2.Get(@event).Value.Unpack(_defaultWorld.Value, out int targetEntity))
                {
                    if (_healthPool.Value.Has(targetEntity))
                    {
                        ref Health health = ref _healthPool.Value.Get(targetEntity);
                        health.CurrentHealth = Mathf.Max(0, health.CurrentHealth - _filter.Pools.Inc3.Get(@event).Value);

                        if (_eventsPositionPool.Value.Has(@event) == false)
                        {
                            if (_centerPointPool.Value.Has(targetEntity))
                            {
                                Vector3 position = _centerPointPool.Value.Get(targetEntity).Value.position;
                                _eventsPositionPool.Value.Add(@event) = new Position() { Value = position };
                            }
                            else if (_defaultPositionPool.Value.Has(targetEntity))
                            {
                                _eventsPositionPool.Value.Add(@event) = _defaultPositionPool.Value.Get(targetEntity);

                            }
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