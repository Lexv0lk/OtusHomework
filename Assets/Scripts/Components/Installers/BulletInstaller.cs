using Client.Common;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Components.Installers
{
    public class BulletInstaller : EntityInstaller
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private Team _team;

        protected override void Install(Entity entity)
        {
            entity.AddData(new BulletTag());
            entity.AddData(new Position() { Value = transform.position });
            entity.AddData(new Rotation() { Value = transform.rotation });
            entity.AddData(new MoveSpeed() { BaseSpeed = _moveSpeed, CurrentSpeed = _moveSpeed });
            entity.AddData(new TeamData() { Value = _team });
            entity.AddData(new MoveDirection() { Value = transform.forward});
            entity.AddData(new TransformView() { Value = transform });
            entity.AddData(new Damage() { Value = _damage });
        }

        protected override void Dispose(Entity entity)
        {
        }
    }
}