using Data;
using Data.Events;
using Unity.Entities;
using UnityEngine;

namespace Systems
{
    public partial class FireRequestSystem : SystemBase
    {
        private float _currentDelay = 1;
        
        protected override void OnUpdate()
        {
            _currentDelay += Time.DeltaTime;

            if (_currentDelay >= 1)
            {
                Entities.ForEach((Entity entity, in WeaponData weaponData) =>
                {   
                    EntityManager.AddComponentData(entity, new FireRequestEvent());
                    _currentDelay = 0;
                }).WithStructuralChanges().Run();
            }
        }
    }
}