using Data;
using Unity.Entities;
using Unity.Physics;

namespace Systems
{
    public partial class MovementSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((ref PhysicsVelocity velocity, in MoveData moveData) =>
            {
                velocity.Linear = moveData.Direction * moveData.Speed;
            }).Schedule();
        }
    }
}