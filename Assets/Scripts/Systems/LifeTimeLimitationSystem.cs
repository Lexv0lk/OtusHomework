using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems
{
    public class LifeTimeLimitationSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<LifeTimeLimit>, Exc<Inactive>> _filter = default;
        private EcsPoolInject<Inactive> _inactivePool = default;
        
        public void Run(IEcsSystems systems)
        {
            float deltaTime = Time.deltaTime;
            
            foreach (var entity in _filter.Value)
            {
                ref LifeTimeLimit lifeTimeLimit = ref _filter.Pools.Inc1.Get(entity);
                lifeTimeLimit.CurrentLifeTime += deltaTime;

                if (lifeTimeLimit.CurrentLifeTime >= lifeTimeLimit.MaximalLifeTime)
                    _inactivePool.Value.Add(entity);
            }
        }
    }
}