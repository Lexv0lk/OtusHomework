using ShootEmUp.Bullets;
using ShootEmUp.Characters;
using ShootEmUp.Components;
using System;
using ShootEmUp.GameUpdate;
using UnityEngine;

namespace ShootEmUp.Enemies.Agents
{
    public sealed class CooldownAttackMechanic : IGameFixedUpdateListener
    {
        private readonly WeaponComponent _weaponComponent;
        private readonly TeamComponent _teamComponent;
        private readonly float _fireDelay;

        private IAttackableTarget _target;
        private float _currentDelay;
        private bool _canAttack;
        
        private Action<BulletSpawner.TeamShootArgs> _attackCallback;

        public CooldownAttackMechanic(WeaponComponent weaponComponent, TeamComponent teamComponent,
            float fireDelay, Action<BulletSpawner.TeamShootArgs> attackCallback)
        {
            _weaponComponent = weaponComponent;
            _teamComponent = teamComponent;
            _fireDelay = fireDelay;
            _attackCallback = attackCallback;
        }
        
        public void StartAttacking(IAttackableTarget target)
        {
            _target = target;
            _canAttack = true;
        }

        public void StopAttacking()
        {
            _canAttack = false;
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            if (_canAttack == false || _target.HitPointsComponent.IsHitPointsExists() == false)
                return;

            _currentDelay -= fixedDeltaTime;

            if (_currentDelay <= 0)
            {
                Attack();
                _currentDelay += _fireDelay;
            }
        }

        private void Attack()
        {
            BulletSpawner.ShootArgs args = _weaponComponent.GetShootArgs(_target.Position);

            var teamShootArgs = new BulletSpawner.TeamShootArgs()
            {
                ShootArgs = args,
                Team = _teamComponent.Team
            };
            
            _attackCallback?.Invoke(teamShootArgs);
        }
    }
}