using System.Collections.Generic;
using Configs;
using Data;
using Unity.Entities;
using Unity.Entities.Hybrid.Internal;
using UnityEngine;

namespace Authorings
{
    [DisallowMultipleComponent]
    public class WeaponDataAuthoring : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
    {
        [SerializeField] private BulletConfig _bulletConfig;
        [SerializeField] private GameObject _firePoint;
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            WeaponData component = default(WeaponData);
            component.BulletPrefab = conversionSystem.GetPrimaryEntity(_bulletConfig.Prefab);
            component.FirePoint = conversionSystem.GetPrimaryEntity(_firePoint);
            component.Damage = _bulletConfig.Damage;
            component.BulletSpeed = _bulletConfig.Speed;
            dstManager.AddComponentData(entity, component);
        }

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            GeneratedAuthoringComponentImplementation.AddReferencedPrefab(referencedPrefabs, _bulletConfig.Prefab);
            GeneratedAuthoringComponentImplementation.AddReferencedPrefab(referencedPrefabs, _firePoint);
        }
    }
}