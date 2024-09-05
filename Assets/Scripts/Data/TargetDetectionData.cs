using Unity.Entities;
using Unity.Physics;

namespace Data
{
    [GenerateAuthoringComponent]
    public struct TargetDetectionData : IComponentData
    {
        public float Radius;
    }
}