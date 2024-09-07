using Data.Tags;
using Unity.Entities;

namespace Systems
{
    [UpdateInGroup(typeof(LateSimulationSystemGroup))]
    public partial class DestroyEntitiesSystem : SystemBase
    {
        private EndSimulationEntityCommandBufferSystem _ecbs;
        
        protected override void OnCreate()
        {
            _ecbs = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            var commandBuffer = _ecbs.CreateCommandBuffer();

            Entities.WithAll<DestroyTag>().ForEach((Entity entity) =>
            {
                commandBuffer.DestroyEntity(entity);
            }).Schedule();
            
            _ecbs.AddJobHandleForProducer(Dependency);
        }
    }
}