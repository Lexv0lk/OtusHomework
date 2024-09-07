using Data;
using Data.Tags;
using Unity.Burst;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;

namespace Systems
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(StepPhysicsWorld))]
    [UpdateBefore(typeof(EndFramePhysicsSystem))]
    public partial class BulletCollisionObserveSystem : SystemBase
    {
        private StepPhysicsWorld _stepPhysicsWorld;
        private EndSimulationEntityCommandBufferSystem _commandBufferSystem;

        protected override void OnCreate()
        {
            _stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
            _commandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override void OnStartRunning()
        {
            base.OnStartRunning();
            this.RegisterPhysicsRuntimeSystemReadOnly();
        }

        protected override void OnUpdate()
        {
            var bulletsTriggerJob = new BulletsTriggerJob
            {
                BulletGroup = GetComponentDataFromEntity<BulletTag>(),
                TeamGroup = GetComponentDataFromEntity<TeamData>(),
                ApplyDamageGroup = GetComponentDataFromEntity<ApplyDamageData>(),
                DestroyGroup = GetComponentDataFromEntity<DestroyTag>(),
                CommandBuffer = _commandBufferSystem.CreateCommandBuffer()
            };
            
            Dependency = bulletsTriggerJob.Schedule(_stepPhysicsWorld.Simulation, Dependency);
            _commandBufferSystem.AddJobHandleForProducer(Dependency);
        }
        
        [BurstCompile]
        private struct BulletsTriggerJob : ITriggerEventsJob
        {
            public ComponentDataFromEntity<BulletTag> BulletGroup;
            public ComponentDataFromEntity<TeamData> TeamGroup;
            public ComponentDataFromEntity<ApplyDamageData> ApplyDamageGroup;
            public ComponentDataFromEntity<DestroyTag> DestroyGroup;
            public EntityCommandBuffer CommandBuffer;
        
            private bool IsBullet(Entity entity)
            {
                return BulletGroup.HasComponent(entity);
            }

            private bool IsDestroyed(Entity entity)
            {
                return DestroyGroup.HasComponent(entity);
            }
            
            public void Execute(TriggerEvent triggerEvent)
            {
                if (IsDestroyed(triggerEvent.EntityA) || IsDestroyed(triggerEvent.EntityB))
                    return;
                
                bool isBulletA = IsBullet(triggerEvent.EntityA);
                bool isBulletB = IsBullet(triggerEvent.EntityB);
                
                if (isBulletA && isBulletB)
                    return;
        
                TeamData teamA, teamB;
        
                if (TeamGroup.TryGetComponent(triggerEvent.EntityA, out teamA) &&
                    TeamGroup.TryGetComponent(triggerEvent.EntityB, out teamB))
                {
                    if (teamA.Value == teamB.Value)
                        return;
                    
                    if (isBulletA)
                    {
                        if (ApplyDamageGroup.TryGetComponent(triggerEvent.EntityA, out ApplyDamageData damageData))
                        {
                            CommandBuffer.AddComponent(triggerEvent.EntityB, new TakeDamageData { Value = damageData.Value });
                            CommandBuffer.AddComponent(triggerEvent.EntityA, new DestroyTag());
                        }
                    }
                    else
                    {
                        if (ApplyDamageGroup.TryGetComponent(triggerEvent.EntityB, out ApplyDamageData damageData))
                        {
                            CommandBuffer.AddComponent(triggerEvent.EntityA, new TakeDamageData { Value = damageData.Value });
                            CommandBuffer.AddComponent(triggerEvent.EntityB, new DestroyTag());
                        }
                    }
                }
            }
        }
    }
}