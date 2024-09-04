using Data;
using Data.Events;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Systems
{
    public partial class BulletSpawnSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((Entity entity, in LocalToWorld localToWorld, in ShootData shootData, in BulletSpawnEvent bulletSpawnEvent) =>
            {
                var bullet = EntityManager.Instantiate(shootData.BulletPrefab);
                EntityManager.SetComponentData(bullet, new Translation{ Value = localToWorld.Position });
                EntityManager.AddComponentData(bullet, new MoveDirectionData { Direction = new float3(0, 0, -5) });
                EntityManager.RemoveComponent(entity, typeof(BulletSpawnEvent));
            }).WithStructuralChanges().Run();
        }
    }
}