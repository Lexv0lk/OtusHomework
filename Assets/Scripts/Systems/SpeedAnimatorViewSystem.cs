using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client.Systems
{
    public class SpeedAnimatorViewSystem : IEcsRunSystem
    {
        private readonly string _speedFloat = "Speed";
        private readonly EcsFilterInject<Inc<MoveSpeed, AnimatorView>> _filter;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                float speed = _filter.Pools.Inc1.Get(entity).CurrentSpeed;
                _filter.Pools.Inc2.Get(entity).Animator.SetFloat(_speedFloat, speed);
            }
        }
    }
}