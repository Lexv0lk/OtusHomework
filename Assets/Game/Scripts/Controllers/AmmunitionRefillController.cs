using Cysharp.Threading.Tasks;
using Game.Scripts.Configs.Controllers;
using Game.Scripts.Models;
using Zenject;

namespace Game.Scripts.Controllers
{
    public class AmmunitionRefillController : IInitializable
    {
        private readonly AmmunitionRefillConfig _config;
        private readonly RiffleStoreModel _model;

        public AmmunitionRefillController(AmmunitionRefillConfig config, RiffleStoreModel model)
        {
            _config = config;
            _model = model;
        }
        
        public void Initialize()
        {
            StartRefilling().Forget();
        }

        private async UniTaskVoid StartRefilling()
        {
            while (true)
                await RefillDelayed();
        }

        private async UniTask RefillDelayed()
        {
            await UniTask.WaitForSeconds(_config.Delay);
            _model.AmmunitionAmount.Value += _config.RefillCount;
        }
    }
}