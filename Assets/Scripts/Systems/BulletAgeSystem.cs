using Data;
using Data.Tags;
using Unity.Entities;

namespace Systems
{
    public partial class BulletAgeSystem : SystemBase
    {
        private EndSimulationEntityCommandBufferSystem _ecbs;
        
        protected override void OnCreate()
        {
            _ecbs = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            var commandBuffer = _ecbs.CreateCommandBuffer();
            float deltaTime = Time.DeltaTime;

            Entities.ForEach((Entity entity, ref BulletAgeData bulletAgeData) =>
            {
                bulletAgeData.Age += deltaTime;
                
                if (bulletAgeData.Age >= bulletAgeData.MaxAge)
                    commandBuffer.AddComponent<DestroyTag>(entity);
            }).Schedule();
            
            _ecbs.AddJobHandleForProducer(Dependency);
        }
    }
}