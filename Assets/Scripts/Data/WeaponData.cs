using Unity.Entities;

namespace Data
{
    public struct WeaponData : IComponentData
    {
        public Entity BulletPrefab;
        public Entity FirePoint;
        public int Damage;
        public float BulletSpeed;
    }
}