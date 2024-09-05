using Unity.Entities;
using Unity.Mathematics;

namespace Data
{
    [GenerateAuthoringComponent]
    public struct ShootData : IComponentData
    {
        public float3 FirePosition;
        public float3 Direction;
    }
}