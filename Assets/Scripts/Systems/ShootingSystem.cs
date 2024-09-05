using Data;
using Data.Events;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Systems
{
    public partial class ShootingSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities.WithAll<FireRequestEvent>().ForEach((Entity entity, ref ShootData shootData, in LocalToWorld localToWorld, in WeaponData weaponData, in TargetData targetData) =>
            {
                if (targetData.Value == Entity.Null)
                    return;
                
                var firePointEntity = weaponData.FirePoint;
                float3 firePointPosition = EntityManager.GetComponentData<LocalToWorld>(firePointEntity).Position;
                float3 direction = GetFireDirection(targetData, localToWorld);
                shootData.FirePosition = firePointPosition;
                shootData.Direction = direction;

                EntityManager.AddComponentData(entity, new BulletSpawnEvent());
                EntityManager.RemoveComponent(entity, typeof(FireRequestEvent));
            }).WithStructuralChanges().Run();
        }

        private float3 GetFireDirection(TargetData targetData, LocalToWorld localToWorld)
        {
            float3 direction;
            LocalToWorld targetLocalToWorld = EntityManager.GetComponentData<LocalToWorld>(targetData.Value);
            direction = targetLocalToWorld.Position - localToWorld.Position;
            direction = math.normalize(direction);
            return direction;
        }
    }
}