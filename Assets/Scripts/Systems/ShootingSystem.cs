using Data;
using Data.Events;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Systems
{
    public partial class ShootingSystem : SystemBase
    {
        private BeginSimulationEntityCommandBufferSystem _endSimulationEntityCommandBufferSystem;
        
        protected override void OnCreate()
        {
            _endSimulationEntityCommandBufferSystem = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            var commandBuffer = _endSimulationEntityCommandBufferSystem.CreateCommandBuffer();
            
            Entities.WithAll<FireRequestEvent>().WithNone<BulletSpawnEvent>().ForEach((Entity entity, ref ShootData shootData, in LocalToWorld localToWorld, in WeaponData weaponData, in TargetData targetData) =>
            {
                if (targetData.Value == Entity.Null)
                    return;
                
                var firePointEntity = weaponData.FirePoint;
                float3 firePointPosition = GetComponent<LocalToWorld>(firePointEntity).Position;
                
                float3 direction;
                
                if (HasComponent<LocalToWorld>(targetData.Value) == false)
                    return;
                
                LocalToWorld targetLocalToWorld = GetComponent<LocalToWorld>(targetData.Value);
                direction = targetLocalToWorld.Position - localToWorld.Position;
                direction = math.normalize(direction);
                
                shootData.FirePosition = firePointPosition;
                shootData.Direction = direction;

                commandBuffer.AddComponent(entity, new BulletSpawnEvent());
                commandBuffer.RemoveComponent<FireRequestEvent>(entity);
            }).Schedule();
            
            _endSimulationEntityCommandBufferSystem.AddJobHandleForProducer(Dependency);
        }
    }
}