using Entities.Components;
using UnityEngine;

namespace Entities.Heros
{
    [CreateAssetMenu(fileName = "Devourer", menuName = "Entities/Devourer")]
    public class Devourer : BaseHeroConfig
    {
        [Header("Random Attack")] 
        [SerializeField] private int _damage = 3;
        
        protected override void SetupComponents()
        {
            base.SetupComponents();
            Add(new RandomAttackComponent(_damage));
        }
    }
}