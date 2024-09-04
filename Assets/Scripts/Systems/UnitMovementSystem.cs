using Data;
using Unity.Entities;
using Unity.Physics;

namespace Systems
{
    public partial class UnitMovementSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((ref PhysicsVelocity velocity, in MoveDirectionData directionData) =>
            {
                velocity.Linear = directionData.Direction;
            }).Schedule();
        }
    }
}