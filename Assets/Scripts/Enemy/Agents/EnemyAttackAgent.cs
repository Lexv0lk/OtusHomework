using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : MonoBehaviour
    {
        public event Action<BulletSystem.Args> OnFire;

        [SerializeField] private WeaponComponent _weaponComponent;
        [SerializeField] private TeamComponent _teamComponent;
        [SerializeField] private EnemyMoveAgent moveAgent;
        [SerializeField] private float countdown;

        private GameObject target;
        private float currentTime;

        public void SetTarget(GameObject target)
        {
            this.target = target;
        }

        public void Reset()
        {
            this.currentTime = this.countdown;
        }

        private void FixedUpdate()
        {
            if (!this.moveAgent.IsReached)
            {
                return;
            }
            
            if (!this.target.GetComponent<HitPointsComponent>().IsHitPointsExists())
            {
                return;
            }

            this.currentTime -= Time.fixedDeltaTime;
            if (this.currentTime <= 0)
            {
                this.Fire();
                this.currentTime += this.countdown;
            }
        }

        private void Fire()
        {
            BulletSystem.Args args = _weaponComponent.GetFireArgsAtTarget(target.transform.position);
            args.Team = _teamComponent.Team;
            this.OnFire?.Invoke(args);
        }
    }
}