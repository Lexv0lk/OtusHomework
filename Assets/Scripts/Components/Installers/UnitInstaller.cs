﻿using Client.Common;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Components.Installers
{
    public abstract class UnitInstaller : EntityInstaller
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _health;
        [SerializeField] private Team _team;
        [SerializeField] private AttackData _attackData;
        [SerializeField] private Transform _bloodPoint;
        
        protected override void Install(Entity entity)
        {
            entity.AddData(new UnitTag());
            entity.AddData(new CenterPointView { Value = _bloodPoint });
            entity.AddData(new Position() { Value = transform.position });
            entity.AddData(new Rotation() { Value = transform.rotation });
            entity.AddData(new MoveSpeed() { BaseSpeed = _moveSpeed, CurrentSpeed = 0 });
            entity.AddData(new TeamData() { Value = _team });
            entity.AddData(new MoveDirection());
            entity.AddData(new TransformView() { Value = transform });
            entity.AddData(new AnimatorView() { Animator = _animator });
            entity.AddData(new TargetEntity());
            entity.AddData(new Reload());
            entity.AddData(new Health() { CurrentHealth = _health, MaxHealth = _health});
            entity.AddData(_attackData);
        }

        protected override void Dispose(Entity entity)
        {
            
        }
    }
}