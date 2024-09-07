using Data;
using Data.Events;
using Unity.Entities;
using Unity.Transforms;

namespace Systems
{
    public partial class BulletSpawnSystem : SystemBase
    {
        private BeginSimulationEntityCommandBufferSystem _ecbs;
        
        protected override void OnCreate()
        {
            _ecbs = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem >();
        }

        protected override void OnUpdate()
        {
            var commandBuffer = _ecbs.CreateCommandBuffer();
            
            Entities.WithAll<BulletSpawnEvent>().ForEach((Entity entity, in ShootData shootData, in WeaponData weaponData) =>
            {
                var bullet = commandBuffer.Instantiate(weaponData.BulletPrefab);
                commandBuffer.SetComponent(bullet, new Translation{ Value = shootData.FirePosition });
                commandBuffer.AddComponent(bullet, new MoveData { Direction = shootData.Direction, Speed = weaponData.BulletSpeed });
                commandBuffer.AddComponent(bullet, new ApplyDamageData { Value = weaponData.Damage });
                commandBuffer.RemoveComponent<BulletSpawnEvent>(entity);
            }).Schedule();
            
            _ecbs.AddJobHandleForProducer(Dependency);
        }
    }
}