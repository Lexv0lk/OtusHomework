using Entities.Components;
using UnityEngine;

namespace Entities.Heros
{
    [CreateAssetMenu(fileName = "FrozenMage", menuName = "Entities/FrozenMage")]
    public class FrozenMage : BaseHeroConfig
    {
        [Header("Freeze Attack")] 
        [SerializeField] private int _turnsInFreeze = 1;
        
        protected override void SetupComponents()
        {
            base.SetupComponents();
            Add(new FrozeAttackComponent(_turnsInFreeze));
        }
    }
}