using Client.Common;
using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems
{
    public class TargetDetectionSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _defaultWorld = default;
        
        private readonly EcsFilterInject<Inc<TargetEntity, TeamData, Position>> _filterToUpdateTarget;
        private readonly EcsFilterInject<Inc<TeamData, Position>, Exc<BulletTag>> _filterToFindTarget;
        
        public void Run(IEcsSystems systems)
        {
            EcsPool<TeamData> teamDataPool = _filterToFindTarget.Pools.Inc1;
            EcsPool<Position> positionPool = _filterToFindTarget.Pools.Inc2;
            
            foreach (var entity in _filterToUpdateTarget.Value)
            {
                int closestTarget = -1;
                float closestDistance = float.MaxValue;
                Team entityTeam = teamDataPool.Get(entity).Value;
                Vector3 entityPosition = positionPool.Get(entity).Value;
                ref TargetEntity targetEntity = ref _filterToUpdateTarget.Pools.Inc1.Get(entity);
                
                if (targetEntity.Value.Unpack(_defaultWorld.Value, out int targetEntityInd))
                    continue;

                foreach (var possibleTarget in _filterToFindTarget.Value)
                {
                    Team targetTeam = teamDataPool.Get(possibleTarget).Value;
                    
                    if (targetTeam == entityTeam)
                        continue;
                    
                    Vector3 targetPosition = positionPool.Get(possibleTarget).Value;
                    float distance = Vector3.SqrMagnitude(targetPosition - entityPosition);

                    if (distance < closestDistance)
                    {
                        closestTarget = possibleTarget;
                        closestDistance = distance;
                    }
                }
                
                if (closestTarget != -1)
                    targetEntity.Value = _defaultWorld.Value.PackEntity(closestTarget);
                else
                    targetEntity.Value = new EcsPackedEntity();
            }
        }
    }
}