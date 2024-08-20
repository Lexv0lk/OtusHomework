using Atomic.Elements;
using Game.Scripts.Configs.Models;

namespace Game.Scripts.Models
{
    public class RiffleStoreModel
    {
        public AtomicVariable<int> AmmunitionAmount;

        public RiffleStoreModel(RiffleStoreConfig config)
        {
            AmmunitionAmount = new AtomicVariable<int>(config.StartAmount);
        }
    }
}