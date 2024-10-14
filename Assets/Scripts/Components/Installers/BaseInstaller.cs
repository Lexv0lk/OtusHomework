using Client.Common;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Components.Installers
{
    public class BaseInstaller : EntityInstaller
    {
        [SerializeField] private float _health;
        [SerializeField] private Team _team;
        [SerializeField] private Transform _baseEnter;
        
        protected override void Install(Entity entity)
        {
            entity.AddData(new Health() { CurrentHealth = _health, MaxHealth = _health });
            entity.AddData(new TeamData() { Value = _team });
            entity.AddData(new Position() { Value = _baseEnter.position});
        }

        protected override void Dispose(Entity entity)
        {
            
        }
    }
}