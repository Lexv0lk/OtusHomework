using Entities.Components;
using UnityEngine;

namespace Entities.Heros
{
    [CreateAssetMenu(fileName = "Palading", menuName = "Entities/Paladin")]
    public class Paladin : BaseHeroConfig
    {
        protected override void SetupComponents()
        {
            base.SetupComponents();
            Add(new ShieldComponent());
        }
    }
}