using Unity.Entities;

namespace Data
{
    [GenerateAuthoringComponent]
    public struct WeaponData : IComponentData
    {
        public Entity BulletPrefab;
        public Entity FirePoint;
    }
}