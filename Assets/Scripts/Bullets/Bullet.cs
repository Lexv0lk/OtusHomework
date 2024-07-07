using ShootEmUp.Common;
using System;
using ShootEmUp.GameStates;
using UnityEngine;

namespace ShootEmUp.Bullets
{
    public sealed class Bullet : MonoBehaviour, IGamePauseListener, IGameResumeListener
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public Team Team { get; private set; }
        public int Damage { get; private set; }

        public event Action<Bullet, Collision2D> CollisionEntered;

        public void Init(BulletShooter.ShootArgs args)
        {
            _rigidbody2D.velocity = args.Velocity;
            gameObject.layer = args.PhysicsLayer;
            transform.position = args.Position;
            _spriteRenderer.color = args.Color;
            Team = args.Team;
            Damage = args.Damage;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            CollisionEntered?.Invoke(this, collision);
        }

        void IGamePauseListener.OnPause()
        {
            _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        void IGameResumeListener.OnResume()
        {
            _rigidbody2D.constraints = RigidbodyConstraints2D.None;
        }
    }
}