using Client.Components;
using Client.Configs;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Helpers;
using UnityEngine;

namespace Client.Systems
{
    public class UnitDamageViewSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _defaultWorld = default;
        
        private readonly EcsFilterInject<Inc<TakeDamageEvent, TargetEntity, Position>> _filter = EcsWorlds.EVENTS;

        private readonly EcsPoolInject<UnitTag> _unitPool = default;

        private readonly EcsFactoryInject<SpawnRequest, Position, Rotation, Prefab> _factory = default;

        private readonly EcsCustomInject<VFXConfig> _config;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var @event in _filter.Value)
            {
                if (_filter.Pools.Inc2.Get(@event).Value.Unpack(_defaultWorld.Value, out int target))
                {
                    if (_unitPool.Value.Has(target))
                    {
                        _factory.Value.NewEntity(
                            new SpawnRequest(),
                            _filter.Pools.Inc3.Get(@event),
                            new Rotation { Value = Quaternion.identity },
                            new Prefab { Value = _config.Value.BloodVFX }
                        );
                    }
                }
            }
        }
    }
}