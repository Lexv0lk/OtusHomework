using Data;
using Data.Events;
using Unity.Entities;

namespace Systems
{
    public partial class ShootingSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((Entity entity, in FireRequestEvent fireRequestEvent, in WeaponData shootData) =>
            {
                var firePointEntity = shootData.FirePoint;
                EntityManager.AddComponentData(firePointEntity, new BulletSpawnEvent());
                EntityManager.RemoveComponent(entity, typeof(FireRequestEvent));
            }).WithStructuralChanges().Run();
        }
    }
}