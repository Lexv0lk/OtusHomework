using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client.Systems
{
    public class HealthDeathSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Health>, Exc<Inactive>> _filter;
        private readonly EcsPoolInject<Inactive> _inactivePool;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                if (_filter.Pools.Inc1.Get(entity).Value <= 0)
                {
                    _inactivePool.Value.Add(entity) = new Inactive();
                }
            }
        }
    }
}