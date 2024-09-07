using Data;
using Data.Tags;
using Unity.Entities;
using Unity.Mathematics;

namespace Systems
{
    public partial class DamageProcessSystem : SystemBase
    {
        private BeginSimulationEntityCommandBufferSystem _ecbs;
        
        protected override void OnCreate()
        {
            _ecbs = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();
        }
        
        protected override void OnUpdate()
        {
            var commandBuffer = _ecbs.CreateCommandBuffer();

            Entities.ForEach((Entity entity, ref HealthData healthData, in TakeDamageData takeDamageData) =>
            {
                healthData.Value = math.max(0, healthData.Value - takeDamageData.Value);
                commandBuffer.RemoveComponent<TakeDamageData>(entity);
                
                if (healthData.Value == 0)
                    commandBuffer.AddComponent(entity, new DestroyTag());
            }).Schedule();
            
            _ecbs.AddJobHandleForProducer(Dependency);
        }
    }
}