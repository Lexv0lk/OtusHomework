using Unity.Entities;
using Unity.Mathematics;

namespace Data
{
    [GenerateAuthoringComponent]
    public struct MoveDirectionData : IComponentData
    {
        public float3 Direction;
    }
}