using Unity.Entities;
using Unity.Mathematics;

namespace Data
{
    [GenerateAuthoringComponent]
    public struct MoveData : IComponentData
    {
        public float3 Direction;
        public float Speed;
    }
}