using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;

namespace Client.Systems
{
    public class InactiveDestroySystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Inactive>> _filter;
        private readonly EcsCustomInject<EntityManager> _entityManager;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter.Value)
                _entityManager.Value.Destroy(entity);
        }
    }
}