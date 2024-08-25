using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Models;
using Game.Scripts.Tech;

namespace Game.Scripts.UI.Presenters
{
    public class GameInfoViewPresenter : IGameInfoViewPresenter
    {
        private readonly RiffleStoreModel _ammoModel;
        private readonly KillCountModel _killsModel;
        private readonly IAtomicValueObservable<int> _playerHealth;

        private AtomicVariable<string> _bulletsLeft = new();
        private AtomicVariable<string> _hitPoints = new();
        private AtomicVariable<string> _killCount = new();
        
        public GameInfoViewPresenter(RiffleStoreModel ammoModel, KillCountModel killsModel, AtomicEntity player)
        {
            _ammoModel = ammoModel;
            _killsModel = killsModel;
            _playerHealth = player.Get<IAtomicValueObservable<int>>(LifeAPI.HEALTH);
            
            OnAmmoChanged(_ammoModel.AmmunitionAmount.Value);
            OnKillsChanged(_killsModel.Kills.Value);
            OnHitPointsChanged(_playerHealth.Value);
            
            _ammoModel.AmmunitionAmount.Subscribe(OnAmmoChanged);;
            _killsModel.Kills.Subscribe(OnKillsChanged);
            _playerHealth.Subscribe(OnHitPointsChanged);
        }
        
        public IAtomicValueObservable<string> BulletsLeft => _bulletsLeft;
        public IAtomicValueObservable<string> HitPoints => _hitPoints;
        public IAtomicValueObservable<string> KillCount => _killCount;

        private void OnAmmoChanged(int newVal)
        {
            _bulletsLeft.Value = $"BULLETS: {newVal}";
        }

        private void OnKillsChanged(int newVal)
        {
            _killCount.Value = $"KILLS: {newVal}";
        }

        private void OnHitPointsChanged(int newVal)
        {
            _hitPoints.Value = $"HIT POINTS {newVal}";
        }

        ~GameInfoViewPresenter()
        {
            _ammoModel.AmmunitionAmount.Unsubscribe(OnAmmoChanged);;
            _killsModel.Kills.Unsubscribe(OnKillsChanged);
            _playerHealth.Unsubscribe(OnHitPointsChanged);
        }
    }
}