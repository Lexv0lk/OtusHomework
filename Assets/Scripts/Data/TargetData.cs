using Unity.Entities;

namespace Data
{
    [GenerateAuthoringComponent]
    public struct TargetData : IComponentData
    {
        public Entity Value;
    }
}