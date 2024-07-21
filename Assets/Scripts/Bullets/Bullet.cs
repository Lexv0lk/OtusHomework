using ShootEmUp.Common;
using System;
using ShootEmUp.GameStates;
using UnityEngine;

namespace ShootEmUp.Bullets
{
    public sealed class Bullet : MonoBehaviour, IGamePauseListener, IGameFinishListener, IGameResumeListener
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private Vector2 _lastVelocity;

        public Team Team { get; private set; }
        public int Damage { get; private set; }

        public event Action<Bullet, Collision2D> CollisionEntered;

        public void Init(BulletSpawner.TeamShootArgs args)
        {
            _rigidbody2D.velocity = args.ShootArgs.Velocity;
            gameObject.layer = args.ShootArgs.PhysicsLayer;
            transform.position = args.ShootArgs.Position;
            _spriteRenderer.color = args.ShootArgs.Color;
            Team = args.Team;
            Damage = args.ShootArgs.Damage;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            CollisionEntered?.Invoke(this, collision);
        }

        public void OnPause()
        {
            _lastVelocity = _rigidbody2D.velocity;
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        public void OnResume()
        {
            _rigidbody2D.constraints = RigidbodyConstraints2D.None;
            _rigidbody2D.velocity = _lastVelocity;
        }

        public void OnFinish()
        {
            _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}