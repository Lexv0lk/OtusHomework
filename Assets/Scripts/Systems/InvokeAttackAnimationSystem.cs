using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems
{
    public class InvokeAttackAnimationSystem : IEcsRunSystem
    {
        private readonly string _attackTrigger = "Attack";
        
        private EcsWorldInject _defaultWorld = default;
        private EcsFilterInject<Inc<TargetEntity, AttackData, Reload, Position, AnimatorView>> _filter;
        private EcsPoolInject<Position> _positionPool;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref Reload reload = ref _filter.Pools.Inc3.Get(entity);
                
                if (_filter.Pools.Inc1.Get(entity).Value.Unpack(_defaultWorld.Value, out int target) == false)
                    continue;
                
                if (Mathf.Approximately(reload.TimeLeft, 0) == false)
                    continue;
                
                Vector3 pos = _filter.Pools.Inc4.Get(entity).Value;
                AttackData attackData = _filter.Pools.Inc2.Get(entity);
                float attackRange = attackData.Range;

                if (_positionPool.Value.Has(target))
                {
                    Vector3 targetPos = _positionPool.Value.Get(target).Value;
                    float distance = Vector3.Distance(pos, targetPos);

                    if (attackRange >= distance)
                    {
                        _filter.Pools.Inc5.Get(entity).Animator.SetTrigger(_attackTrigger);
                        reload.TimeLeft = attackData.ReloadTime;
                    }
                }
            }
        }
    }
}