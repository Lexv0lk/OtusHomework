using Unity.Entities;

namespace Data
{
    [GenerateAuthoringComponent]
    public struct ShootingDelay : IComponentData
    {
        public int Value;
    }
}