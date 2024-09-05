using Data;
using Structs;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;
using UnityEngine;

namespace Systems
{
    public partial class TargetDetectionSystem : SystemBase
    {
        private float _closestDistance;
        private Entity _closestEntity;
        
        protected override void OnUpdate()
        {
            var entityArray = GetEntityQuery(typeof(TeamData)).ToEntityArray(Allocator.Temp);
            
            Entities.ForEach((int entityInQueryIndex, ref TargetData targetData, in LocalToWorld localToWorld, in TeamData teamData, in TargetDetectionData targetDetectionData) =>
            {
                _closestDistance = float.MaxValue;
                _closestEntity = Entity.Null;
                float3 position = localToWorld.Position;
                Team team = teamData.Value;

                for (int i = 0; i < entityArray.Length; i++)
                {
                    if (entityArray[i].Index == entityInQueryIndex || GetComponent<TeamData>(entityArray[i]).Value == team)
                        continue;
                    
                    float3 targetPos = GetComponent<LocalToWorld>(entityArray[i]).Position;
                    float distance = math.distance(position, targetPos);
                    
                    if (distance > targetDetectionData.Radius)
                        continue;
                    
                    if (distance < _closestDistance)
                    {
                        _closestDistance = distance;
                        _closestEntity = entityArray[i];
                    }
                }
                
                targetData.Value = _closestEntity;
            })
            .WithDisposeOnCompletion(entityArray)
            .WithoutBurst()
            .Run();
        }
    }
}