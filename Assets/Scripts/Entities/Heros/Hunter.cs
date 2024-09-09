using Entities.Components;
using UnityEngine;

namespace Entities.Heros
{
    [CreateAssetMenu(fileName = "Hunter", menuName = "Entities/Hunter")]
    public class Hunter : BaseHeroConfig
    {
        protected override void SetupComponents()
        {
            base.SetupComponents();
            Add(new ProtectedAttackComponent());
        }
    }
}