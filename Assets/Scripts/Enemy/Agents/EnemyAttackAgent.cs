using ShootEmUp.Bullets;
using ShootEmUp.Characters;
using ShootEmUp.Components;
using System;
using UnityEngine;

namespace ShootEmUp.Enemies.Agents
{
    public sealed class EnemyAttackAgent : MonoBehaviour
    {
        [SerializeField] private WeaponComponent _weaponComponent;
        [SerializeField] private TeamComponent _teamComponent;
        [SerializeField] private EnemyMoveAgent _moveAgent;
        [SerializeField] private float _fireDelay;

        private Character _target;
        private float _currentDelay;

        public event Action<BulletSystem.Args> Fired;

        public void SetTarget(Character target)
        {
            _target = target;
        }

        public void Reset()
        {
            _currentDelay = _fireDelay;
        }

        private void FixedUpdate()
        {
            if (_moveAgent.IsReached == false)
                return;
            
            if (_target.HitPointsComponent.IsHitPointsExists() == false)
                return;

            _currentDelay -= Time.fixedDeltaTime;

            if (_currentDelay <= 0)
            {
                Fire();
                _currentDelay += _fireDelay;
            }
        }

        private void Fire()
        {
            BulletSystem.Args args = _weaponComponent.GetFireArgsAtTarget(_target.transform.position);
            args.Team = _teamComponent.Team;
            Fired?.Invoke(args);
        }
    }
}