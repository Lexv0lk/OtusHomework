using Unity.Entities;

namespace Data
{
    [GenerateAuthoringComponent]
    public struct ShootData : IComponentData
    {
        public Entity BulletPrefab;
    }
}