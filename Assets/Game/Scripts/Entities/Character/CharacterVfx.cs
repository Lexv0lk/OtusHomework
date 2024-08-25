using System;
using UnityEngine;

namespace Game.Scripts.Entities
{
    [Serializable]
    public class CharacterVfx
    {
        [SerializeField] private ParticleSystem _takeDamageVfx;

        private CharacterCore _core;

        public void Compose(CharacterCore core)
        {
            _core = core;
            _core.LifeComponent.TakeDamageEvent.Subscribe(PlayTakeDamageVfx);
        }

        public void Dispose()
        {
            _core.LifeComponent.TakeDamageEvent.Unsubscribe(PlayTakeDamageVfx);
        }

        private void PlayTakeDamageVfx(int _)
        {
            _takeDamageVfx.Play();
        }
    }
}