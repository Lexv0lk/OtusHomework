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
            Entities.WithAll<BulletSpawnEvent>().ForEach((Entity entity, in ShootData shootData, in WeaponData weaponData) =>
            {
                var bullet = EntityManager.Instantiate(weaponData.BulletPrefab);
                EntityManager.SetComponentData(bullet, new Translation{ Value = shootData.FirePosition });
                EntityManager.AddComponentData(bullet, new MoveData { Direction = shootData.Direction, Speed = 5});
                EntityManager.RemoveComponent(entity, typeof(BulletSpawnEvent));
            }).WithStructuralChanges().Run();
        }
    }
}