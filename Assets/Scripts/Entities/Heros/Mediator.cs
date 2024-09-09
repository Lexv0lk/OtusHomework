using Entities.Components;
using UnityEngine;

namespace Entities.Heros
{
    [CreateAssetMenu(fileName = "Mediator", menuName = "Entities/Mediator")]
    public class Mediator : BaseHeroConfig
    {
        [Header("Heal")] 
        [SerializeField] private int _healAmount = 1;
        
        protected override void SetupComponents()
        {
            base.SetupComponents();
            Add(new RandomHealComponent(_healAmount));
        }
    }
}