using Atomic.Elements;
using Atomic.Objects;

namespace Game.Scripts.Mechanics
{
    public class ReloadingHitMechanic : IAtomicUpdate
    {
        private readonly IAtomicValue<bool> _isReachedTarget;
        private readonly IAtomicAction<int> _takeDamageAction;
        private readonly float _reloadTime;
        private readonly int _damage;

        private float _currentReloadTime;

        public ReloadingHitMechanic(IAtomicValue<bool> isReachedTarget, IAtomicAction<int> takeDamageAction,
            float reloadTime, int damage)
        {
            _isReachedTarget = isReachedTarget;
            _takeDamageAction = takeDamageAction;
            _reloadTime = reloadTime;
            _damage = damage;
        }
        
        public void OnUpdate(float deltaTime)
        {
            _currentReloadTime -= deltaTime;
            
            if (_isReachedTarget.Value && _currentReloadTime <= 0)
            {
                _takeDamageAction.Invoke(_damage);
                _currentReloadTime = _reloadTime;
            }
        }
    }
}