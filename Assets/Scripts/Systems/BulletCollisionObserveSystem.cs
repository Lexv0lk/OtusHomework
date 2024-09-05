using Data;
using Data.Tags;
using Structs;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;

namespace Systems
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(StepPhysicsWorld))]
    [UpdateBefore(typeof(EndFramePhysicsSystem))]
    public partial class BulletCollisionObserveSystem : SystemBase
    {
        protected StepPhysicsWorld StepPhysicsWorld;
        protected EndSimulationEntityCommandBufferSystem CommandBufferSystem;

        protected override void OnCreate()
        {
            StepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
            CommandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override void OnStartRunning()
        {
            base.OnStartRunning();
            this.RegisterPhysicsRuntimeSystemReadOnly();
        }

        protected override void OnUpdate()
        {
            Dependency = new TestCollisionJob().Schedule(StepPhysicsWorld.Simulation, Dependency);
        }
        
        [BurstCompile]
        private struct TestCollisionJob : ITriggerEventsJob
        {
            public void Execute(TriggerEvent triggerEvent)
            {
                Debug.Log($"collision event: {triggerEvent}. Entities: {triggerEvent.EntityA}, {triggerEvent.EntityB}");
            }
        }
        
        // private struct CollisionEvent : ICollisionEventsJob
        // {
        //     public ComponentDataFromEntity<BulletTag> BulletGroup;
        //     public ComponentDataFromEntity<TeamData> TeamGroup;
        //
        //     private bool IsBullet(Entity entity)
        //     {
        //         return BulletGroup.HasComponent(entity);
        //     }
        //     
        //     public void Execute(Unity.Physics.CollisionEvent collisionEvent)
        //     {
        //         bool isBulletA = IsBullet(collisionEvent.EntityA);
        //         bool isBulletB = IsBullet(collisionEvent.EntityB);
        //         
        //         if (isBulletA && isBulletB)
        //             return;
        //
        //         TeamData teamA, teamB;
        //
        //         if (TeamGroup.TryGetComponent(collisionEvent.EntityA, out teamA) &&
        //             TeamGroup.TryGetComponent(collisionEvent.EntityB, out teamB))
        //         {
        //             if (teamA.Value == teamB.Value)
        //                 return;
        //         }
        //     }
        // }
    }
}