using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Components.Installers
{
    public class SwordsmanInstaller : UnitInstaller
    {
        [SerializeField] private float _damage;
        
        protected override void Install(Entity entity)
        {
            base.Install(entity);
            entity.AddData(new Damage() { Value = _damage });
        }
    }
}