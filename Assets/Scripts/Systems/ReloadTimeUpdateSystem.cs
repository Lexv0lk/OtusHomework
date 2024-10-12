using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems
{
    public class ReloadTimeUpdateSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<Reload>> _filter;
        
        public void Run(IEcsSystems systems)
        {
            float deltaTime = Time.deltaTime;
            
            foreach (var entity in _filter.Value)
            {
                ref Reload reload = ref _filter.Pools.Inc1.Get(entity);
                reload.TimeLeft = Mathf.Max(0, reload.TimeLeft - deltaTime);
            }
        }
    }
}