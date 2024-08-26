using ShootEmUp.Common;
using System;
using ShootEmUp.GameStates;
using UnityEngine;

namespace ShootEmUp.Bullets
{
    public sealed class Bullet : MonoBehaviour, IGamePauseListener, IGameResumeListener, IGameFinishListener
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private Vector2 _lastVelocity;

        public Team Team { get; private set; }
        public int Damage { get; private set; }

        public event Action<Bullet, Collision2D> CollisionEntered;

        public void Init(BulletSpawner.ShootArgs args)
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
            _lastVelocity = _rigidbody2D.velocity;
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        void IGameResumeListener.OnResume()
        {
            _rigidbody2D.constraints = RigidbodyConstraints2D.None;
            _rigidbody2D.velocity = _lastVelocity;
        }

        void IGameFinishListener.OnFinish()
        {
            _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}