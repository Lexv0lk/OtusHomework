using Data;
using Data.Tags;
using Enums;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Systems
{
    public partial class TargetDetectionSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var queryDescription = new EntityQueryDesc
            {
                All = new ComponentType[] { typeof(TeamData) },
                None = new ComponentType[] { typeof(BulletTag) }
            };
            var entityArray = GetEntityQuery(queryDescription).ToEntityArray(Allocator.TempJob);
            
            Entities.ForEach((int entityInQueryIndex, ref TargetData targetData, in LocalToWorld localToWorld, in TeamData teamData, in TargetDetectionData targetDetectionData) =>
            {
                float closestDistance = float.MaxValue;
                Entity closestEntity = Entity.Null;
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
                    
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestEntity = entityArray[i];
                    }
                }
                
                targetData.Value = closestEntity;
            })
            .WithDisposeOnCompletion(entityArray)
            .Schedule();
        }
    }
}