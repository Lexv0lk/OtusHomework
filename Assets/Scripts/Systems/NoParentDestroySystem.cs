using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client.Systems
{
    public class NoParentDestroySystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _defaultWorld = default;
        private readonly EcsFilterInject<Inc<Parent>, Exc<Inactive>> _filter = default;
        private readonly EcsPoolInject<Inactive> _inactivePool = default;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                if (_filter.Pools.Inc1.Get(entity).Value.Unpack(_defaultWorld.Value, out int _) == false)
                    _inactivePool.Value.Add(entity);
            }
        }
    }
}