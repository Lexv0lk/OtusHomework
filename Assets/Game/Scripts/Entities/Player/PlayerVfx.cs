using System;
using UnityEngine;

namespace Game.Scripts.Entities
{
    [Serializable]
    public class PlayerVfx
    {
        [SerializeField] private ParticleSystem _shootVfx;

        private PlayerCore _playerCore;
        
        public void Compose(PlayerCore core)
        {
            _playerCore = core;
            _playerCore.ShootComponent.ShootEvent.Subscribe(PlayShootVfx);
        }

        public void Dispose()
        {
            _playerCore.ShootComponent.ShootEvent.Unsubscribe(PlayShootVfx);
        }

        private void PlayShootVfx()
        {
            _shootVfx.Play();
        }
    }
}