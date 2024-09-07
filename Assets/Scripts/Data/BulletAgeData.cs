using Unity.Entities;

namespace Data
{
    [GenerateAuthoringComponent]
    public struct BulletAgeData : IComponentData
    {
        public float Age;
        public float MaxAge;
    }
}