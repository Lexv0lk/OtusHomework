using Client.Components;
using Client.Configs;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Helpers;
using UnityEngine;

namespace Client.Systems
{
    public class BuildingsHarmViewSystem : IEcsRunSystem
    {
        private EcsWorldInject _defaultWorld = default;

        private EcsFilterInject<Inc<TakeDamageEvent, TargetEntity>> _filter = EcsWorlds.EVENTS;

        private EcsPoolInject<BuildingHarmViewData> _harmViewPool = default;
        private EcsPoolInject<Health> _healthPool = default;

        private EcsFactoryInject<SpawnRequest, Position, Rotation, Prefab, Parent> _factory = EcsWorlds.EVENTS;

        private EcsCustomInject<VFXConfig> _vfxConfig;
        
        public void Run(IEcsSystems systems)
        {
            var defaultWorld = _defaultWorld.Value;

            foreach (var @event in _filter.Value)
            {
                if (_filter.Pools.Inc2.Get(@event).Value.Unpack(defaultWorld, out int entity))
                {
                    if (_harmViewPool.Value.Has(entity) && _healthPool.Value.Has(entity))
                    {
                        Health health = _healthPool.Value.Get(entity);
                        ref BuildingHarmViewData harmViewData = ref _harmViewPool.Value.Get(entity);
                        float healthPart = health.CurrentHealth / health.MaxHealth;
                        int calculatedIndex = 0;

                        for (int i = 0; i < harmViewData.Positions.Length; i++)
                        {
                            if (healthPart <= harmViewData.Positions[i].HealthPart)
                                calculatedIndex = i;
                            else
                                break;
                        }

                        for (int i = harmViewData.CurrentPosIndex; i <= calculatedIndex; i++)
                        {
                            _factory.Value.NewEntity(
                                new SpawnRequest(),
                                new Position { Value = harmViewData.Positions[i].Position },
                                new Rotation { Value = Quaternion.identity },
                                new Prefab { Value = _vfxConfig.Value.FireVFX },
                                new Parent { Value = defaultWorld.PackEntity(entity) }
                            );
                        }

                        harmViewData.CurrentPosIndex = Mathf.Max(harmViewData.CurrentPosIndex, calculatedIndex + 1);
                    }
                }
            }
        }
    }
}