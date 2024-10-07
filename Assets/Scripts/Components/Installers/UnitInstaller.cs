using Client.Common;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Components.Installers
{
    public class UnitInstaller : EntityInstaller
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private Team _team;
        
        protected override void Install(Entity entity)
        {
            entity.AddData(new Position() { Value = transform.position});
            entity.AddData(new Rotation() { Value = transform.rotation});
            entity.AddData(new MoveSpeed() { Value = _moveSpeed});
            entity.AddData(new TeamData() { Value = _team});
            entity.AddData(new MoveDirection());
        }

        protected override void Dispose(Entity entity)
        {
            
        }
    }
}