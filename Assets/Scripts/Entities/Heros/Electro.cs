using Entities.Components;
using UnityEngine;

namespace Entities.Heros
{
    [CreateAssetMenu(fileName = "Electro", menuName = "Entities/Electro")]
    public class Electro : BaseHeroConfig
    {
        [Header("Mass Attack")] 
        [SerializeField] private int _damage = 1;
        
        protected override void SetupComponents()
        {
            base.SetupComponents();
            Add(new MassAttackComponent(_damage));
        }
    }
}