using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Components.Installers
{
    public class ParticleInstaller : EntityInstaller
    {
        [SerializeField] private float _maximalLifeTime = -1;
        
        protected override void Install(Entity entity)
        {
            if (_maximalLifeTime >= 0)
                entity.AddData(new LifeTimeLimit { MaximalLifeTime = _maximalLifeTime, CurrentLifeTime = 0 });
        }

        protected override void Dispose(Entity entity)
        {
            
        }
    }
}